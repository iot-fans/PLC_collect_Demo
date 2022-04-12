using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus_test
{
    public class ModbusRTU
    {
        public ModbusRTU()
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
                    serialPort1.ReadTimeout = 1000;
                    serialPort1.WriteTimeout = 1000;
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

        public byte[] Read(Area area, ushort address, int length)
        {
            try
            {
                byte[] Data = null;
                //构建指令
                List<byte> send = new List<byte>();
                send.Add(Station);
                send.Add((byte)area);
                send.Add(BitConverter.GetBytes(address)[1]);
                send.Add(BitConverter.GetBytes(address)[0]);
                send.Add(BitConverter.GetBytes(length)[1]);
                send.Add(BitConverter.GetBytes(length)[0]);
                byte[] CRC_send = Others.CalculateCrc(send.ToArray());
                send.AddRange(CRC_send);
                sendmessage = send.ToArray();
                //通讯
                byte[] receive = ReadBase(send.ToArray());
                recvmessage = receive;
                if (receive[0] != Station)
                {
                    throw new Exception("接收的站号不一致");
                }
                byte[] message = new byte[receive.Length - 2];
                Array.Copy(receive, message, receive.Length - 2);
                byte[] checkCRC = Others.CalculateCrc(message);
                if (checkCRC[0] != receive[receive.Length - 2] || checkCRC[1] != receive[receive.Length - 1])
                {
                    throw new Exception("接收校验错误");
                }
                int datalen = receive[2];
                //if(datalen!=length)
                //{
                //    throw new Exception("接收数据长度错误");//线圈8位寄存器一字
                //}
                Data = new byte[datalen];
                Array.Copy(receive, 3, Data, 0, datalen);
                return Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] ReadBase(byte[] send)
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
                byte[] frameStart = ReadBase(4);//报头
                byte[] frameEnd = ReadBase(frameStart[2] + 1);//剩余报文
                List<byte> frame = new List<byte>();
                frame.AddRange(frameStart);
                frame.AddRange(frameEnd);
                result = frame.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private byte[] ReadBase(int count)
        {
            byte[] frameBytes = new byte[count];
            int numBytesRead = 0;
            int noDataTimes = 0;
            while (numBytesRead != count)
            {
                int recvBytes = serialPort1.Read(frameBytes, numBytesRead, count - numBytesRead);
                numBytesRead += recvBytes;
                if (noDataTimes > 16)
                {
                    throw new Exception("Read resulted in 0 bytes returned.");
                }
                if (recvBytes == 0)
                {
                    noDataTimes++;
                    System.Threading.Thread.Sleep(10);
                    continue;
                }
                else
                {
                    noDataTimes = 0;
                }
            }
            return frameBytes;
        }

        public bool WriteCoil(ushort address,bool value)
        {
            bool success = false;
            try
            {
                //构建指令
                List<byte> send = new List<byte>();
                send.Add(Station);
                send.Add(0x05);
                send.Add(BitConverter.GetBytes(address)[1]);
                send.Add(BitConverter.GetBytes(address)[0]);
                send.Add((byte)(value ? 0xFF : 0x00));
                send.Add(0x00);
                byte[] CRC_send = Others.CalculateCrc(send.ToArray());
                send.AddRange(CRC_send);
                sendmessage = send.ToArray();
                //接收
                byte[] receive = WriteBase(send.ToArray());
                recvmessage = receive;
                if (receive[0] != Station)
                {
                    throw new Exception("接收的站号不一致");
                }
                byte[] message = new byte[receive.Length - 2];
                Array.Copy(receive, message, receive.Length - 2);
                byte[] checkCRC = Others.CalculateCrc(message);
                if (checkCRC[0] != receive[receive.Length - 2] || checkCRC[1] != receive[receive.Length - 1])
                {
                    throw new Exception("接收校验错误");
                }
                success = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return success;
        }

        public bool WriteRegister(ushort address,ushort value)
        {
            bool success = false;
            try
            {
                //构建指令
                List<byte> send = new List<byte>();
                send.Add(Station);
                send.Add(0x06);
                send.Add(BitConverter.GetBytes(address)[1]);
                send.Add(BitConverter.GetBytes(address)[0]);
                send.Add(BitConverter.GetBytes(value)[1]);
                send.Add(BitConverter.GetBytes(value)[0]);
                byte[] CRC_send = Others.CalculateCrc(send.ToArray());
                send.AddRange(CRC_send);
                sendmessage = send.ToArray();
                //接收
                byte[] receive = WriteBase(send.ToArray());
                recvmessage = receive;
                if (receive[0] != Station)
                {
                    throw new Exception("接收的站号不一致");
                }
                byte[] message = new byte[receive.Length - 2];
                Array.Copy(receive, message, receive.Length - 2);
                byte[] checkCRC = Others.CalculateCrc(message);
                if (checkCRC[0] != receive[receive.Length - 2] || checkCRC[1] != receive[receive.Length - 1])
                {
                    throw new Exception("接收校验错误");
                }
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        private byte[] WriteBase(byte[] send)
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
            {//读取
                byte[] frame = ReadBase(8);//接收报文
                result = frame;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
