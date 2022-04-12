using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Fins.Message;
using Fins.Utility;

namespace Fins.IO
{
    internal class FinsTcpTransport : FinsTransport
    {
        private static readonly object _transactionIdLock = new object();
        private ushort _transactionId;
        private byte[] tcpFinsHeader = new byte[16];
        private byte[] tcpNodeAddrReq = new byte[20];
        private byte nodeAddress = 0;
        private byte slaveAddress = 0;
        private List<byte> remainData = new List<byte>();
        private int headerIndex = -1;
        private int msgLength = -1;

        internal FinsTcpTransport(IStreamResource streamResource)
            : base(streamResource)
        {
            Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");

            tcpNodeAddrReq[0] = (byte)'F';
            tcpNodeAddrReq[1] = (byte)'I';
            tcpNodeAddrReq[2] = (byte)'N';
            tcpNodeAddrReq[3] = (byte)'S';
            tcpNodeAddrReq[4] = 0x00;
            tcpNodeAddrReq[5] = 0x00;
            tcpNodeAddrReq[6] = 0x00;
            tcpNodeAddrReq[7] = 0x0C;
            tcpNodeAddrReq[8] = 0x00;
            tcpNodeAddrReq[9] = 0x00;
            tcpNodeAddrReq[10] = 0x00;
            tcpNodeAddrReq[11] = 0x00;
            tcpNodeAddrReq[12] = 0x00;
            tcpNodeAddrReq[13] = 0x00;
            tcpNodeAddrReq[14] = 0x00;
            tcpNodeAddrReq[15] = 0x00;
            tcpNodeAddrReq[16] = 0x00;
            tcpNodeAddrReq[17] = 0x00;
            tcpNodeAddrReq[18] = 0x00;
            tcpNodeAddrReq[19] = 0x00;
            StreamResource.Write(tcpNodeAddrReq, 0, tcpNodeAddrReq.Length);
            byte[] tcpNodeAddrRsp = ReadRequestResponse(streamResource);
            nodeAddress = tcpNodeAddrRsp[3];
            slaveAddress = tcpNodeAddrRsp[7];

            tcpFinsHeader[0] = (byte)'F';/* Header */
            tcpFinsHeader[1] = (byte)'I';
            tcpFinsHeader[2] = (byte)'N';
            tcpFinsHeader[3] = (byte)'S';
            tcpFinsHeader[4] = 0x00;/* Length */
            tcpFinsHeader[5] = 0x00;
            tcpFinsHeader[6] = 0x00;
            tcpFinsHeader[7] = 0x00;/*Length of data from Command up to end of FINS frame */
            tcpFinsHeader[8] = 0x00;/* Command */
            tcpFinsHeader[9] = 0x00;
            tcpFinsHeader[10] = 0x00;
            tcpFinsHeader[11] = 0x02;
            tcpFinsHeader[12] = 0x00;/* Error Code */
            tcpFinsHeader[13] = 0x00;
            tcpFinsHeader[14] = 0x00;
            tcpFinsHeader[15] = 0x00;
        }

        internal byte[] ReadRequestResponse(IStreamResource streamResource)
        {
            // read header
            byte[] buffer = new byte[10240];
            int count = 0;
            while (true)
            {
                count = streamResource.Read(buffer, 0, buffer.Length);

                if (count == 0)
                    throw new IOException("Read resulted in 0 bytes returned.");
                byte[] data = new byte[count];
                Array.Copy(buffer, data, count);
                remainData.AddRange(data);
                if (headerIndex < 0)
                {
                    for (int i = 0; i < remainData.Count - 3; i++)
                    {
                        if ((remainData[i] == (byte)'F') &&
                            (remainData[i + 1] == (byte)'I') &&
                            (remainData[i + 2] == (byte)'N') &&
                            (remainData[i + 3] == (byte)'S'))
                        {
                            headerIndex = i;
                            remainData.RemoveRange(0, headerIndex);
                            break;
                        }
                    }
                }

                if ((headerIndex >= 0) && (msgLength < 0) && (remainData.Count > 8))
                    msgLength = 8 + remainData[4] * 16777216 + remainData[5] * 65536 + remainData[6] * 256 + remainData[7];

                if ((msgLength > 0) && (remainData.Count >= msgLength))
                    break;
            }

            byte[] fins = null;
            try
            {
                fins = new byte[msgLength - 16];
                for (int i = 16; i < msgLength; i++)
                    fins[i - 16] = remainData[i];
            }
            finally
            {
                remainData.RemoveRange(0, msgLength);
                msgLength = -1;
                headerIndex = -1;
            }

            return fins;
        }

        /// <summary>
        /// Create a new transaction ID.
        /// </summary>
        internal virtual ushort GetNewTransactionId()
        {
            lock (_transactionIdLock)
                _transactionId = _transactionId == UInt16.MaxValue ? (ushort)1 : ++_transactionId;

            return _transactionId;
        }

        internal override byte[] BuildMessageFrame(IFinsMessage message)
        {
            byte[] bs = BitConverter.GetBytes(message.ProtocolDataUnit.Length + 8);
            tcpFinsHeader[4] = bs[3];
            tcpFinsHeader[5] = bs[2];
            tcpFinsHeader[6] = bs[1];
            tcpFinsHeader[7] = bs[0];

            List<byte> messageBody = new List<byte>();
            messageBody.AddRange(tcpFinsHeader);
            message.SlaveAddress = slaveAddress;
            message.NetNodeNo = nodeAddress;
            messageBody.AddRange(message.ProtocolDataUnit);
            return messageBody.ToArray();
        }

        internal override void Write(IFinsMessage message)
        {
            message.TransactionId = GetNewTransactionId();
            byte[] frame = BuildMessageFrame(message);
            StreamResource.Write(frame, 0, frame.Length);
        }

        internal override byte[] ReadRequest()
        {
            return ReadRequestResponse(StreamResource);
        }

        internal override IFinsMessage ReadResponse<T>()
        {
            return base.CreateResponse<T>(ReadRequestResponse(StreamResource));
        }

        internal override void OnValidateResponse(IFinsMessage request, IFinsMessage response)
        {
            //if (request.TransactionId != response.TransactionId)
            //    throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response was not of expected transaction ID. Expected {0}, received {1}.", request.TransactionId, response.TransactionId));
        }
    }
}
