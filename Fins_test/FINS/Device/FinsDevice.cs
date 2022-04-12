using System;
using Fins.IO;

namespace Fins.Device
{
	/// <summary>
	/// Fins device.
	/// </summary>
	public abstract class FinsDevice : IDisposable
	{
		private FinsTransport _transport;

		internal FinsDevice(FinsTransport transport)
		{
			_transport = transport;
		}

		/// <summary>
		/// Gets the Fins Transport.
		/// </summary>
		/// <value>The transport.</value>
		public FinsTransport Transport
		{
			get 
			{ 
				return _transport; 
			}
		}

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transport != null)
                {
                    _transport.Dispose();
                    _transport = null;
                }
            }
        }
	}
}
