using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Fins.Data;
using Fins.IO;
using Fins.Message;
using Fins.Utility;

namespace Fins.Device
{
	/// <summary>
	/// Fins master device.
	/// </summary>
	public abstract class FinsMaster : FinsDevice
	{
		internal FinsMaster(FinsTransport transport)
			: base(transport)
		{
		}

		/// <summary>
		/// Read from 1 to 2000 contiguous coils status.
		/// </summary>
		/// <param name="slaveAddress">Address of device to read values from.</param>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of coils to read.</param>
		/// <returns>Coils status</returns>
		public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
		{
			ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 2000);

			return ReadDiscretes(Fins.ReadCoils, slaveAddress, startAddress, numberOfPoints);
		}

		/// <summary>
		/// Read from 1 to 2000 contiguous discrete input status.
		/// </summary>
		/// <param name="slaveAddress">Address of device to read values from.</param>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of discrete inputs to read.</param>
		/// <returns>Discrete inputs status</returns>
		public bool[] ReadInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
		{
			ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 2000);

			return ReadDiscretes(Fins.ReadInputs, slaveAddress, startAddress, numberOfPoints);
		}		

		/// <summary>
		/// Read contiguous block of holding registers.
		/// </summary>
		/// <param name="slaveAddress">Address of device to read values from.</param>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of holding registers to read.</param>
		/// <returns>Holding registers status</returns>
		public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
		{
			ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 125);

			return ReadRegisters(Fins.ReadHoldingRegisters, slaveAddress, startAddress, numberOfPoints);
		}

		/// <summary>
		/// Read contiguous block of input registers.
		/// </summary>
		/// <param name="slaveAddress">Address of device to read values from.</param>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of holding registers to read.</param>
		/// <returns>Input registers status</returns>
		public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
		{
			ValidateNumberOfPoints("numberOfPoints", numberOfPoints, 125);

			return ReadRegisters(Fins.ReadInputRegisters, slaveAddress, startAddress, numberOfPoints);
		}

		/// <summary>
		/// Write a single coil value.
		/// </summary>
		/// <param name="slaveAddress">Address of the device to write to.</param>
		/// <param name="coilAddress">Address to write value to.</param>
		/// <param name="value">Value to write.</param>
		public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
		{
			WriteSingleCoilRequestResponse request = new WriteSingleCoilRequestResponse(slaveAddress, coilAddress, value);
			Transport.UnicastMessage<WriteSingleCoilRequestResponse>(request);
		}

		/// <summary>
		/// Write a single holding register.
		/// </summary>
		/// <param name="slaveAddress">Address of the device to write to.</param>
		/// <param name="registerAddress">Value to write.</param>
		/// <param name="value">Value to write.</param>
		public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
		{
			WriteSingleRegisterRequestResponse request = new WriteSingleRegisterRequestResponse(slaveAddress, registerAddress, value);
			Transport.UnicastMessage<WriteSingleRegisterRequestResponse>(request);
		}

		/// <summary>
		/// Write a block of 1 to 123 contiguous registers.
		/// </summary>
		/// <param name="slaveAddress">Address of the device to write to.</param>
		/// <param name="startAddress">Address to begin writing values.</param>
		/// <param name="data">Values to write.</param>
		public void WriteHoldingRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
		{
			ValidateData("data", data, 123);

			WriteHoldingRegistersRequest request = new WriteHoldingRegistersRequest(slaveAddress, startAddress, new RegisterCollection(data));
			Transport.UnicastMessage<WriteHoldingRegistersResponse>(request);
		}	

		/// <summary>
		/// Force each coil in a sequence of coils to a provided value.
		/// </summary>
		/// <param name="slaveAddress">Address of the device to write to.</param>
		/// <param name="startAddress">Address to begin writing values.</param>
		/// <param name="data">Values to write.</param>
		public void WriteCoils(byte slaveAddress, ushort startAddress, bool[] data)
		{
			ValidateData("data", data, 1968);

			WriteCoilsRequest request = new WriteCoilsRequest(slaveAddress, startAddress, new DiscreteCollection(data));
			Transport.UnicastMessage<WriteCoilsResponse>(request);
		}

		/// <summary>
		/// Executes the custom message.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response.</typeparam>
		/// <param name="request">The request.</param>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[SuppressMessage("Microsoft.Usage", "CA2223:MembersShouldDifferByMoreThanReturnType")]
		public TResponse ExecuteCustomMessage<TResponse>(IFinsMessage request) where TResponse : IFinsMessage, new()
		{
			return Transport.UnicastMessage<TResponse>(request);
		}

		internal static void ValidateData<T>(string argumentName, T[] data, int maxDataLength)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			if (data.Length == 0 || data.Length > maxDataLength)
			{
				throw new ArgumentException(String.Format(CultureInfo.InvariantCulture,
					"The length of argument {0} must be between 1 and {1} inclusive.", argumentName, maxDataLength));
			}
		}

		internal static void ValidateNumberOfPoints(string argumentName, ushort numberOfPoints, ushort maxNumberOfPoints)
		{
			if (numberOfPoints < 1 || numberOfPoints > maxNumberOfPoints)
			{
				throw new ArgumentException(String.Format(CultureInfo.InvariantCulture,
					"Argument {0} must be between 1 and {1} inclusive.", argumentName, maxNumberOfPoints));
			}
		}

		internal ushort[] ReadRegisters(byte functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
		{
			ReadHoldingRegistersRequest request = new ReadHoldingRegistersRequest(functionCode, slaveAddress, startAddress, numberOfPoints);
			ReadHoldingRegistersResponse response = Transport.UnicastMessage<ReadHoldingRegistersResponse>(request);

			return CollectionUtility.ToArray(response.Data);
		}

		internal bool[] ReadDiscretes(byte functionCode, byte slaveAddress, ushort startAddress, ushort numberOfPoints)
		{
			ReadCoilsInputsRequest request = new ReadCoilsInputsRequest(functionCode, slaveAddress, startAddress, numberOfPoints);
			ReadCoilsInputsResponse response = Transport.UnicastMessage<ReadCoilsInputsResponse>(request);

			return CollectionUtility.Slice(response.Data, 0, request.NumberOfPoints);
		}
	}
}
