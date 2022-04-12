using System;
using System.Globalization;
using Fins.Data;
using Fins.Utility;

namespace Fins.Message
{	
	class ReadCoilsInputsResponse : FinsMessageWithData<DiscreteCollection>, IFinsMessage
	{
		private const int _minimumFrameSize = 3;

		public ReadCoilsInputsResponse()
		{
		}

		public ReadCoilsInputsResponse(byte functionCode, byte slaveAddress, byte byteCount, DiscreteCollection data)
			: base(slaveAddress, functionCode)
		{
            if (data == null)
                throw new ArgumentNullException("data");

            ByteCount = byteCount;
			Data = data;
		}

        protected override void InitializeUnique(byte[] data)
		{
            if (data == null) return;
            Data = new DiscreteCollection(data);
		}
	}
}
