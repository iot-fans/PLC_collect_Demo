using Fins.Data;

namespace Fins.Message
{
	abstract class FinsMessageWithData<TData> : FinsMessage where TData : IFinsMessageDataCollection
	{
		public FinsMessageWithData()
		{
		}

		public FinsMessageWithData(byte slaveAddress, byte functionCode)
			: base(slaveAddress, functionCode)
		{
		}

        public new TData Data { get; set; }
	}
}
