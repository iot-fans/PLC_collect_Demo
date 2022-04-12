using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus_test
{
    public class ModbusASCII
    {
        public ModbusASCII()
        {
            serialPort1 = new System.IO.Ports.SerialPort();
        }

        public System.IO.Ports.SerialPort serialPort1;
        public byte[] sendmessage = null;
        public byte[] recvmessage = null;

        public bool Connect(string com, int BaudRate = 9600, int DataBits = 8, System.IO.Ports.StopBits StopBits = System.IO.Ports.StopBits.One, System.IO.Ports.Parity Parity = System.IO.Ports.Parity.Even)
        {
            try
            {
                //*** 设置端口参数*****//
                serialPort1.Close();
                serialPort1.BaudRate = BaudRate;
                serialPort1.DataBits = DataBits;
                serialPort1.StopBits = StopBits;
                serialPort1.Parity = Parity;
                serialPort1.PortName = com;
                //comport.Encoding = Encoding.ASCII;
                serialPort1.Open();//打开端口

                if (serialPort1.IsOpen)
                {
                    //   MessageBox.Show("打开" + com + "端口成功");
                    return true;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("端口打开失败！" + e.Message, "提示");
                throw e;
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
                    //       MessageBox.Show("关闭端口成功");
                    return true;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("端口关闭失败！" + e.Message, "提示");
                throw e;
            }
            return false;
        }

        public byte Station = 0x02;//站号

        public enum Area
        {
            Coil = 0x01,
            Input = 0x02,
            Register = 0x03,
            InputRegister = 0x04
        }

        public byte[] Read(Area area,ushort address,int length)
        {
            try
            {
                byte[] Data = null;
                //构建指令
                List<byte> send = new List<byte>();
                send.Add(0x3A);
                List<byte> rtuFrame = new List<byte>();
                rtuFrame.Add(Station);
                rtuFrame.Add((byte)area);
                rtuFrame.Add(BitConverter.GetBytes(address)[1]);
                rtuFrame.Add(BitConverter.GetBytes(address)[0]);
                rtuFrame.Add(BitConverter.GetBytes(address)[1]);
                rtuFrame.Add(BitConverter.GetBytes(address)[0]);
                byte LRC = Others.CalculateLrc(rtuFrame.ToArray());
                rtuFrame.Add(LRC);
                byte[] asciiFrame = Others.BytesToAsciiBytes(rtuFrame.ToArray());
                send.AddRange(asciiFrame);
                send.Add(0x0D);
                send.Add(0x0A);
                sendmessage = send.ToArray();
                //通讯
                byte[] receive = ReadBase(send.ToArray());
                recvmessage = receive;
                byte[] frame = new byte[receive.Length - 3];//去掉 0x3A  0x0D 0x0A
                Array.Copy(receive, 1, frame, 0, frame.Length);
                byte[] rtuframe = Others.AsciiBytesToBytes(frame);
                byte[] buf = new byte[length - 1];//去掉LRC,计算校验
                Array.Copy(rtuframe, 0, buf, 0, buf.Length);
                LRC = Others.CalculateLrc(buf);
                if(LRC != rtuframe[rtuframe.Length - 1])
                {
                    throw new Exception("接收校验错误");
                }
                if ((byte)area + 0x80 == rtuframe[1])
                {
                    throw new Exception("通讯发生错误");
                }
                Data = new byte[rtuframe.Length - 4];
                Array.Copy(rtuframe, 3, Data, 0, Data.Length);
                return Data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ReadBase(byte[] send)
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
            byte[] result = null;//返回结果
            try
            {//接收
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
                    if (headerIndex < 0)
                    {
                        for (int i = 0; i < instructionData.Count; i++)
                        {
                            if (instructionData[i] == 0x3A)
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
                            if (instructionData[i] == 0x0A && instructionData[i - 1] == 0x0D)
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
                    if (tailIndex <= instructionData.Count)
                    {
                        result = instructionData.ToArray();
                        return result;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
