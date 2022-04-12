using System;
using System.Collections.Generic;
using System.Globalization;

namespace Fins.Message
{
	class SlaveExceptionResponse : FinsMessage, IFinsMessage
	{
		private static readonly Dictionary<byte, string> _exceptionMessages = CreateExceptionMessages();
		private const int _minimumFrameSize = 3;

		public SlaveExceptionResponse()
		{
		}

		public SlaveExceptionResponse(byte slaveAddress, byte functionCode, byte exceptionCode)
			: base(slaveAddress, functionCode)	
		{
			ExceptionCode = exceptionCode;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString()
		{
			string message = _exceptionMessages.ContainsKey(ExceptionCode.Value) ? _exceptionMessages[ExceptionCode.Value] : Resources.Unknown;
			return String.Format(CultureInfo.InvariantCulture, Resources.SlaveExceptionResponseFormat, Environment.NewLine, ExceptionCode.Value, message);
		}

		internal static Dictionary<byte, string> CreateExceptionMessages()
		{
			Dictionary<byte, string> messages = new Dictionary<byte, string>(9);

			messages.Add(1, Resources.IllegalFunction);
			messages.Add(2, Resources.IllegalDataAddress);
			messages.Add(3, Resources.IllegalDataValue);
			messages.Add(4, Resources.SlaveDeviceFailure);
			messages.Add(5, Resources.Acknowlege);
			messages.Add(6, Resources.SlaveDeviceBusy);
			messages.Add(8, Resources.MemoryParityError);
			messages.Add(10, Resources.GatewayPathUnavailable);
			messages.Add(11, Resources.GatewayTargetDeviceFailedToRespond);

			return messages;
		}

		protected override void InitializeUnique(byte[] frame)
		{
			ExceptionCode = frame[2];
		}
	}
}
