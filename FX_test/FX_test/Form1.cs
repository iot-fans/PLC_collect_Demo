using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FX_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FX_com = new FX();
        }

        private int address;

        private FX FX_com = null;

        private void connectPLC_Click(object sender, EventArgs e)
        {
            if (FX_com.serialPort1.IsOpen)
            {
                if (FX_com.Disconnect())
                { this.connectPLC.Text = "连接"; }
            }
            else
            {
                if (FX_com.connect(this.comportName.Text))
                { this.connectPLC.Text = "断开"; }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //自动获取COM口名称
            foreach (string com in System.IO.Ports.SerialPort.GetPortNames())
            {
                this.comportName.Items.Add(com);
            }
            address = 0;
            address_.Text = Convert.ToString(address);
            count.Text = "1";//默认个数
        }

        private void address_add_Click(object sender, EventArgs e)
        {
            address++;
            address_.Text = Convert.ToString(address);
        }

        private void address_reduce_Click(object sender, EventArgs e)
        {
            address--;
            address_.Text = Convert.ToString(address);
        }

        private void address__TextChanged(object sender, EventArgs e)
        {
            try
            {
                address = Int32.Parse(address_.Text);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void read_Click(object sender, EventArgs e)
        {
            return_value.Text = "";
            int time = Environment.TickCount;
            try
            {
                lock (this)
                {
                    byte[] data = null;
                    bool[] value = null;
                    string outputstring = "";
                    switch (address_type.Text)
                    {
                        case "D":
                            data = FX_com.read(FX.Typ.D, address, int.Parse(count.Text));
                            return_value.Text = data2string(data);
                            break;
                        case "M":
                            data = FX_com.read(FX.Typ.M, address, int.Parse(count.Text));
                            value = byte2bool(HexToBytes(data));
                            outputstring = "";
                            for (int i = 0; i < int.Parse(count.Text); i++)
                            {
                                outputstring += value[i + address % 16].ToString();
                                outputstring += " ";
                            }
                            return_value.Text = outputstring;
                            break;
                        case "X"://八进制
                            data = FX_com.read(FX.Typ.X, address, int.Parse(count.Text));
                            value = byte2bool(HexToBytes(data));
                            outputstring = "";
                            for (int i = 0; i < int.Parse(count.Text); i++)
                            {
                                outputstring += value[i + address % 16].ToString();
                                outputstring += " ";
                            }
                            return_value.Text = outputstring;
                            break;
                        case "Y"://八进制
                            data = FX_com.read(FX.Typ.Y, address, int.Parse(count.Text));
                            value = byte2bool(HexToBytes(data));
                            outputstring = "";
                            for (int i = 0; i < int.Parse(count.Text); i++)
                            {
                                outputstring += value[i + address % 16].ToString();
                                outputstring += " ";
                            }
                            return_value.Text = outputstring;
                            break;
                    }
                    send_msg.Text = FX_com.send_string;
                    recv_msg.Text = FX_com.recv_string;
                    time = Environment.TickCount - time;
                    Time.Text = time.ToString() + " ms";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //System.Threading.Thread.Sleep(200);
        }

        public string data2string(byte[] data)
        {
            string return_string = "";
            //for (int i = 0; i < data.Length; i += 4)
            //{
            //    return_string += Encoding.ASCII.GetString(data, i + 2, 2);
            //    return_string += Encoding.ASCII.GetString(data, i, 2);
            //    if (i < data.Length - 4)
            //    {
            //        return_string += " ";
            //    }
            //}
            byte[] bytedata = HexToBytes(data);
            int[] value = new int[bytedata.Length / 2];
            for (int i = 0; i < value.Length; i+=2)
            {
                value[i] = (int)((bytedata[i + 1] << 8) | bytedata[i]);
                if (value[i] > 32767)
                {
                    value[i] -= 65536;
                }
                return_string += value[i].ToString();
                if (i < value.Length - 1)
                {
                    return_string += " ";
                }
            }
            return return_string;
        }

        public static byte[] HexToBytes(byte[] hex)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");

            if (hex.Length % 2 != 0)
                throw new FormatException("HexCharacterCountNotEven");

            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte("" + (char)hex[i * 2] + (char)hex[i * 2 + 1], 16);

            return bytes;
        }

        public bool[] byte2bool(byte[] data)
        {
            bool[] value = new bool[data.Length * 8];
            for (int i = 0; i < data.Length / 2; i++)
            {
                int j = i * 16, k = i * 2;
                value[j + 0] = (data[k] & 1) > 0;
                value[j + 1] = (data[k] & 2) > 0;
                value[j + 2] = (data[k] & 4) > 0;
                value[j + 3] = (data[k] & 8) > 0;
                value[j + 4] = (data[k] & 16) > 0;
                value[j + 5] = (data[k] & 32) > 0;
                value[j + 6] = (data[k] & 64) > 0;
                value[j + 7] = (data[k] & 128) > 0;

                k++;
                value[j + 8] = (data[k] & 1) > 0;
                value[j + 9] = (data[k] & 2) > 0;
                value[j + 10] = (data[k] & 4) > 0;
                value[j + 11] = (data[k] & 8) > 0;
                value[j + 12] = (data[k] & 16) > 0;
                value[j + 13] = (data[k] & 32) > 0;
                value[j + 14] = (data[k] & 64) > 0;
                value[j + 15] = (data[k] & 128) > 0;
            }

            return value;
        }

        private void write_Click(object sender, EventArgs e)
        {
            return_value.Text = "";
            try
            {
                lock (this)
                {
                    //byte[] data = null;
                    bool write_ok = false;
                    switch (address_type.Text)
                    {
                        case "D":
                            write_ok = FX_com.write(FX.Typ.D, address, int.Parse(write_value.Text));
                            break;
                        case "M"://有问题，地址问题
                            write_ok = FX_com.writeBool(FX.Typ.M, address, bool.Parse(bitValue.Text));
                            //return_value.Text = write_ok.ToString();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
