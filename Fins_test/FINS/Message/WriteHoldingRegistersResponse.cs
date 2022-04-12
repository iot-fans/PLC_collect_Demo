using System;
using System.Globalization;
using System.Net;

namespace Fins.Message
{
	class WriteHoldingRegistersResponse : FinsMessage, IFinsMessage
	{
		public WriteHoldingRegistersResponse()
		{
		}

		public WriteHoldingRegistersResponse(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
			: base(slaveAddress, Fins.WriteMultipleRegisters)
		{
			StartAddress = startAddress;
			NumberOfPoints = numberOfPoints;
		}

		protected override void InitializeUnique(byte[] frame)
		{

		}

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "Wrote {0} holding registers starting at address {1}.", NumberOfPoints, StartAddress);
        }
    }
}
