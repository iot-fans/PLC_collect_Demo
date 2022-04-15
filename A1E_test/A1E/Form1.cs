using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication;
using HslCommunication.Melsec;

namespace A1E
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ReadBtn.Enabled = false;
            ReadFBtn.Enabled = false;
            ReadBitBtn.Enabled = false;
            DisconnectBtn.Enabled = false;
        }

        private MelsecA1ENet Melsec = null;
        private bool connected = false;

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(!connected)
                {
                    Melsec = new MelsecA1ENet(text_ip.Text, int.Parse(text_port.Text));
                    Melsec.ConnectTimeOut = 1000;
                    Melsec.ReceiveTimeOut = 3000;
                    OperateResult result = Melsec.ConnectServer();
                    if (!result.IsSuccess)
                    {
                        throw new Exception("connect failed." + result.Message);
                    }
                    ReadBtn.Enabled = true;
                    ReadFBtn.Enabled = true;
                    ReadBitBtn.Enabled = true;
                    DisconnectBtn.Enabled = true;
                    ConnectBtn.Enabled = false;
                    connected = true;
                }
            }
            catch(Exception ex)
            {
                text_err.Text = ex.Message;
            }
        }

        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(connected)
                {
                    if (Melsec != null)
                    {
                        try { Melsec.ConnectClose(); }
                        finally { Melsec = null; }
                    }
                    ReadBtn.Enabled = false;
                    ReadFBtn.Enabled = false;
                    ReadBitBtn.Enabled = false;
                    DisconnectBtn.Enabled = false;
                    ConnectBtn.Enabled = true;
                    connected = false;
                }
            }
            catch (Exception ex)
            {
                text_err.Text = ex.Message;
            }
        }

        private void ReadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult<byte[]> Result = new OperateResult<byte[]>();
                string address = text_addr.Text.Trim();
                ushort SizeRead = ushort.Parse(text_num.Text.Trim());
                StringBuilder outputstr = new StringBuilder();
                Result = Melsec.Read(address, SizeRead);
                if (Result.IsSuccess)
                {
                    for (int i = 0; i < SizeRead; i++)
                    {
                        outputstr.Append(string.Format("{0:X2}",Result.Content[i]));
                        outputstr.Append(" ");
                    }
                    text_data.Text = outputstr.ToString();
                }
                else
                {
                    throw new Exception(Result.Message);
                }
            }
            catch (Exception ex)
            {
                text_err.Text = ex.Message;
                DisconnectBtn_Click(null, null);
            }
        }

        private void ReadFBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult<byte[]> Result = new OperateResult<byte[]>();
                string address = text_addr.Text.Trim();
                ushort SizeRead = 4;
                StringBuilder outputstr = new StringBuilder();
                Result = Melsec.Read(address, SizeRead);
                if (Result.IsSuccess)
                {
                    UInt32 rdata;
                    rdata = Result.Content[3]; rdata <<= 8;
                    rdata |= Result.Content[2]; rdata <<= 8;
                    rdata |= Result.Content[1]; rdata <<= 8;
                    rdata |= Result.Content[0];

                    byte[] FloatArray = BitConverter.GetBytes(rdata);
                    outputstr.Append(BitConverter.ToSingle(FloatArray, 0).ToString("f3"));
                    text_data.Text = outputstr.ToString();
                }
                else
                {
                    throw new Exception(Result.Message);
                }
            }
            catch (Exception ex)
            {
                text_err.Text = ex.Message;
                DisconnectBtn_Click(null, null);
            }
        }

        private void ReadBitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OperateResult<bool[]> Result = new OperateResult<bool[]>();
                string address = text_addr.Text.Trim();
                ushort SizeRead = ushort.Parse(text_num.Text.Trim());
                StringBuilder outputstr = new StringBuilder();
                Result = Melsec.ReadBool(address, SizeRead);
                if (Result.IsSuccess)
                {
                    for (int i = 0; i < SizeRead; i++)
                    {
                        outputstr.Append(Result.Content[i].ToString());
                        outputstr.Append(" ");
                    }
                    text_data.Text = outputstr.ToString();
                }
                else
                {
                    throw new Exception(Result.Message);
                }
            }
            catch (Exception ex)
            {
                text_err.Text = ex.Message;
                DisconnectBtn_Click(null, null);
            }
        }
    }
}
