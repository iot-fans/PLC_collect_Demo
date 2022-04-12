using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace ppi_test
{
    class PPI
    {
        public PPI()
        {
            serialPort1 = new System.IO.Ports.SerialPort();
            //this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
        }

        public System.IO.Ports.SerialPort serialPort1;
        public byte[] Data;
        public int time = 0;
        public byte num;//批次
        public string send_string = "";
        public string recv_string = "";

        public bool connect(string com, Int16 BaudRate = 9600)
        {
            try
            {
                //*** 设置端口参数*****//
                serialPort1.Close();
                serialPort1.BaudRate = BaudRate;
                serialPort1.DataBits = 8;
                serialPort1.StopBits = System.IO.Ports.StopBits.One;
                serialPort1.Parity = System.IO.Ports.Parity.Even;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tp"></param>
        /// <param name="sz"></param>
        /// <param name="Address"></param>
        /// <param name="Length">小于等于32</param>
        /// <param name="StationNO"></param>
        /// <returns></returns>
        public byte[] Read(Typ tp = Typ.Q, Siz sz = Siz.bit, int Address = 0, int Length = 1, int StationNO = 2)
        {
            try
            {
                this.Data = null;
                time = 0;//计时
                int Elapsed = Environment.TickCount;//计时
                byte[] data = BuildReadCmd(tp, sz, Address, Length, StationNO);
                send_string = BitConverter.ToString(data).Replace("-", " ");//发送信息字符串
                byte[] First = ReadBase(serialPort1, data);//第一次发送读取
                if (First[0] == 0xE5)
                {
                    byte[] executeConfirm = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };
                    byte[] Second = ReadBase(serialPort1, executeConfirm);//第二次发送肯定
                    if (Second.Length < 25)
                    {
                        throw new Exception("");
                    }
                    else
                    {//处理
                        if (Second[17] != 0x00 && Second[18] != 0x00)
                        {
                            switch (Second[18])
                            {
                                case 0x80:
                                    //为请求的操作切换错误的位置
                                    throw new Exception("为请求的操作切换错误的位置");
                                case 0x81:
                                    //命令中有其他结构错误错误。CPU不支持该命令
                                    throw new Exception("命令中有其他结构错误错误。CPU不支持该命令");
                                case 0x84:
                                    //由于系统故障，CPU正忙于处理上载或下载CPU无法处理命令
                                    throw new Exception("由于系统故障，CPU正忙于处理上载或下载CPU无法处理命令");
                                case 0x85:
                                    //长度字段不正确或与接收到的数据量不一致
                                    throw new Exception("长度字段不正确或与接收到的数据量不一致");
                                case 0xD2:
                                    //上载或下载命令错误
                                    throw new Exception("上载或下载命令错误");
                                case 0xD6:
                                    //保护错误（密码）
                                    throw new Exception("保护错误（密码）");
                                case 0xDC:
                                    //时间时钟数据错误
                                    throw new Exception("时间时钟数据错误");
                            }
                        }
                        if (Second[21] == 0xFF)
                        {
                            this.Data = new byte[Second.Length - 27];
                            Array.Copy(Second, 25, Data, 0, Data.Length);
                            recv_string = BitConverter.ToString(Second).Replace("-", " ");//接收信息字符串
                        }
                        else
                        {
                            switch (Second[21])
                            {
                                case 0x01:
                                    //硬件故障
                                    throw new Exception("硬件故障");
                                case 0x03:
                                    //非法对象访问
                                    throw new Exception("非法对象访问");
                                case 0x05:
                                    //地址无效（变量地址错误）
                                    throw new Exception("地址无效（变量地址错误）");
                                case 0x06:
                                    //数据类型不支持
                                    throw new Exception("数据类型不支持");
                                case 0x0A:
                                    //对象不存在或长度错误
                                    throw new Exception("对象不存在或长度错误");
                            }
                        }
                    }
                }
                time = Environment.TickCount - Elapsed;
                return this.Data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ReadBase(SerialPort COM, byte[] send)
        {
            try
            {//发送

                if (COM.IsOpen)
                {
                    COM.Write(send, 0, send.Length);
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
                byte[] buffer = new byte[512];
                List<byte> instructionData = new List<byte>();
                int count = 0;
                int noDataTimes = 0;
                int headerIndex = -1;//开头字符
                int tailIndex = -1;//结束定界符
                int len = 0;
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //DateTime start = DateTime.Now;
                //while (true)
                //{
                //    Thread.Sleep(20);
                //    try
                //    {
                //        if (COM.BytesToRead < 1)
                //        {
                //            if ((DateTime.Now - start).TotalMilliseconds > 5000)//超时
                //            {
                //                ms.Dispose();
                //                //return new OperateResult<byte[]>(string.Format("Time out: {0}", ReceiveTimeout));
                //            }
                //            else if (ms.Length > 0)
                //            {
                //                break;
                //            }
                //            else if (true)//一定要接收到数据（可修改）
                //            {
                //                continue;
                //            }
                //            else
                //            {
                //                //break;
                //            }
                //        }

                //        // 继续接收数据
                //        int sp_receive = COM.Read(buffer, 0, buffer.Length);
                //        ms.Write(buffer, 0, sp_receive);
                //    }
                //    catch (Exception ex)
                //    {
                //        ms.Dispose();
                //        throw ex;
                //    }
                //}
                //result = ms.ToArray();
                //ms.Dispose();
                while (true)
                {
                    count = COM.Read(buffer, 0, buffer.Length);
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
                    if(cmd == 0xE5)
                    {
                        result = new byte[] { cmd };
                        instructionData.RemoveAt(0);
                        return result;
                    }
                    if (headerIndex < 0)
                    {
                        for (int i = 0; i < instructionData.Count; i++)
                        {
                            if (instructionData[i] == 0x68)
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
                    if(headerIndex == 0&&instructionData.Count>=4)
                    {
                        if (instructionData[3] == 0x68)
                        {
                            len = instructionData[1];
                        }
                    }
                    if (tailIndex < 0)
                    {
                        for (int i = 0; i < instructionData.Count; i++)
                        {
                            if (instructionData[i] == 0x16 && i == len+5)
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
                                len = 0;
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
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public byte[] BuildReadCmd(Typ tp = Typ.Q, Siz sz = Siz.bit, int Address = 0, int Length = 1, int StationNO = 2)
        {
            byte[] data = new byte[33];
            data[0] = 0x68;
            data[1] = 0x1B;
            data[2] = 0x1B;
            data[3] = 0x68;
            data[4] = (byte)StationNO;
            data[5] = 0x00;
            data[6] = 0x6C;
            data[7] = 0x32;
            data[8] = 0x01;
            data[9] = 0x00;
            data[10] = 0x00;
            data[11] = 0x00;
            data[12] = num; num++;// 0x00;//批次
            data[13] = 0x00;
            data[14] = 0x0E;
            data[15] = 0x00;
            data[16] = 0x00;
            data[17] = 0x04;
            data[18] = 0x01;
            data[19] = 0x12;
            data[20] = 0x0A;
            data[21] = 0x10;
            switch (sz)
            {
                case Siz.bit:
                    data[22] = 0x01;//(byte)sz; //长度 
                    break;
                case Siz.B:
                    data[22] = 0x02;//Siz.B 2  Siz.W 4  Siz.D 6
                    break;
                case Siz.W:
                    data[22] = 0x04;//Siz.B 2  Siz.W 4  Siz.D 6
                    break;
                case Siz.D:
                    data[22] = 0x06;//Siz.B 2  Siz.W 4  Siz.D 6
                    break;
                default:
                    break;
            }
            switch (tp)
            {
                case Typ.T:
                    data[22] = 0x1F;
                    break;
                case Typ.C:
                    data[22] = 0x1E;
                    break;
                case Typ.HC:
                    data[22] = 0x20;
                    break;
                default:
                    break;
            }
            data[23] = 0x00;
            data[24] = BitConverter.GetBytes(Length)[0];//个数//小于0xDE//Siz.B
            if (Siz.bit == sz)
                data[24] = 0x01;
            data[25] = 0x00;
            if (Typ.V == tp)
            { data[26] = 0x01; }//存储器类型
            else
            { data[26] = 0x00; }//存储器类型
            data[27] = (byte)tp;//存储器类型
            data[28] = 0x00;
            if (Siz.bit == sz)
            {
                Address = Address / 10 * 8 + Address % 10;//八进制换成十进制
                data[29] = Convert.ToByte(Address / 256);
                data[30] = Convert.ToByte(Address % 256);

            }
            else
            {
                data[29] = Convert.ToByte(Address * 8 / 256);
                data[30] = Convert.ToByte(Address * 8 % 256);

            }
            if (tp == Typ.T || tp == Typ.C || tp == Typ.HC)//
            {
                data[29] = Convert.ToByte(Address / 256);
                data[30] = Convert.ToByte(Address % 256);
            }
            int j = 0;
            for (int i = 4; i <= 30; i++)
            {
                j = j + Convert.ToInt32(data[i]);
            }
            data[31] = Convert.ToByte(j % 256);
            data[32] = 0x16;
            return data;
        }

        public void Write(Typ tp = Typ.Q, Siz sz = Siz.bit, int Address = 0, Int32 Value = 1, int StationNO = 2)
        {
            time = 0;//计时
            int Elapsed = Environment.TickCount;//计时
            byte[] data = BuildWriteCmd(tp, sz, Address, Value, StationNO);
            send_string = BitConverter.ToString(data).Replace("-", " ");//发送信息字符串
            byte[] First = ReadBase(serialPort1, data);//第一次发送写入
            if (First[0] == 0xE5)
            {
                byte[] executeConfirm = new byte[] { 0x10, 0x02, 0x00, 0x5C, 0x5E, 0x16 };
                byte[] Second = ReadBase(serialPort1, executeConfirm);//第二次发送肯定
                if (Second.Length == 24)
                {
                    bool write_ok = data[21] == 0xFF ? true : false;
                    recv_string = BitConverter.ToString(Second).Replace("-", " ");//接收信息字符串
                }
            }
            time = Environment.TickCount - Elapsed;
        }

        public byte[] BuildWriteCmd(Typ tp = Typ.Q, Siz sz = Siz.bit, int Address = 0, Int32 Value = 1, int StationNO = 2)
        {
            byte[] data;
            int j;
            if (Siz.W == sz)
            {
                data = new byte[39];
                data[1] = 0x21;
                data[2] = 0x21;
            }
            else
            {
                if (Siz.D == sz)
                {
                    data = new byte[41];
                    data[1] = 0x23;
                    data[2] = 0x23;
                }
                else
                {
                    data = new byte[38];
                    data[1] = 0x20;
                    data[2] = 0x20;
                }
            }
            data[0] = 0x68;
            data[3] = 0x68;
            data[4] = (byte)StationNO; //站号
            data[5] = 0x00;
            data[6] = 0x7C; //功能码(写入)
            data[7] = 0x32;
            data[8] = 0x01;
            data[9] = 0x00;
            data[10] = 0x00;
            data[11] = 0x00;
            data[12] = num; num++;// 0x00;//批次
            data[13] = 0x00;
            data[14] = 0x0E;
            data[15] = 0x00;
            data[16] = 0x05;//0x06;//数据区长度
            data[17] = 0x05;
            data[18] = 0x01;
            data[19] = 0x12;
            data[20] = 0x0A;
            data[21] = 0x10;
            data[22] = 0x02;//(byte)sz; //长度 
            data[23] = 0x00;
            data[24] = 0x01;
            data[25] = 0x00;
            if (Typ.V == tp)
            { data[26] = 0x01; }//存储器类型
            else
            { data[26] = 0x00; }//存储器类型
            data[27] = (byte)tp;//存储器类型
            data[28] = 0x00;
            data[31] = 0x00;
            data[33] = 0x00;

            if (Siz.bit == sz)
            {
                Address = Address / 10 * 8 + Address % 10;
                data[29] = Convert.ToByte(Address / 256);
                data[30] = Convert.ToByte(Address % 256);
                data[32] = 0x03;
            }
            else
            {
                data[29] = Convert.ToByte(Address * 8 / 256);
                data[30] = Convert.ToByte(Address * 8 % 256);
                data[32] = 0x04;
            }

            if (Siz.W == sz)
            {

                data[34] = 0x10;//位数 
                data[35] = Convert.ToByte(Value / 256);
                data[36] = Convert.ToByte(Value % 256);
                j = 0;
                for (int i = 4; i <= 36; i++)
                    j = j + data[i];
                data[37] = Convert.ToByte(j % 256);
                data[38] = 0x16;

            }
            else
            {
                if (Siz.D == sz)
                {
                    data[34] = 0x20;//位数 
                    data[35] = Convert.ToByte((Value >> 24) % 256);
                    data[36] = Convert.ToByte((Value >> 16) % 256);
                    data[37] = Convert.ToByte((Value >> 8) % 256);
                    data[38] = Convert.ToByte(Value % 256);
                    j = 0;
                    for (int i = 4; i <= 38; i++)
                        j = j + data[i];
                    data[39] = Convert.ToByte(j % 256);
                    data[40] = 0x16;
                }
                else
                {
                    if (Siz.bit == sz) { data[34] = 0x01; }
                    if (Siz.B == sz) { data[34] = 0x08; }
                    //data[35] = (byte)Value;
                    //data[36] = 0x00;
                    //j = 0;
                    //for (int i = 4; i <= 36; i++)
                    //    j = j + data[i];
                    //data[37] = Convert.ToByte(j % 256);
                    //data[38] = 0x16;
                    data[35] = (byte)Value;

                    //效验和
                    j = 0;
                    for (int i = 4; i <= 35; i++)
                        j = j + data[i];
                    data[36] = Convert.ToByte(j % 256);
                    data[37] = 0x16;
                }
            }
            return data;
        }

        public enum Typ
        {
            S = 0x04, SM = 0x05,
            AI = 0x06, AQ = 0x07,
            C = 0x1E, T = 0x1F, HC = 0x20,
            I = 0x81, Q = 0x82,
            M = 0x83, V = 0x84
        };
        public enum Siz { bit = 0x01, B = 0x02, W = 0x04, D = 0x06 };
    }
}
