using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Fins.Message;

namespace Fins.IO
{
	/// <summary>
	/// Fins transport.
	/// Abstraction - http://en.wikipedia.org/wiki/Bridge_Pattern
	/// </summary>
	public abstract class FinsTransport : IDisposable
	{
		private object _syncLock = new object();
		private int _retries = Fins.DefaultRetries;
		private int _waitToRetryMilliseconds = Fins.DefaultWaitToRetryMilliseconds;
		private IStreamResource _streamResource;

		/// <summary>
		/// This constructor is called by the NullTransport.
		/// </summary>
		internal FinsTransport()
		{
		}

		internal FinsTransport(IStreamResource streamResource)
		{
			Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");

			_streamResource = streamResource;
		}

		/// <summary>
		/// Number of times to retry sending message after encountering a failure such as an IOException, 
		/// TimeoutException, or a corrupt message.
		/// </summary>
		public int Retries
		{
			get { return _retries; }
			set { _retries = value; }
		}

		/// <summary>
		/// Gets or sets the number of milliseconds the tranport will wait before retrying a message after receiving 
		/// an ACKNOWLEGE or SLAVE DEVICE BUSY slave exception response.
		/// </summary>
		public int WaitToRetryMilliseconds
		{
			get
			{
				return _waitToRetryMilliseconds;
			}
			set
			{
				if (value < 0)
					throw new ArgumentException(Resources.WaitRetryGreaterThanZero);

				_waitToRetryMilliseconds = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of milliseconds before a timeout occurs when a read operation does not finish.
		/// </summary>	
		public int ReadTimeout
		{
			get
			{
				return StreamResource.ReadTimeout;
			}
			set
			{
				StreamResource.ReadTimeout = value;
			}
		}

		/// <summary>
		/// Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
		/// </summary>
		public int WriteTimeout
		{
			get
			{
				return StreamResource.WriteTimeout;
			}
			set
			{
				StreamResource.WriteTimeout = value;
			}
		}

		/// <summary>
		/// Gets the stream resource.
		/// </summary>
		internal IStreamResource StreamResource
		{
			get
			{
				return _streamResource;
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		internal virtual T UnicastMessage<T>(IFinsMessage message) where T : IFinsMessage, new()
		{
			IFinsMessage response = null;
			int attempt = 1;
			bool readAgain;
			bool success = false;

			do
			{
				try
				{
					lock (_syncLock)
					{
						Write(message);

						do
						{
							readAgain = false;
							response = ReadResponse<T>();

							SlaveExceptionResponse exceptionResponse = response as SlaveExceptionResponse;
							if (exceptionResponse != null)
							{
								// if SlaveExceptionCode == ACKNOWLEDGE we retry reading the response without resubmitting request
								if (readAgain = exceptionResponse.ExceptionCode.Value == Fins.Acknowledge)
								{
									//_logger.InfoFormat("Received ACKNOWLEDGE slave exception response, waiting {0} milliseconds and retrying to read response.", _waitToRetryMilliseconds);
									Thread.Sleep(WaitToRetryMilliseconds);
								}
								else
								{
									throw new SlaveException(exceptionResponse);
								}
							}
						} while (readAgain);
					}

					ValidateResponse(message, response);
					success = true;
				}
				catch (SlaveException se)
				{
					if (se.SlaveExceptionCode != Fins.SlaveDeviceBusy)
						throw;

					//_logger.InfoFormat("Received SLAVE_DEVICE_BUSY exception response, waiting {0} milliseconds and resubmitting request.", _waitToRetryMilliseconds);
					Thread.Sleep(WaitToRetryMilliseconds);
				}
				catch (Exception e)
				{
					if (e is FormatException ||
						e is NotImplementedException ||
						e is TimeoutException ||
						e is IOException)
					{
						//_logger.WarnFormat("{0}, {1} retries remaining - {2}", e.GetType().Name, _retries - attempt + 1, e);

						if (attempt++ > _retries)
							throw;
					}
					else
					{
						throw;
					}
				}
			} while (!success);

			return (T) response;
		}

		internal virtual IFinsMessage CreateResponse<T>(byte[] frame) where T : IFinsMessage, new()
		{
			//byte functionCode = frame[1];
			IFinsMessage response;

			// check for slave exception response else create message from frame
            //if (functionCode > Fins.ExceptionOffset)
            //    response = FinsMessageFactory.CreateFinsMessage<SlaveExceptionResponse>(frame);
            //else				
				response = FinsMessage.CreateFinsMessage<T>(frame);

			return response;
		}

		internal void ValidateResponse(IFinsMessage request, IFinsMessage response)
		{
			// always check the function code and slave address, regardless of transport protocol
			//if (request.FunctionCode != response.FunctionCode)
			//	throw new IOException(String.Format(CultureInfo.InvariantCulture, "Received response with unexpected Function Code. Expected {0}, received {1}.", request.FunctionCode, response.FunctionCode));

			if (request.SlaveAddress != response.NetNodeNo)
				throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response slave address does not match request. Expected {0}, received {1}.", response.NetNodeNo, request.SlaveAddress));

			// message specific validation
			IFinsRequest FinsRequest = request as IFinsRequest;
			if (FinsRequest != null)
				FinsRequest.ValidateResponse(response);

			OnValidateResponse(request, response);
		}

		/// <summary>
		/// Provide hook to do transport level message validation.
		/// </summary>
		internal abstract void OnValidateResponse(IFinsMessage request, IFinsMessage response);

		internal abstract byte[] ReadRequest();

		internal abstract IFinsMessage ReadResponse<T>() where T : IFinsMessage, new();

		internal abstract byte[] BuildMessageFrame(IFinsMessage message);

		internal abstract void Write(IFinsMessage message);

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_streamResource != null)
				{
					_streamResource.Dispose();
					_streamResource = null;
				}
			}
		}
	}
}
