using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modbus_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            tabControl1.SelectedIndex = 1;

            //自动获取COM口名称
            foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
            {
                comportName_rtu.Items.Add(com);
                comportName_rtu.SelectedIndex = 0;
                comportName_ascii.Items.Add(com);
                comportName_ascii.SelectedIndex = 0;
            }
            baudRate_rtu.SelectedIndex = 0;
            Parity_rtu.SelectedIndex = 0;//Even
            baudRate_ascii.SelectedIndex = 0;
            Parity_ascii.SelectedIndex = 0;//Even
        }

        #region Modbus Rtu

        private ModbusRTU modbusrtu = null;

        private void connectPLC_com_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusrtu == null)
                {
                    modbusrtu = new ModbusRTU();
                }
                System.IO.Ports.StopBits stopbits = System.IO.Ports.StopBits.One;
                System.IO.Ports.Parity parity = System.IO.Ports.Parity.Even;
                if (modbusrtu.serialPort1.IsOpen)
                {
                    if (modbusrtu.Disconnect())
                    { 
                        this.connectPLC_RTU.Text = "连接"; 
                    }
                }
                else
                {
                    switch (Parity_rtu.Text)
                    {
                        case "Even":
                            parity = System.IO.Ports.Parity.Even;
                            break;
                        case "None":
                            parity = System.IO.Ports.Parity.None;
                            //stopbits = System.IO.Ports.StopBits.Two;
                            break;
                        case "Odd":
                            parity = System.IO.Ports.Parity.Odd;
                            break;
                    }
                    if (modbusrtu.Connect(this.comportName_rtu.Text, int.Parse(this.baudRate_rtu.Text), 8, stopbits, parity))
                    { 
                        this.connectPLC_RTU.Text = "断开"; 
                    }
                }
            }
            catch(Exception ex)
            {
                txt_rtu.Text = "Connect Error : " + ex.Message;
            }
        }

        private void station_rtu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int station = Int32.Parse(station_rtu.Text);
            }
            catch (Exception ex)
            { 
                txt_rtu.Text = "Station Error : " + ex.Message; 
            }
        }

        private void read_rtu_Click(object sender, EventArgs e)
        {
            try
            {
                int time = Environment.TickCount;
                ModbusRTU.Area area = ModbusRTU.Area.Coil;
                switch (area_rtu.Text)
                {
                    case "coil":
                        area = ModbusRTU.Area.Coil;
                        break;
                    case "input":
                        area = ModbusRTU.Area.Input;
                        break;
                    case "Register":
                        area = ModbusRTU.Area.Register;
                        break;
                    case "InputRegister":
                        area = ModbusRTU.Area.InputRegister;
                        break;
                    default:
                        throw new Exception("未选择区域");
                }
                modbusrtu.Station = byte.Parse(station_rtu.Text);
                byte[] data = modbusrtu.Read(area, ushort.Parse(address_rtu.Text), int.Parse(length_rtu.Text));
                send_rtu.Text = BitConverter.ToString(modbusrtu.sendmessage).Replace("-", " ");
                recv_rtu.Text = BitConverter.ToString(modbusrtu.recvmessage).Replace("-", " ");
                data_rtu.Text = BitConverter.ToString(data).Replace("-", " ");
                txt_rtu.Text = "Read Time : " + (Environment.TickCount - time).ToString() + " ms";
            }
            catch(Exception ex)
            {
                txt_rtu.Text = "Read Error : " + ex.Message;
            }
        }

        private void write_rtu_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Modbus Ascii

        private ModbusASCII modbusascii = null;

        private void connectPLC_ASCII_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbusascii == null)
                {
                    modbusascii = new ModbusASCII();
                }
                System.IO.Ports.StopBits stopbits = System.IO.Ports.StopBits.One;
                System.IO.Ports.Parity parity = System.IO.Ports.Parity.Even;
                if (modbusascii.serialPort1.IsOpen)
                {
                    if (modbusascii.Disconnect())
                    { 
                        this.connectPLC_ASCII.Text = "连接"; 
                    }
                }
                else
                {
                    switch (Parity_ascii.Text)
                    {
                        case "Even":
                            parity = System.IO.Ports.Parity.Even;
                            break;
                        case "None":
                            parity = System.IO.Ports.Parity.None;
                            stopbits = System.IO.Ports.StopBits.Two;
                            break;
                        case "Odd":
                            parity = System.IO.Ports.Parity.Odd;
                            break;
                    }
                    if (modbusascii.Connect(this.comportName_ascii.Text, int.Parse(this.baudRate_ascii.Text), 7, stopbits, parity))
                    { 
                        this.connectPLC_ASCII.Text = "断开"; 
                    }
                }
            }
            catch(Exception ex)
            {
                txt_rtu.Text = "Connect Error : " + ex.Message;
            }
        }

        private void station_ascii_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int station = Int32.Parse(station_ascii.Text);
            }
            catch (Exception ex)
            {
                txt_rtu.Text = "Station Error : " + ex.Message;
            }
        }

        private void read_ascii_Click(object sender, EventArgs e)
        {
            try
            {
                int time = Environment.TickCount;
                ModbusASCII.Area area = ModbusASCII.Area.Coil;
                switch(area_ascii.Text)
                {
                    case "coil":
                        area = ModbusASCII.Area.Coil;
                        break;
                    case "input":
                        area = ModbusASCII.Area.Input;
                        break;
                    case "Register":
                        area = ModbusASCII.Area.Register;
                        break;
                    case "InputRegister":
                        area = ModbusASCII.Area.InputRegister;
                        break;
                    default:
                        throw new Exception("未选择区域");
                }
                modbusascii.Station = byte.Parse(station_ascii.Text);
                byte[] data = modbusascii.Read(area, ushort.Parse(address_ascii.Text), int.Parse(length_ascii.Text));
                send_ascii.Text = BitConverter.ToString(modbusascii.sendmessage).Replace("-", " ");
                recv_ascii.Text = BitConverter.ToString(modbusascii.recvmessage).Replace("-", " ");
                data_ascii.Text = BitConverter.ToString(data).Replace("-", " ");
                txt_rtu.Text = "Read Time : " + (Environment.TickCount - time).ToString() + " ms";
            }
            catch(Exception ex)
            {
                txt_ascii.Text = "Read Error : " + ex.Message;
            }
        }

        private void write_ascii_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                txt_rtu.Text = "Write Error : " + ex.Message;
            }
        }

        #endregion

        #region Modbus Tcp

        private ModbusTCP modbustcp = null;

        private void connectPLC_net_Click(object sender, EventArgs e)
        {
            try
            {
                if (modbustcp == null)
                {
                    modbustcp = new ModbusTCP();
                }
                if(modbustcp.connected)
                {
                    if (modbustcp.Disconnect())
                    { 
                        this.connectPLC_TCP.Text = "连接"; 
                    }
                }
                else
                {
                    if(modbustcp.Connect(TxtIP.Text,int.Parse(TxtPort.Text)))
                    {
                        this.connectPLC_TCP.Text = "断开";
                    }
                }
            }
            catch(Exception ex)
            {
                txt_tcp.Text = "Connect Error : " + ex.Message;
            }
        }

        private void read_tcp_Click(object sender, EventArgs e)
        {
            try
            {
                int time = Environment.TickCount;
                ModbusTCP.Area area = ModbusTCP.Area.Coil;
                switch (area_tcp.Text)
                {
                    case "coil":
                        area = ModbusTCP.Area.Coil;
                        break;
                    case "input":
                        area = ModbusTCP.Area.Input;
                        break;
                    case "Register":
                        area = ModbusTCP.Area.Register;
                        break;
                    case "InputRegister":
                        area = ModbusTCP.Area.InputRegister;
                        break;
                    default:
                        throw new Exception("未选择区域");
                }
                modbustcp.Station = byte.Parse(station_tcp.Text);
                byte[] data = modbustcp.Read(area, ushort.Parse(address_tcp.Text), int.Parse(length_tcp.Text));
                data_tcp.Text = BitConverter.ToString(data).Replace("-", " ");
                txt_tcp.Text = "Read Time : " + (Environment.TickCount - time).ToString() + " ms";
            }
            catch (Exception ex)
            {
                txt_tcp.Text = "Read Error : " + ex.Message;
                if (modbustcp.connected)
                {
                    if (modbustcp.Disconnect())
                    {
                        this.connectPLC_TCP.Text = "连接";
                    }
                }
            }
            finally
            {
                send_tcp.Text = BitConverter.ToString(modbustcp.sendmessage).Replace("-", " ");
                recv_tcp.Text = BitConverter.ToString(modbustcp.recvmessage).Replace("-", " ");
            }
        }

        private void write_tcp_Click(object sender, EventArgs e)
        {
            try
            {
                int time = Environment.TickCount;
                ModbusTCP.Area area = ModbusTCP.Area.Coil;
                switch (area_tcp.Text)
                {
                    case "coil":
                        area = ModbusTCP.Area.WriteCoil;
                        break;
                    case "Register":
                        area = ModbusTCP.Area.WriteRegister;
                        break;
                    default:
                        throw new Exception("该区域暂不支持写入");
                }
                modbustcp.Station = byte.Parse(station_tcp.Text);
                bool data = modbustcp.Write(area, ushort.Parse(address_tcp.Text), int.Parse(value_tcp.Text));
                data_tcp.Text = "Write " + data.ToString();
                txt_tcp.Text = "Write Time : " + (Environment.TickCount - time).ToString() + " ms";
            }
            catch (Exception ex)
            {
                txt_tcp.Text = "Write Error : " + ex.Message;
                if (modbustcp.connected)
                {
                    if (modbustcp.Disconnect())
                    {
                        this.connectPLC_TCP.Text = "连接";
                    }
                }
            }
            finally
            {
                send_tcp.Text = BitConverter.ToString(modbustcp.sendmessage).Replace("-", " ");
                recv_tcp.Text = BitConverter.ToString(modbustcp.recvmessage).Replace("-", " ");
            }
        }

        #endregion

        #region 界面处理

        private void min_width(object sender, EventArgs e)
        {

        }

        private void rtu_sendrecv_size(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 100;
        }

        private void tcp_sendrecv_size(object sender, EventArgs e)
        {
            splitContainer3.SplitterDistance = 100;
        }

        private void ascii_sendrecv_size(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = 100;
        }
        #endregion



    }
}
