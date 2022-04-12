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
    internal class FinsUdpTransport : FinsTransport
    {
        private static readonly object _transactionIdLock = new object();
        private ushort _transactionId;

        internal FinsUdpTransport(IStreamResource streamResource)
            : base(streamResource)
        {
            Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");
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

                if (count > 0)
                    break;
            }

            byte[] fins = new byte[count];
            Array.Copy(buffer, fins, count);

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
            return message.ProtocolDataUnit;
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
