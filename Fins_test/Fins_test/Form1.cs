using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Fins.Device;
using Fins.IO;
using Fins.Utility;

namespace Fins_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //自动获取COM口名称
            foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
            {
                comportName_plc.Items.Add(com);
                comportName_plc.SelectedIndex = 0;
            }
            baudRate_plc.SelectedIndex = 0;
            databits_plc.SelectedIndex = 0;
            stopbits_plc.DataSource = System.Enum.GetNames(typeof(Fins.IO.StopBits));
            stopbits_plc.Text = StopBits.Two.ToString();
            Parity_plc.DataSource = System.Enum.GetNames(typeof(Fins.IO.Parity));
            Parity_plc.SelectedIndex = 2;

            read_plc.Enabled = true;
            write_plc.Enabled = true;
        }

        private void read_plc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!startread)
                {
                    read_plc.Text = "停止读取";
                    data_plc.Clear();
                    timer1.Start();
                    area_plc.Enabled = false;
                    address_plc.Enabled = false;
                    length_plc.Enabled = false;
                    //value_plc.Enabled = false;
                    startread = true;
                }
                else
                {
                    disconnect();
                }
            }
            catch (Exception ex)
            {
                txt_plc.Text = "read error : " + ex.Message;
            }
        }

        private void disconnect()
        {
            if ("SERIAL" == transport.SelectedTab.Text.ToUpper())
            {
                lock (GlobalShareObjs.Instance)
                {
                    object o = GlobalShareObjs.GetObj(comportName_plc.Text);
                    if (o != null && o.GetHashCode() == shareObjHashCode)
                    {
                        GlobalShareObjs.DelObj(comportName_plc.Text);
                    }
                }
            }

            read_plc.Text = "开始读取";
            timer1.Stop();
            area_plc.Enabled = true;
            address_plc.Enabled = true;
            length_plc.Enabled = true;
            //value_plc.Enabled = true;
            startread = false;

            if (fins == null)
            {
                return;
            }
            try { using (fins) { } }
            finally { fins = null; }
        }

        private void write_plc_Click(object sender, EventArgs e)
        {
            try
            {
                timer1.Stop();
                if ("SERIAL" == transport.SelectedTab.Text.ToUpper())
                {
                    if (Fins == null) { }//Init Fins before GetLock to have lock to be used
                    lock (GlobalShareObjs.GetObj(comportName_plc.Text))
                    {
                        switch (area_plc.Text)
                        {
                            case "CIO":
                                Fins.WriteSingleRegister((byte)StationNumber, ushort.Parse(address_plc.Text), ushort.Parse(value_plc.Text));
                                break;
                            default:
                                Fins.WriteSingleRegister((byte)StationNumber, ushort.Parse(address_plc.Text), ushort.Parse(value_plc.Text));
                                break;
                        }
                    }
                }
                else
                {
                    Fins.WriteSingleRegister((byte)StationNumber, ushort.Parse(address_plc.Text), ushort.Parse(value_plc.Text));
                }
            }
            catch (Exception ex)
            {
                txt_plc.Text = "write error : " + ex.Message;
            }
            finally
            {
                timer1.Start();
            }
        }

        private ushort BoolToUShort(bool b)
        {
            return (ushort)(b ? 1 : 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ushort[] data = null;
                if ("SERIAL" == transport.SelectedTab.Text.ToUpper())
                {
                    if (Fins == null) { }//Init Fins before GetLock to have lock to be used
                    lock (GlobalShareObjs.GetObj(comportName_plc.Text))
                    {
                        switch (area_plc.Text)
                        {
                            case "CIO":
                                bool[] bdata = Fins.ReadCoils((byte)StationNumber, ushort.Parse(address_plc.Text), ushort.Parse(length_plc.Text));
                                data = Array.ConvertAll(bdata, new Converter<bool, ushort>(BoolToUShort));
                                break;
                            default:
                                data = Fins.ReadHoldingRegisters((byte)StationNumber, ushort.Parse(address_plc.Text), ushort.Parse(length_plc.Text));
                                break;
                        }
                    }
                }
                else
                {
                    data = Fins.ReadHoldingRegisters((byte)StationNumber, ushort.Parse(address_plc.Text), ushort.Parse(length_plc.Text));
                }
                if (data != null)
                {
                    int len = 20;
                    if(data.Length<20)
                    {
                        len = data.Length;
                    }
                    string datastring = "";
                    for (int i = 0; i < data.Length; i++)
                    {
                        byte[] bytedata = BitConverter.GetBytes(data[i]);
                        byte temp = bytedata[0];
                        bytedata[0] = bytedata[1];
                        bytedata[1] = temp;
                        string asciidata = BitConverter.ToString(bytedata).Replace("-", "");
                        datastring+=asciidata;
                        datastring+=" ";
                        if ((i + 1) % len == 0 && i > 0)
                        {
                            datastring+=Environment.NewLine;
                        }
                    }
                    data_plc.AppendText(datastring);
                }
                else
                {
                    throw new Exception("No data received! area=" + area_plc.Text + " address=" + address_plc.Text + " length=" + length_plc.Text);
                }
            }
            catch (Exception ex)
            {
                disconnect();
                txt_plc.Text = "read error : " + ex.Message;
            }
        }

        #region 通讯字段

        public bool startread = false;

        public int baudRate { get { return int.Parse(baudRate_plc.Text); } }

        public int dataBits { get { return int.Parse(databits_plc.Text); } }

        public int stopBits { get { return stopbits_plc.SelectedIndex; } }

        public int Parity { get { return Parity_plc.SelectedIndex; } }

        public int StationNumber { get { return int.Parse(station_plc.Text); } }

        private int shareObjHashCode = 0;

        private FinsMaster fins = null;
        private FinsMaster Fins
        {
            get
            {
                try
                {
                    if ((fins != null) && (fins.Transport != null)) return fins;

                    string protocol = transport.SelectedTab.Text.ToUpper();
                    switch (protocol)
                    {
                        case "SERIAL":
                            SerialPort sp = null;
                            lock (GlobalShareObjs.Instance)
                            {
                                object o = GlobalShareObjs.GetObj(comportName_plc.Text);
                                if (o != null && o is SerialPort)
                                {
                                    sp = (SerialPort)o;
                                }
                                else
                                {
                                    sp = new SerialPort(comportName_plc.Text);
                                    GlobalShareObjs.DelObj(comportName_plc.Text);
                                    GlobalShareObjs.AddObj(comportName_plc.Text, sp);
                                    shareObjHashCode = sp.GetHashCode();
                                    sp.BaudRate = baudRate;
                                    sp.DataBits = dataBits;
                                    sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBits.ToString()); ;
                                    sp.Parity = (Parity)Enum.Parse(typeof(Parity), Parity.ToString());
                                    sp.Open();
                                }
                            }
                            fins = FinsIpMaster.Create(sp);
                            break;
                        case "TCP":
                            fins = FinsIpMaster.CreateIp(new TcpClient(TxtIP.Text, int.Parse(TxtPort.Text)));
                            break;
                    }

                    fins.Transport.ReadTimeout = 6000;
                    fins.Transport.WriteTimeout = 6000;
                    fins.Transport.Retries = 2;
                    return fins;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

    }
}
