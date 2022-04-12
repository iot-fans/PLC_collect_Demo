using System;
using System.Collections.Generic;
using System.Globalization;
using Fins.Data;
using Fins.Utility;

namespace Fins.Message
{
	abstract class FinsMessage
	{
        public FinsMessage()
        {
            DoInit();
        }

		public FinsMessage(byte slaveAddress, byte functionCode)
		{
			SlaveAddress = slaveAddress;
            DoInit();
		}

        public static T CreateFinsMessage<T>(byte[] frame) where T : IFinsMessage, new()
        {
            IFinsMessage message = new T();
            message.Initialize(frame);

            return (T)message;
        }

        private byte[] finsHeader = new byte[12];
        private void DoInit()
        {
            finsHeader[0] = 0x80;//ICF
            finsHeader[1] = 0x00;//RSV
            finsHeader[2] = 0x02;//GCT
            finsHeader[3] = 0x00;//DNA
            finsHeader[4] = 0x00;//DA1
            finsHeader[5] = 0x00;//DA2
            finsHeader[6] = 0x00;//SNA
            finsHeader[7] = 0x00;//SA1
            finsHeader[8] = 0x00;//SA2
            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            int SID = ra.Next(1, 100);//generate random sid in orde to check response packet
            finsHeader[9] = Convert.ToByte(SID.ToString(), 16);//SID
            finsHeader[10] = 0x00;//Command Code 1
            finsHeader[11] = 0x00;//Command Code 2

            Init();
        }

        internal virtual void Init()
        {

        }

        internal byte[] CommandData { get; set; }

        public byte SlaveAddress
        {
            get { return finsHeader[4]; }
            set { finsHeader[4] = value; }
        }

        public byte NetNodeNo
        {
            get { return finsHeader[7]; }
            set { finsHeader[7] = value; }
        }

        public byte SID
        {
            get { return finsHeader[9]; }//SID
        }

        internal byte CommandCode1
        {
            get { return finsHeader[10]; }
            set { finsHeader[10] = value; }
        }

        internal byte CommandCode2
        {
            get { return finsHeader[11]; }
            set { finsHeader[11] = value; }
        }

        public virtual ushort NumberOfPoints { get; set; }

        public virtual ushort StartAddress { get; set; }

        public byte ByteCount { get; set; }

        public byte? ExceptionCode { get; set; }

		public ushort TransactionId { get; set; }
		
		public IFinsMessageDataCollection Data { get; set; }

		public virtual byte[] ProtocolDataUnit
		{
			get
			{
                BeforeMakeProtocalDataUnit();

                List<byte> pdu = new List<byte>();

                pdu.AddRange(finsHeader);

                if (CommandData != null)
                    pdu.AddRange(CommandData);

				return pdu.ToArray();
			}
		}

        internal virtual void BeforeMakeProtocalDataUnit()
        {

        }

		public void Initialize(byte[] frame)
		{
            if (frame == null)
                throw new ArgumentNullException("frame", "Argument frame cannot be null.");

            for (int i = 0; i < finsHeader.Length; i++)
            {
                if (i >= frame.Length) break;
                finsHeader[i] = frame[i];
            }

            if (frame.Length > 12 + 2)
            {
                InitializeUnique(CollectionUtility.Slice(frame, 12 + 2, frame.Length - 12 - 2));
            }
		}
     
		protected abstract void InitializeUnique(byte[] data);

	}
}
