using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using Fins.Data;
using Fins.Utility;

namespace Fins.Message
{
    class WriteSingleCoilRequestResponse : FinsMessageWithData<RegisterCollection>, IFinsRequest
	{
		public WriteSingleCoilRequestResponse()
		{
		}

		public WriteSingleCoilRequestResponse(byte slaveAddress, ushort startAddress, bool coilState)
			: base(slaveAddress, Fins.WriteSingleCoil)
		{
			StartAddress = startAddress;
            Data = new RegisterCollection(coilState ? Fins.CoilOn : Fins.CoilOff);
		}

        byte[] commandBody = new byte[8];
        internal override void Init()
        {
            CommandCode1 = 0x01;//Write
            CommandCode2 = 0x02;//Write
            commandBody[0] = 0x80;//CIO,LR,HR,AR
            commandBody[3] = 0x00;
            NumberOfPoints = 1;
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
            byte[] bs = BitConverter.GetBytes(Data[0]);
            commandBody[6] = bs[0];
            commandBody[7] = bs[1];
            CommandData = commandBody;
        }

        public void ValidateResponse(IFinsMessage response)
        {

        }

		protected override void InitializeUnique(byte[] frame)
		{

        }

        public override string ToString()
        {
            Debug.Assert(Data != null, "Argument Data cannot be null.");
            Debug.Assert(Data.Count == 1, "Data should have a count of 1.");

            return String.Format(CultureInfo.InvariantCulture, "Write single coil {0} at address {1}.",
                Data[0] == Fins.CoilOn ? 1 : 0, StartAddress);
        }
    }
}
