using System;
using System.Diagnostics.CodeAnalysis;
//using System.IO.Ports;
using System.Net.Sockets;
using Fins.IO;

namespace Fins.Device
{
	/// <summary>
	/// Fins IP master device.
	/// </summary>
	[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Breaking change.")]
	public class FinsIpMaster : FinsMaster
	{
		private FinsIpMaster(FinsTransport transport)
			: base(transport)
		{
		}

		/// <summary>
		/// Fins IP master factory method.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Breaking change.")]
		public static FinsIpMaster CreateIp(TcpClient tcpClient)
		{
			if (tcpClient == null)
				throw new ArgumentNullException("tcpClient");

			return CreateIp(new TcpClientAdapter(tcpClient));
		}

		/// <summary>
		/// Fins IP master factory method.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Breaking change.")]
		public static FinsIpMaster CreateIp(UdpClient udpClient)
		{
		    if (udpClient == null)
		        throw new ArgumentNullException("udpClient");
		    if (!udpClient.Client.Connected)
		        throw new InvalidOperationException(Resources.UdpClientNotConnected);

		    return CreateIp(new UdpClientAdapter(udpClient));
		}

		/// <summary>
		/// Fins IP master factory method.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Breaking change.")]
		public static FinsIpMaster Create(SerialPort serialPort)
		{
			if (serialPort == null)
				throw new ArgumentNullException("serialPort");

			return new FinsIpMaster(new FinsSerialTransport(new SerialPortAdapter(serialPort)));
		}

		/// <summary>
		/// Fins IP master factory method.
		/// </summary>
		[SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Breaking change.")]
		public static FinsIpMaster CreateIp(IStreamResource streamResource)
		{
			if (streamResource == null)
				throw new ArgumentNullException("streamResource");

			return new FinsIpMaster(new FinsTcpTransport(streamResource));
		}

		/// <summary>
		/// Read from 1 to 2000 contiguous coils status.
		/// </summary>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of coils to read.</param>
		/// <returns>Coils status</returns>
		public bool[] ReadCoils(ushort startAddress, ushort numberOfPoints)
		{
			return base.ReadCoils(Fins.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
		}

		/// <summary>
		/// Read from 1 to 2000 contiguous discrete input status.
		/// </summary>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of discrete inputs to read.</param>
		/// <returns>Discrete inputs status</returns>
		public bool[] ReadInputs(ushort startAddress, ushort numberOfPoints)
		{
			return base.ReadInputs(Fins.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
		}

		/// <summary>
		/// Read contiguous block of holding registers.
		/// </summary>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of holding registers to read.</param>
		/// <returns>Holding registers status</returns>
		public ushort[] ReadHoldingRegisters(ushort startAddress, ushort numberOfPoints)
		{
			return base.ReadHoldingRegisters(Fins.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
		}

		/// <summary>
		/// Read contiguous block of input registers.
		/// </summary>
		/// <param name="startAddress">Address to begin reading.</param>
		/// <param name="numberOfPoints">Number of holding registers to read.</param>
		/// <returns>Input registers status</returns>
		public ushort[] ReadInputRegisters(ushort startAddress, ushort numberOfPoints)
		{
			return base.ReadInputRegisters(Fins.DefaultIpSlaveUnitId, startAddress, numberOfPoints);
		}

		/// <summary>
		/// Write a single coil value.
		/// </summary>
		/// <param name="coilAddress">Address to write value to.</param>
		/// <param name="value">Value to write.</param>
		public void WriteSingleCoil(ushort coilAddress, bool value)
		{
			base.WriteSingleCoil(Fins.DefaultIpSlaveUnitId, coilAddress, value);
		}

		/// <summary>
		/// Write a single holding register.
		/// </summary>
		/// <param name="registerAddress">Value to write.</param>
		/// <param name="value">Value to write.</param>
		public void WriteSingleRegister(ushort registerAddress, ushort value)
		{
			base.WriteSingleRegister(Fins.DefaultIpSlaveUnitId, registerAddress, value);
		}

		/// <summary>
		/// Write a block of 1 to 123 contiguous registers.
		/// </summary>
		/// <param name="startAddress">Address to begin writing values.</param>
		/// <param name="data">Values to write.</param>
		public void WriteHoldingRegisters(ushort startAddress, ushort[] data)
		{
			base.WriteHoldingRegisters(Fins.DefaultIpSlaveUnitId, startAddress, data);
		}

		/// <summary>
		/// Force each coil in a sequence of coils to a provided value.
		/// </summary>
		/// <param name="startAddress">Address to begin writing values.</param>
		/// <param name="data">Values to write.</param>
		public void WriteCoils(ushort startAddress, bool[] data)
		{
			base.WriteCoils(Fins.DefaultIpSlaveUnitId, startAddress, data);
		}
	}
}
