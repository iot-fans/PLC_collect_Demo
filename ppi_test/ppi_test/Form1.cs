using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ppi_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.PPI_com = new PPI();
            this.read.Enabled = false;
            //this.wirte.Enabled = false;
        }

        private int address;

        private PPI PPI_com = null;

        private void connectPLC_Click(object sender, EventArgs e)
        {
            if (PPI_com.serialPort1.IsOpen)
            {
                if (PPI_com.Disconnect())
                { this.connectPLC.Text = "连接"; this.read.Enabled = false; }
            }
            else
            {
                if (PPI_com.connect(this.comportName.Text,short.Parse(this.baudRate.Text)))
                { this.connectPLC.Text = "断开"; this.read.Enabled = true; }
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
            //默认波特率为9600
            baudRate.SelectedIndex = 0;
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

        private void read_Click(object sender, EventArgs e)
        {
            try
            {
                lock (this)
                {
                    byte[] data = null;
                    switch (address_type.Text)
                    {
                        case "VB":
                            data = PPI_com.Read(PPI.Typ.V, PPI.Siz.B, address, int.Parse(count.Text));
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "MB":
                            data = PPI_com.Read(PPI.Typ.M, PPI.Siz.B, address, int.Parse(count.Text));
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "IB":
                            data = PPI_com.Read(PPI.Typ.I, PPI.Siz.B, address, int.Parse(count.Text));
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "QB":
                            data = PPI_com.Read(PPI.Typ.Q, PPI.Siz.B, address, int.Parse(count.Text));
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "SMB":
                            data = PPI_com.Read(PPI.Typ.SM, PPI.Siz.B, address, int.Parse(count.Text));
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "AI":
                            data = PPI_com.Read(PPI.Typ.AI, PPI.Siz.W, address, int.Parse(count.Text));//返回2个字节
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "AQ":
                            data = PPI_com.Read(PPI.Typ.AQ, PPI.Siz.W, address, int.Parse(count.Text));//返回2个字节
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "S":
                            data = PPI_com.Read(PPI.Typ.S, PPI.Siz.B, address, int.Parse(count.Text));
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "T":
                            data = PPI_com.Read(PPI.Typ.T, PPI.Siz.W, address, int.Parse(count.Text));//返回5个字节
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "C":
                            data = PPI_com.Read(PPI.Typ.C, PPI.Siz.W, address, int.Parse(count.Text));//返回3个字节
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        case "HC":
                            data = PPI_com.Read(PPI.Typ.HC, PPI.Siz.W, address, int.Parse(count.Text));//返回3个字节
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                        default:
                            data = PPI_com.Read(PPI.Typ.V, PPI.Siz.B, address, int.Parse(count.Text));
                            return_value.Text = BitConverter.ToString(data).Replace("-", " ");
                            break;
                    }
                    Time.Text = PPI_com.time.ToString() + " ms";
                    send_msg.Text = PPI_com.send_string;
                    recv_msg.Text = PPI_com.recv_string;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            System.Threading.Thread.Sleep(200);
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

        private void wirte_Click(object sender, EventArgs e)
        {
            try
            {
                switch (address_type.Text)
                {
                    case "VB":
                        PPI_com.Write(PPI.Typ.V, PPI.Siz.B, address, int.Parse(write_value.Text));
                        break;
                    case "MB":
                        PPI_com.Write(PPI.Typ.M, PPI.Siz.B, address, int.Parse(write_value.Text));
                        break;
                    case "IB":
                        PPI_com.Write(PPI.Typ.I, PPI.Siz.B, address, int.Parse(write_value.Text));
                        break;
                    case "QB":
                        PPI_com.Write(PPI.Typ.Q, PPI.Siz.B, address, int.Parse(write_value.Text));
                        break;
                    case "SMB":
                        PPI_com.Write(PPI.Typ.SM, PPI.Siz.B, address, int.Parse(write_value.Text));//0-29不能写，30-194可以写(30也不建议写)
                        break;
                    case "S":
                        PPI_com.Write(PPI.Typ.SM, PPI.Siz.B, address, int.Parse(write_value.Text));//写入失败
                        break;
                    default:
                        break;
                }
                Time.Text = PPI_com.time.ToString() + " ms";
                send_msg.Text = PPI_com.send_string;
                recv_msg.Text = PPI_com.recv_string;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
