using System;
using System.Globalization;
using System.IO;
using System.Net;

namespace Fins.Message
{
	class ReadCoilsInputsRequest : FinsMessage, IFinsRequest
	{
		private const int _minimumFrameSize = 6;

		public ReadCoilsInputsRequest()
		{
		}

		public ReadCoilsInputsRequest(byte functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
			: base(slaveAddress, functionCode)
		{
			StartAddress = startAddress;
			NumberOfPoints = numberOfPoints;
		}

        byte[] commandBody = new byte[6];
        internal override void Init()
        {
            CommandCode1 = 0x01;//Read
            CommandCode2 = 0x01;//Read
            commandBody[0] = 0x80;//CIO
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
            //get
            //{
            //    return (ushort)(commandBody[1] * 256 + commandBody[2]);
            //}
            //set
            //{
            //    byte[] bs = BitConverter.GetBytes(value);
            //    commandBody[1] = bs[1];
            //    commandBody[2] = bs[0];
            //}
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
            CommandData = commandBody;
        }

        public void ValidateResponse(IFinsMessage response)
        {

        }

		protected override void InitializeUnique(byte[] frame)
		{

        }        
    }
}
