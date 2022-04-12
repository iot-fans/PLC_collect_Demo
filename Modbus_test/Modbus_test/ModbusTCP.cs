using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Modbus_test
{
    public class ModbusTCP : IDisposable
    {
        public ModbusTCP()
        {
            connected = false;
        }

        public bool connected;
        private Socket Msock;
        public byte[] sendmessage = null;
        public byte[] recvmessage = null;

        public void Dispose()
        {
            Disconnect();
        }

        public bool Disconnect()
        {
            if (connected)
            {
                if (Msock != null)
                {
                    Msock.Dispose();
                    Msock = null;
                }
                connected = false;
                return true;
            }
            return false;
        }

        public bool Connect(string ip, int port)
        {
            if (!connected)
            {
                try
                {
                    Msock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    Msock.NoDelay = true;
                    Msock.SendTimeout = 5000;
                    Msock.ReceiveTimeout = 5000;
                    //msock.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.11"), port));//指定本机地址及端口
                    Msock.Connect(ip, port);
                    connected = true;
                    return true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    throw ex;
                }
            }
            return false;
        }

        public byte Station = 0x02;//站号

        private int id = 0;

        public int ID
        {
            get
            {
                return id++;
            }
        }

        public enum Area
        {
            Coil = 0x01,
            Input = 0x02,
            Register = 0x03,
            InputRegister = 0x04,
            WriteCoil = 0x05,
            WriteRegister = 0x06
        }

        public byte[] Read(Area area,ushort address,int length)
        {
            try
            {
                byte[] data = null;
                //构建指令
                byte[] send = new byte[12];
                byte[] _id = BitConverter.GetBytes(ID);
                send[0] = _id[1];
                send[1] = _id[0];
                send[5] = 6;//信息长度
                send[6] = Station;
                send[7] = (byte)area;
                byte[] _adr = BitConverter.GetBytes(address);
                send[8] = _adr[1];
                send[9] = _adr[0];
                byte[] _length = BitConverter.GetBytes(length);
                send[10] = _length[1];
                send[11] = _length[0];
                sendmessage = send;
                //通讯
                byte[] receive = ReadBase(send);
                recvmessage = receive;
                if(receive[7]!=send[7])
                {
                    throw new Exception("响应异常 异常码:" + receive[8].ToString());
                }
                int datalen = receive[8];
                data = new byte[datalen];
                Array.Copy(receive, 9, data, 0, datalen);
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private byte[] ReadBase(byte[] send)
        {
            try
            {
                if(connected)
                {
                    Msock.Send(send);
                    List<byte> result = new List<byte>();
                    byte[] headbuffer = new byte[6];
                    int headnum = Msock.Receive(headbuffer);
                    if(headnum!=6)
                    {
                        throw new Exception("读取报头失败");
                    }
                    int bufferlen = headbuffer[4] * 256 + headbuffer[5];
                    byte[] buffer = new byte[bufferlen];
                    int buffernum = Msock.Receive(buffer);
                    if (buffernum != bufferlen)
                    {
                        throw new Exception("读取报文失败");
                    }
                    result.AddRange(headbuffer);
                    result.AddRange(buffer);
                    return result.ToArray();
                }
                else
                {
                    throw new Exception("网口未连接");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Write(Area area, ushort address, int value)
        {
            try
            {
                //构建指令
                byte[] send = new byte[12];
                byte[] _id = BitConverter.GetBytes(ID);
                send[0] = _id[1];
                send[1] = _id[0];
                send[5] = 6;//信息长度
                send[6] = Station;
                send[7] = (byte)area;
                byte[] _adr = BitConverter.GetBytes(address);
                send[8] = _adr[1];
                send[9] = _adr[0];
                byte[] _length = BitConverter.GetBytes(value);
                send[10] = _length[1];
                send[11] = _length[0];
                if (area == Area.WriteCoil)
                {
                    if(value > 0)
                    {
                        send[10] = 0xFF;
                        send[11] = 0x00;
                    }
                    else
                    {
                        send[10] = 0x00;
                        send[11] = 0x00;
                    }
                }
                sendmessage = send;
                //通讯
                byte[] receive = WriteBase(send);
                recvmessage = receive;
                if (receive[7] != send[7])
                {
                    throw new Exception("响应异常 异常码:"+receive[8].ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] WriteBase(byte[] send)
        {
            try
            {//发送

                if (connected)
                {
                    Msock.Send(send);
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
                byte[] frame = new byte[send.Length];
                int buffernum = Msock.Receive(frame);//接收报文
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
