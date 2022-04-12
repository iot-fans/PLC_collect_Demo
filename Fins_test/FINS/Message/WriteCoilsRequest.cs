using System;
using System.Globalization;
using System.IO;
using System.Net;
using Fins.Data;
using Fins.Utility;

namespace Fins.Message
{
	class WriteCoilsRequest : FinsMessageWithData<DiscreteCollection>, IFinsRequest
	{
		public WriteCoilsRequest()
		{
		}

		public WriteCoilsRequest(byte slaveAddress, ushort startAddress, DiscreteCollection data)
			: base(slaveAddress, Fins.WriteMultipleCoils)
		{
			StartAddress = startAddress;
			NumberOfPoints = (ushort) data.Count;
			ByteCount = (byte) ((data.Count + 7) / 8);
			Data = data;
		}

        byte[] commandBody = new byte[6];
        internal override void Init()
        {
            CommandCode1 = 0x01;//Write
            CommandCode2 = 0x02;//Write
            commandBody[0] = 0x80;//CIO,LR,HR,AR
            commandBody[3] = 0x00;
        }

        public override ushort StartAddress
        {
            get
            {
                return (ushort)((commandBody[1] * 256 + commandBody[2]) * 16 + commandBody[3]);
            }
            set
            {
                ushort channel = (ushort)(value / 16);
                byte bit = (byte)(value % 16);
                byte[] bs = BitConverter.GetBytes(channel);
                commandBody[1] = bs[1];
                commandBody[2] = bs[0];
                commandBody[3] = bit;
            }
        }

        public override ushort NumberOfPoints
        {
            get
            {
                return (ushort)(commandBody[4] * 256 + commandBody[5]);
            }
            set
            {
                byte[] bs = BitConverter.GetBytes(value);
                commandBody[4] = bs[1];
                commandBody[5] = bs[0];
            }
        }

        internal override void BeforeMakeProtocalDataUnit()
        {
            if (Data == null)
            {
                CommandData = commandBody;
                return;
            }
            CommandData = new byte[6 + Data.Count * 2];
            commandBody.CopyTo(CommandData, 0);
            for (int i = 0, j = 0; i < Data.Count; i++)
            {
                j = 6 + i * 2;
                byte[] bs = BitConverter.GetBytes(Data[i]);
                CommandData[j] = bs[0];
                CommandData[j + 1] = 0;
            }
        }

        public void ValidateResponse(IFinsMessage response)
        {

        }

		protected override void InitializeUnique(byte[] frame)
		{

        }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "Write {0} coils starting at address {1}.", NumberOfPoints, StartAddress);
        }
    }
}
