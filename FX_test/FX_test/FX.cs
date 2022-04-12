using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FX_test
{
    class FX
    {
        public FX()
        {
            serialPort1 = new System.IO.Ports.SerialPort();
            //this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
        }

        public System.IO.Ports.SerialPort serialPort1;
        public string send_string = "";
        public string recv_string = "";
        //public List<byte> Data;
        //public bool writeOK;

        public bool connect(string com, Int16 BaudRate = 9600)
        {
            try
            {
                //*** 设置端口参数*****//
                serialPort1.Close();
                serialPort1.BaudRate = BaudRate;
                serialPort1.DataBits = 7;
                serialPort1.StopBits = System.IO.Ports.StopBits.One;
                serialPort1.Parity = System.IO.Ports.Parity.Even;
                serialPort1.PortName = com;
                serialPort1.Open();//打开端口

                if (serialPort1.IsOpen)
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("端口打开失败！" + e.Message, "提示");
                return false;
            }
            return false;
        }

        public bool Disconnect()
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    return true;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("端口关闭失败！" + e.Message, "提示");
                return false;
            }
            return false;

        }

        //public void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    int length = serialPort1.BytesToRead;
        //    byte[] data = new byte[length];
        //    //int ae = 0;
        //    try
        //    {
        //        if (length > 0)
        //        {
        //            //byte[] data = new byte[length];
        //            serialPort1.Read(data, 0, length);
        //            serialPort1.DiscardInBuffer();
        //            //MessageBox.Show( BitConverter.ToString(data).Replace("-", " "));
        //            if (length > 2)
        //            {
        //                if(data[0]==0x02)
        //                {
        //                    Data = new List<byte>();
        //                    if(data[data.Length-3] == 0x03)
        //                    {
        //                        byte[] newdata = new byte[data.Length - 4];
        //                        Array.Copy(data, 1, newdata, 0, newdata.Length);
        //                        Data.AddRange(newdata);
        //                    }
        //                    else
        //                    {
        //                        byte[] newdata = new byte[data.Length - 1];
        //                        Array.Copy(data, 1, newdata, 0, newdata.Length);
        //                        Data.AddRange(newdata);
        //                    }
        //                }
        //                else
        //                {
        //                    if (data[data.Length - 3] == 0x03)
        //                    {
        //                        byte[] newdata = new byte[data.Length - 3];
        //                        Array.Copy(data, 0, newdata, 0, newdata.Length);
        //                        Data.AddRange(newdata);
        //                    }
        //                    else
        //                    {
        //                        byte[] newdata = new byte[data.Length];
        //                        Array.Copy(data, 0, newdata, 0, newdata.Length);
        //                        Data.AddRange(newdata);
        //                    }
        //                }
        //            }
        //            if(length == 1)
        //            {
        //                if(data[0] == 0x06)
        //                {
        //                    writeOK = true;
        //                }
        //                if(data[0] == 0x15)
        //                {
        //                    writeOK = false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        public static byte[] GetAsciiBytes(params ushort[] numbers)
        {
            string asciiStr = "";
            foreach (ushort u in numbers)
            {
                asciiStr += u.ToString("X4");
            }
            return Encoding.ASCII.GetBytes(asciiStr);
        }

        public static byte[] GetAsciiBytes(params byte[] numbers)
        {
            string asciiStr = "";
            foreach (byte b in numbers)
            {
                asciiStr += b.ToString("X2");
            }
            return Encoding.ASCII.GetBytes(asciiStr);
        }

        public byte[] read(Typ tp,int address,int count)
        {
            //this.Data = null;
            byte[] data = new byte[11];
            data[0] = 0x02;
            data[1] = 0x30;//读出
            byte[] bs = null;
            byte byteCount;
            switch (tp)
            {
                case Typ.D:
                    if (address >= 8000)
                    {
                        bs = GetAsciiBytes((ushort)((address - 8000) * 2 + 0x0E00));//D区地址:0x0E00 - 0x0FF0（D8000 - D8255）
                    }
                    else
                    {
                        bs = GetAsciiBytes((ushort)(address * 2 + 0x1000));//D区地址:0x1000 - 0x13F7（D0 - D512）
                    }                 
                    data[2] = bs[0];
                    data[3] = bs[1];
                    data[4] = bs[2];
                    data[5] = bs[3];
                    bs = GetAsciiBytes((byte)(count * 4));//一个字四个字节
                    if(count>63)
                    {
                        MessageBox.Show("The count is too large");
                    }
                    data[6] = bs[0];
                    data[7] = bs[1];
                    break;
                case Typ.M:
                    if (address >= 8000)
                    {
                        bs = GetAsciiBytes((ushort)(((address - 8000) / 16) * 2 + 0x01E0));//M区地址:0x01E0 - 0x01FF（M8000 - M8255）
                    }
                    else
                    {
                        bs = GetAsciiBytes((ushort)((address / 16) * 2 + 0x0100));//M区地址:0x0100 - 0x017F（M0 - M1023）、0x0300 - 0x037F（M0 - M1023）
                    }
                    data[2] = bs[0];
                    data[3] = bs[1];
                    data[4] = bs[2];
                    data[5] = bs[3];
                    byteCount = (byte)(((address + count) / 16 + 1) * 2 - (address / 16) * 2);
                    bs = GetAsciiBytes(byteCount);
                    data[6] = bs[0];
                    data[7] = bs[1];
                    break;
                case Typ.X:
                    bs = GetAsciiBytes((ushort)((address / 16) * 2 + 0x80));//X区地址:0x0080 - 0x008F（X0 - X77）(逻辑地址八进制)
                    data[2] = bs[0];
                    data[3] = bs[1];
                    data[4] = bs[2];
                    data[5] = bs[3];
                    byteCount = (byte)(((address + count) / 16 + 1) * 2 - (address / 16) * 2);
                    bs = GetAsciiBytes(byteCount);
                    data[6] = bs[0];
                    data[7] = bs[1];
                    break;
                case Typ.Y:
                    bs = GetAsciiBytes((ushort)((address / 16) * 2 + 0xA0));//Y区地址:0x00A0 - 0x00AF（Y0 - Y77）(逻辑地址八进制)
                    data[2] = bs[0];
                    data[3] = bs[1];
                    data[4] = bs[2];
                    data[5] = bs[3];
                    byteCount = (byte)(((address + count) / 16 + 1) * 2 - (address / 16) * 2);
                    bs = GetAsciiBytes(byteCount);
                    data[6] = bs[0];
                    data[7] = bs[1];
                    break;
            }
            data[8] = 0x03;
            ushort sum = 0;
            for (int i = 1; i < 9; i++)
            {
                sum += data[i];
            }
            string sumStr = sum.ToString("X2");

            data[9] = (byte)sumStr[sumStr.Length - 2];
            data[10] = (byte)sumStr[sumStr.Length - 1];

            //try
            //{

            //    if (this.serialPort1.IsOpen)
            //    {
            //        Thread.Sleep(10);
            //        serialPort1.Write(data, 0, data.Length);
            //        //throw new Exception(BitConverter.ToString(data).Replace("-"," "));
            //        //MessageBox.Show(BitConverter.ToString(data).Replace("-", " "));
            //    }
            //    else
            //    {
            //        //MessageBox.Show("端口尚未打开");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message);
            //    throw new Exception(ex.Message);
            //}
            //int time = ((count / 6 + 1) / 2 + 1) * 100;
            //Thread.Sleep(time);//数量增加，时间增加 6 100 18 200 30 300 42 400 54 500
            //return this.Data.ToArray();
            send_string = BitConverter.ToString(data).Replace("-", " ");//发送信息字符串
            return readbase(data);
        }

        private byte[] readbase(byte[] send)
        {
            try
            {//发送

                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(send, 0, send.Length);
                    //MessageBox.Show(BitConverter.ToString(data).Replace("-", " "));
                }
                else
                {
                    throw new Exception("端口尚未打开");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            byte[] data = null;//返回结果
            try
            {
                byte[] buffer = new byte[512];
                List<byte> instructionData = new List<byte>();
                int count = 0;
                int noDataTimes = 0;
                int headerIndex = -1;//开头字符
                int tailIndex = -1;//结束定界符
                while (true)
                {
                    count = serialPort1.Read(buffer, 0, buffer.Length);
                    if (noDataTimes > 16)
                    {
                        throw new Exception("Read resulted in 0 bytes returned.");
                    }
                    if (count == 0)
                    {
                        noDataTimes++;
                        System.Threading.Thread.Sleep(10);
                        continue;
                    }
                    else
                    {
                        noDataTimes = 0;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        instructionData.Add(buffer[i]);
                    }
                    byte cmd = instructionData[0];
                    if ((cmd == 0x06) || (cmd == 0x15))
                    {//写入返回
                        data = new byte[] { cmd };
                        instructionData.RemoveAt(0);
                        return data;
                    }
                    if (headerIndex < 0)
                    {
                        for (int i = 0; i < instructionData.Count; i++)
                        {
                            if (instructionData[i] == 0x02)
                            {
                                headerIndex = i;
                                break;
                            }
                        }
                        if (headerIndex < 0)
                        {
                            instructionData.Clear();
                            continue;
                        }
                        if (headerIndex > 0)
                        {
                            instructionData.RemoveRange(0, headerIndex);
                        }
                    }
                    if (tailIndex < 0)
                    {
                        for (int i = 0; i < instructionData.Count; i++)
                        {
                            if (instructionData[i] == 0x03)
                            {
                                tailIndex = i;
                                break;
                            }
                        }
                        if (tailIndex < 0)
                        {
                            if (instructionData.Count > 10240)
                            {
                                instructionData.Clear();
                                headerIndex = -1;
                            }
                            continue;
                        }
                    }
                    if (tailIndex + 2 + 1 <= instructionData.Count)
                    {
                        data = new byte[tailIndex - 1];
                        for (int i = 1; i <= data.Length; i++)
                        {
                            data[i - 1] = instructionData[i];
                        }
                        recv_string = BitConverter.ToString(instructionData.ToArray()).Replace("-", " ");//接收信息字符串
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool write(Typ tp, int address, int value)
        {
            byte[] data = new byte[15];
            data[0] = 0x02;
            data[1] = 0x31;//写入
            byte[] bs = null;
            switch (tp)
            {
                case Typ.D:
                    if (address >= 8000)
                    {
                        bs = GetAsciiBytes((ushort)((address - 8000) * 2 + 0x0E00));//D区地址:0x0E00 - 0x0FF0（D8000 - D8255）
                    }
                    else
                    {
                        bs = GetAsciiBytes((ushort)(address * 2 + 0x1000));//D区地址:0x1000 - 0x13F7（D0 - D512）
                    }
                    data[2] = bs[0];
                    data[3] = bs[1];
                    data[4] = bs[2];
                    data[5] = bs[3];
                    data[6] = 0x30;
                    data[7] = 0x32;//只写入一个字节
                    break;
            }

            data[8] = Encoding.ASCII.GetBytes(value.ToString("X4"))[2];//2
            data[9] = Encoding.ASCII.GetBytes(value.ToString("X4"))[3];//3
            data[10] = Encoding.ASCII.GetBytes(value.ToString("X4"))[0];//0
            data[11] = Encoding.ASCII.GetBytes(value.ToString("X4"))[1];//1

            data[12] = 0x03;
            ushort sum = 0;
            for (int i = 1; i < 13; i++)
            {
                sum += data[i];
            }
            string sumStr = sum.ToString("X2");
            data[13] = (byte)sumStr[sumStr.Length - 2];
            data[14] = (byte)sumStr[sumStr.Length - 1];

            byte[] result = readbase(data);
            if(result[0]==0x06)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool writeBool(Typ tp, int address, bool value)
        {
            byte[] data = new byte[9];
            data[0] = 0x02;
            if(value)
            {
                data[1] = 0x37;//强制ON
            }
            else
            {
                data[1] = 0x38;//强制ON
            }
            byte[] bs = null;
            switch (tp)
            {
                case Typ.M:
                    if (address >= 8000)
                    {
                        bs = GetAsciiBytes((ushort)(((address - 8000)) + 0x0F00));//M区地址:0x0F00 - 0x0FFF（M8000 - M8255）
                    }
                    else
                    {
                        bs = GetAsciiBytes((ushort)(address + 0x0800));//M区地址:0x0800 - 0x0BFF（M0 - M1023）
                    }
                    //if (address >= 8000)
                    //{
                    //    bs = GetAsciiBytes((ushort)(((address - 8000) / 16) * 2 + 0x01E0));//M区地址:0x01E0 - 0x01FF（M8000 - M8255）
                    //}
                    //else
                    //{
                    //    bs = GetAsciiBytes((ushort)((address / 16) * 2 + 0x0100));//M区地址:0x0100 - 0x017F（M0 - M1023）、0x0300 - 0x037F（M0 - M1023）
                    //}
                    data[2] = bs[2];//2
                    data[3] = bs[3];//3
                    data[4] = bs[0];//0
                    data[5] = bs[1];//1
                    break;
            }

            data[6] = 0x03;
            ushort sum = 0;
            for (int i = 1; i < 7; i++)
            {
                sum += data[i];
            }
            string sumStr = sum.ToString("X2");
            data[7] = (byte)sumStr[sumStr.Length - 2];
            data[8] = (byte)sumStr[sumStr.Length - 1];

            byte[] result = readbase(data);
            if (result[0] == 0x06)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public enum Typ { D, M, X, Y};
    }
}
