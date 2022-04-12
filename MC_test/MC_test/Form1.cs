using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MC_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ProtocolType.SelectedIndex = 0;
            area_plc.SelectedIndex = 0;
        }

        private IMelsecMaster melsec = null;

        private IMelsecMaster Melsec
        {
            get
            {
                if (melsec != null) return melsec;
                switch (ProtocolType.Text)
                {
                    case "ASCII":
                        QsLsFXsAscTcp at = new QsLsFXsAscTcp();
                        at.ActHostAddress = TxtIP.Text;
                        at.ActPortNumber = int.Parse(TxtPort.Text);
                        at.Open();
                        melsec = at;
                        break;
                    case "BINARY":
                        QsLsFXsBinTcp bt = new QsLsFXsBinTcp();
                        bt.ActHostAddress = TxtIP.Text;
                        bt.ActPortNumber = int.Parse(TxtPort.Text);
                        bt.Open();//
                        melsec = bt;
                        break;
                    default:
                        break;
                }

                return melsec;
            }
        }

        public void Disconnect()
        {
            if (melsec == null)
            {
                return;
            }
            try { using (melsec) { } }
            finally { melsec = null; }
        }

        private byte ShortToByte(short s)
        {
            return (byte)s;
        }

        private void read_plc_Click(object sender, EventArgs e)
        {
            try
            {
                short[] data = null;
                Melsec.ReadDeviceBlock(area_plc.Text, int.Parse(address_plc.Text), int.Parse(length_plc.Text), out data);
                byte[] bytedata = Array.ConvertAll(data, new Converter<short, byte>(ShortToByte));
                data_plc.Text = BitConverter.ToString(bytedata).Replace("-", " "); ;
            }
            catch (Exception ex)
            {
                Disconnect();
                txt_plc.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff") + ": " + "读取错误 ： " + ex.Message;
            }
        }

        private void write_plc_Click(object sender, EventArgs e)
        {
            try
            {
                short[] data = new short[1];
                data[0] = short.Parse(value_plc.Text);
                int value = 1;
                value = Melsec.WriteDeviceBlock(area_plc.Text, int.Parse(address_plc.Text), data.Length, data);
                if (value != 0)
                {
                    throw new Exception("area:" + area_plc.Text + " address:" + address_plc.Text + " value:" + value_plc.Text);
                }
            }
            catch (Exception ex)
            {
                Disconnect();
                txt_plc.Text = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff") + ": " + "写入错误 ： " + ex.Message;
            }
        }
    }
}
