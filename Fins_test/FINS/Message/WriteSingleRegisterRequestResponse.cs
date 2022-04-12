using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using Fins.Data;

namespace Fins.Message
{
	class WriteSingleRegisterRequestResponse : FinsMessageWithData<RegisterCollection>, IFinsRequest
	{
		private const int _minimumFrameSize = 6;

		public WriteSingleRegisterRequestResponse()
		{
		}

		public WriteSingleRegisterRequestResponse(byte slaveAddress, ushort startAddress, ushort registerValue)
			: base(slaveAddress, Fins.WriteSingleRegister)
		{
			StartAddress = startAddress;
			Data = new RegisterCollection(registerValue);
		}

        byte[] commandBody = new byte[8];
        internal override void Init()
        {
            CommandCode1 = 0x01;//Write
            CommandCode2 = 0x02;//Write
            commandBody[0] = 0x82;//DM
            commandBody[3] = 0x00;
            NumberOfPoints = 1;
        }

        public override ushort StartAddress
        {
            get
            {
                return (ushort)(commandBody[1] * 256 + commandBody[2]);
            }
            set
            {
                byte[] bs = BitConverter.GetBytes(value);
                commandBody[1] = bs[1];
                commandBody[2] = bs[0];
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
            commandBody[6] = bs[1];
            commandBody[7] = bs[0];
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

            return String.Format(CultureInfo.InvariantCulture, "Write single holding register {0} at address {1}.", Data[0], StartAddress);
        }
    }
}
