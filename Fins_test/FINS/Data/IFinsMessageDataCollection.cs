using System.Diagnostics.CodeAnalysis;

namespace Fins.Data
{
	/// <summary>
	/// Fins message containing data.
	/// </summary>	
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
	public interface IFinsMessageDataCollection	
	{
		/// <summary>
		/// Gets the network bytes.
		/// </summary>
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		byte[] NetworkBytes { get; }

		/// <summary>
		/// Gets the byte count.
		/// </summary>
		byte ByteCount { get; }
	}
}
