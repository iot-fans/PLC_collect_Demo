using System;
using System.Globalization;
using Fins.Data;
using Fins.Utility;

namespace Fins.Message
{
	class ReadHoldingRegistersResponse : FinsMessageWithData<RegisterCollection>, IFinsMessage
	{
		public ReadHoldingRegistersResponse()
		{
		}

		public ReadHoldingRegistersResponse(byte functionCode, byte slaveAddress, RegisterCollection data)
			: base(slaveAddress, functionCode)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			ByteCount = data.ByteCount;
			Data = data;
		}

		protected override void InitializeUnique(byte[] data)
		{
            if (data == null) return;
            Data = new RegisterCollection(data);
        }
	}
}
