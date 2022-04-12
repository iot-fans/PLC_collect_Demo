using System.Diagnostics.CodeAnalysis;

namespace Fins.Message
{
	/// <summary>
	/// A message built by the master (client) that initiates a Fins transaction.
	/// </summary>
	public interface IFinsMessage
	{
		/// <summary>
		/// Address of the slave (server).
		/// </summary>
		byte SlaveAddress { get; set; }

        /// <summary>
        /// Address of the master (client).
        /// </summary>
        byte NetNodeNo { get; set; }

        byte SID { get; }

        /// <summary>
		/// Composition of the function code and message data.
		/// </summary>
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		byte[] ProtocolDataUnit { get; }

		/// <summary>
		/// A unique identifier assigned to a message when using the IP protocol.
		/// </summary>
		ushort TransactionId { get; set; }

		/// <summary>
		/// Initializes a Fins message from the specified message frame.
		/// </summary>
		/// <param name="frame">The frame.</param>
		void Initialize(byte[] frame);
	}
}
