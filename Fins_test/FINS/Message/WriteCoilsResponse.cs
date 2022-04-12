using System;
using System.Globalization;
using System.Net;

namespace Fins.Message
{
	class WriteCoilsResponse : FinsMessage, IFinsMessage
	{
		private const int _minimumFrameSize = 6;

		public WriteCoilsResponse()
		{
		}

		public WriteCoilsResponse(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
			: base(slaveAddress, Fins.WriteMultipleCoils)
		{
			StartAddress = startAddress;
			NumberOfPoints = numberOfPoints;
		}

		protected override void InitializeUnique(byte[] frame)
		{

        }

        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "Wrote {0} coils starting at address {1}.", NumberOfPoints, StartAddress);
        }
    }
}
