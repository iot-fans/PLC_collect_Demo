using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using Fins.Utility;

namespace Fins.Data
{
	/// <summary>
	/// Collection of 16 bit registers.
	/// </summary>
	public class RegisterCollection : Collection<ushort>, IFinsMessageDataCollection
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterCollection"/> class.
		/// </summary>
		public RegisterCollection()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterCollection"/> class.
		/// </summary>
		public RegisterCollection(byte[] bytes)
			: this((IList<ushort>) FinsUtility.NetworkBytesToHostUInt16(bytes))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterCollection"/> class.
		/// </summary>
		public RegisterCollection(params ushort[] registers)
			: this((IList<ushort>) registers)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterCollection"/> class.
		/// </summary>
		public RegisterCollection(IList<ushort> registers)
			: base(registers.IsReadOnly ? new List<ushort>(registers) : registers)
		{
		}

		/// <summary>
		/// Gets the network bytes.
		/// </summary>
		public byte[] NetworkBytes
		{
			get
			{
				List<byte> bytes = new List<byte>();

				foreach (ushort register in this)
					bytes.AddRange(BitConverter.GetBytes((ushort) IPAddress.HostToNetworkOrder((short) register)));

				return bytes.ToArray();
			}
		}

		/// <summary>
		/// Gets the byte count.
		/// </summary>
		public byte ByteCount
		{
			get 
			{ 
				return (byte) (Count * 2);
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
            return String.Concat("{", String.Join(", ", CollectionUtility.ToArray(SequenceUtility.ToList(this, delegate(ushort v) { return v.ToString(); }))), "}");
		}	}
}
