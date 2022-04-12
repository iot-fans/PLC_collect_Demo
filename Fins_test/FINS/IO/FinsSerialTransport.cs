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
	internal class FinsSerialTransport : FinsTransport
	{
		private static readonly object _transactionIdLock = new object();
		private ushort _transactionId;
        private byte[] serialHeader = ASCIIEncoding.ASCII.GetBytes("@00FAF");
        private string remainData = "";

        internal FinsSerialTransport(IStreamResource streamResource)
			: base(streamResource)
		{
			Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");
		}

        internal byte[] ReadRequestResponse(IStreamResource streamResource)
        {
            // read header
            byte[] buffer = new byte[256];
            int count = 0;
            string rawData = null;
            int noDataTimes = 0;
            while (true)
            {
                count = streamResource.Read(buffer, 0, 256);

                if (noDataTimes > 16)
                {
                    throw new IOException("Read resulted in 0 bytes returned.");
                }
                if (count == 0)
                {
                    noDataTimes++;
                    System.Threading.Thread.Sleep(10);
                    continue;
                }
                else
                {
                    noDataTimes = 0;
                }

                remainData += ASCIIEncoding.ASCII.GetString(buffer, 0, count);
                int tailIndex = remainData.IndexOf("*\r");
                if (tailIndex >= 0)
                {
                    rawData = remainData.Substring(0, tailIndex);
                    int prevIndex = tailIndex;
                    tailIndex = remainData.LastIndexOf("*\r");
                    //if (prevIndex != tailIndex)
                    //    throw new Exception("More than 1 message income.");
                    remainData = remainData.Substring(tailIndex + 2);
                    break;
                }
            }

            if (rawData.Length < 3) return null;

            string fcs = FinsUtility.AsciiFCS(rawData, rawData.Length - 2);
            if (fcs != rawData.Substring(rawData.Length - 2, 2))
                throw new Exception("FCS is incorrect.");

            rawData = rawData.Remove(rawData.Length - 2, 2);
            int index = rawData.IndexOf("@");
            if (index < 0) return null;
            if (index + 6 >= rawData.Length) return null;
            rawData = rawData.Remove(0, index + 6 + 1);

            return FinsUtility.HexToBytes(rawData);
        }

		/// <summary>
		/// Create a new transaction ID.
		/// </summary>
		internal virtual ushort GetNewTransactionId()
		{
			lock (_transactionIdLock)
				_transactionId = _transactionId == UInt16.MaxValue ? (ushort) 1 : ++_transactionId;

			return _transactionId;
		}

        private byte[] ToAsciiArray(byte[] array)
        {
            byte[] asciiArray = new byte[array.Length * 2];
            for (int i = 0; i < array.Length; i++)
            {
                string ascii = array[i].ToString("X2");
                asciiArray[i * 2] = (byte)ascii[0];
                asciiArray[i * 2 + 1] = (byte)ascii[1];
            }
            return asciiArray;
        }

        internal override byte[] BuildMessageFrame(IFinsMessage message)
		{
			List<byte> messageBody = new List<byte>();
            messageBody.AddRange(serialHeader);
            byte[] ProtocalData = ToAsciiArray(message.ProtocolDataUnit);
            messageBody.AddRange(ProtocalData);
            messageBody.AddRange(FinsUtility.AsciiFCS(serialHeader, ProtocalData));
            messageBody.AddRange(ASCIIEncoding.ASCII.GetBytes("*\r"));

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
            if (request.ProtocolDataUnit[9] != response.ProtocolDataUnit[9])
            {
                throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response was not of expected transaction ID. Expected {0}, received {1}.", request.ProtocolDataUnit[9], response.ProtocolDataUnit[9]));
            }
            //if (request.TransactionId != response.TransactionId)
            //    throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response was not of expected transaction ID. Expected {0}, received {1}.", request.TransactionId, response.TransactionId));
        }
    }
}
