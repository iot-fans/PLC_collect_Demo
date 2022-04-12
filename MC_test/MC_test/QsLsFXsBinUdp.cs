using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

namespace MC_test
{
    public class QsLsFXsBinUdp : IMelsecMaster
    {
        // Fields
        private const string CmdR = "01-04-";
        private const string CmdW = "01-14-";
        private const string DataLength = "0C-00-";
        private EndPoint ep_PLC;
        private int i_LocalPortNumber;
        private int i_RemotePortNumber;
        private int i_SocketTimeout;
        private const string IONo = "FF-03-";
        private IPEndPoint ipep_PLCR;
        private IPEndPoint ipep_PLCS;
        private const short Maximum = 0x2fff;
        private const short MonitorTime = 0xa00;
        private const string NetworkNo = "00-";
        private const string PCNo = "FF-";
        private string s_PlcIPAddr;
        private Socket socket;
        private string sLocalHostAddress;
        private const string StationNo = "00-";
        private const ushort Step = 0x162;
        private const string Subcmd = "00-00-";
        private const string SubHeader = "50-00-";
        private const string SubHeaderR = "01-";
        private const string SubHeaderW = "03-";
        private const string TargetStation = "FF-";

        // Methods
        public QsLsFXsBinUdp()
        {
            this.i_SocketTimeout = 0;
            this.i_LocalPortNumber = 0;
            this.i_RemotePortNumber = 0;
            this.s_PlcIPAddr = "";
            this.sLocalHostAddress = "";
        }

        public void Dispose()
        {
            if (socket != null)
            {
                using (socket) { }
                socket = null;
            }
            GC.SuppressFinalize(this);
        }

        private int BinaryToInteger(string binaryString, int bits)
        {
            int num3;
            int num = 0;
            int num2 = 1;
            bits--;
            if (binaryString[0] == '0')
            {
                for (num3 = bits; num3 > 0; num3--)
                {
                    if (binaryString[num3] == '1')
                    {
                        num += num2;
                    }
                    num2 *= 2;
                }
                return num;
            }
            for (num3 = bits; num3 > 0; num3--)
            {
                if (binaryString[num3] == '0')
                {
                    num -= num2;
                }
                num2 *= 2;
            }
            num--;
            return num;
        }

        private bool IsBitDevice(string devType)
        {
            devType = devType.ToUpper();
            switch (devType)
            {
                case "D":
                case "W":
                case "G":
                case "Z":
                case "R":
                case "ZR":
                case "SN":
                case "CN":
                case "SW":
                case "SD":
                    return false;
                case "X":
                case "Y":
                case "M":
                case "L":
                case "F":
                case "V":
                case "B":
                case "S":
                case "TS":
                case "TC":
                case "SS":
                case "SC":
                case "CS":
                case "CC":
                case "SB":
                case "SM":
                    return true;
                default:
                    throw new Exception("Unknown device type.");
            }
        }

        private byte[] BuildReadCmd(string devType, int firstDevIndex, short iStep)
        {
            string ins = "50-00-00-FF-FF-03-00-0C-00-" + this.GetMonitorTime() + CmdR + Subcmd + this.GetStartDevice(devType, firstDevIndex) +
                BitConverter.ToString(BitConverter.GetBytes(this.GetRWRange(devType, iStep)));
            string[] strArray = (ins).Split(new char[] { '-' });
            byte[] buffer = new byte[strArray.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(strArray[i], 0x10);
            }
            return buffer;
        }

        private byte[] BuildWriteCmd(string devType, int firstDevIndex, short iStep, short[] iWData)
        {
            string ins = "50-00-00-FF-FF-03-00-" + this.GetDataLength(iStep) + this.GetMonitorTime() + CmdW + Subcmd +
                this.GetStartDevice(devType, firstDevIndex) +
                BitConverter.ToString(BitConverter.GetBytes(this.GetRWRange(devType, iStep))) + this.GetWriteData(iWData);
            string[] strArray = (ins).Split(new char[] { '-' });
            byte[] buffer = new byte[strArray.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(strArray[i], 0x10);
            }
            return buffer;
        }

        private byte[] BuildWriteBitCmd(string devType, int firstDevIndex, short iStep, short[] iWData)
        {
            string ins = "50-00-00-FF-FF-03-00-" + (12 + ((iStep - 1)/ 2 + 1)).ToString("X") + this.GetMonitorTime() + CmdW + "01-00-" +
                this.GetStartDevice(devType, firstDevIndex) +
                BitConverter.ToString(BitConverter.GetBytes(iStep)) + this.GetWriteBitData(iWData);
            string[] strArray = (ins).Split(new char[] { '-' });
            byte[] buffer = new byte[strArray.Length];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(strArray[i], 0x10);
            }
            return buffer;
        }

        public int Close()
        {
            try
            {
                this.socket.Close();
                return 0;
            }
            catch (Exception)
            {
                return -9999;
            }
        }

        private string GetDataLength(short iStep)
        {
            string str = (12 + (iStep * 2)).ToString("X");
            if (str.Length < 4)
            {
                int num = 4 - str.Length;
                for (int i = 0; i < num; i++)
                {
                    str = "0" + str;
                }
            }
            return (str.Substring(2, 2) + "-" + str.Substring(0, 2) + "-");
        }

        private string GetMonitorTime()
        {
            byte[] bytes = BitConverter.GetBytes((short)0xa00);
            Array.Reverse(bytes);
            return (BitConverter.ToString(bytes) + "-");
        }

        //private short GetRWRange(string devType, int firstDevIndex, short iStep)
        //{
        //    if (IsBitDevice(devType))
        //    {
        //        int cnt1 = firstDevIndex / 16;
        //        int cnt2 = (firstDevIndex + iStep - 1) / 16;
        //        return ((short)(cnt2 + 1 - cnt1));
        //    }
        //    return iStep;
        //}

        private short GetRWRange(string devType, short iStep)
        {
            if (IsBitDevice(devType))
            {
                return ((short)((iStep - 1) / 16 + 1));
            }
            return iStep;
        }

        private string GetStartDevice(string devType, int firstDevIndex)
        {
            string ins = "";
            if (devType.Equals("D")) ins = "-00-A8-";
            if (devType.Equals("W")) ins = "-00-B4-";
            if (devType.Equals("M")) ins = "-00-90-";
            if (devType.Equals("L")) ins = "-00-92-";
            if (devType.Equals("S")) ins = "-00-98-";
            if (devType.Equals("X")) ins = "-00-9C-";
            if (devType.Equals("Y")) ins = "-00-9D-";
            if (devType.Equals("Z")) ins = "-00-CC-";
            return (BitConverter.ToString(BitConverter.GetBytes((short)firstDevIndex)) + ins);
        }

        private string GetWriteData(short[] iWData)
        {
            string str = "";
            for (int i = 0; i < iWData.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(iWData[i]);
                str = str + "-" + BitConverter.ToString(bytes);
            }
            return str;
        }

        private string GetWriteBitData(short[] iWData)
        {
            string str = "";
            byte[] bytes = new byte[1];
            for (int i = 0; i < iWData.Length / 2; i++)
            {
                bytes[0] = 0;
                if (iWData[i * 2] > 0)
                {
                    bytes[0] = 0x10;
                }
                if (iWData[i * 2 + 1] > 0)
                {
                    bytes[0] += 0x01;
                }
                str = str + "-" + BitConverter.ToString(bytes);
            }
            if (iWData.Length % 2 > 0)
            {
                if (iWData[iWData.Length - 1] > 0)
                {
                    str += "-10";
                }
                else
                {
                    str += "-00";
                }
            }
            return str;
        }

        public int Open()
        {
            try
            {
                this.ipep_PLCS = new IPEndPoint(IPAddress.Parse(this.ActHostAddress), this.ActPortNumber);
                this.ipep_PLCR = new IPEndPoint(IPAddress.Parse(this.LocalHostAddress), this.LocalPortNumber);
                this.ep_PLC = this.ipep_PLCS;
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                this.socket.Bind(this.ipep_PLCR);
                if (this.ActTimeOut <= 0)
                {
                    this.ActTimeOut = 3000;
                }
                this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, this.ActTimeOut);
                byte[] buffer = this.BuildReadCmd("D", 1, 1);
                byte[] buffer2 = new byte[13];
                this.socket.SendTo(buffer, this.ipep_PLCS);
                if (this.socket.ReceiveFrom(buffer2, ref this.ep_PLC) != 13)
                {
                    throw new Exception();
                }
                return 0;
            }
            catch (Exception)
            {
                this.socket.Close();
                return -9999;
            }
        }

        public int ReadDeviceBlock(string devType, int firstDevIndex, int deviceCount, out short[] data)
        {
            data = new short[GetRWRange(devType, (short)deviceCount)];
            int num2 = 0;
            if ((0x2fff >= firstDevIndex) && ((0x2fff - ((firstDevIndex + deviceCount) - 1)) < 0))
            {
                deviceCount = (0x2fff - firstDevIndex) + 1;
            }
            try
            {
                do
                {
                    byte[] buffer;
                    int num4;
                    if (deviceCount > 0x162)
                    {
                        buffer = this.BuildReadCmd(devType, firstDevIndex, 0x162);
                        this.socket.SendTo(buffer, buffer.Length, SocketFlags.None, this.ipep_PLCS);
                        buffer = new byte[0x2cf];
                        if (this.socket.ReceiveFrom(buffer, ref this.ep_PLC) != buffer.Length)
                        {
                            break;
                        }
                        for (num4 = 0; num4 < 0x162; num4++)
                        {
                            string str2 = Convert.ToString(buffer[(num4 * 2) + 11], 2).PadLeft(8, '0');
                            string str3 = Convert.ToString(buffer[(num4 * 2) + 12], 2).PadLeft(8, '0');
                            data[num2 + num4] = (short)this.BinaryToInteger(str3 + str2, 0x10);
                        }
                        firstDevIndex = (short)(firstDevIndex + 0x162);
                        num2 += 0x162;
                        deviceCount -= 0x162;
                    }
                    if (deviceCount <= 0x162)
                    {
                        buffer = this.BuildReadCmd(devType, firstDevIndex, (short)deviceCount);
                        this.socket.SendTo(buffer, buffer.Length, SocketFlags.None, this.ipep_PLCS);
                        buffer = new byte[(deviceCount * 2) + 11];
                        if (this.socket.ReceiveFrom(buffer, ref this.ep_PLC) == buffer.Length)
                        {
                            for (num4 = 0; num4 < deviceCount; num4++)
                            {
                                string str2 = Convert.ToString(buffer[(num4 * 2) + 11], 2).PadLeft(8, '0');
                                string str3 = Convert.ToString(buffer[(num4 * 2) + 12], 2).PadLeft(8, '0');
                                data[num2 + num4] = (short)this.BinaryToInteger(str3 + str2, 0x10);
                            }
                        }
                    }
                }
                while (deviceCount > 0x162);

                //if (IsBitDevice(devType))
                //{
                //    data = new short[deviceCount];
                //    int offset = firstDevIndex % 16;
                //    for (int i = 0; i < deviceCount; i++)
                //    {
                //        data[i] = (short)((tempData[(offset + i) / 16] >> ((offset + i) % 16)) & 0x01);
                //    }
                //}

                if (IsBitDevice(devType))
                {
                    byte[] byteData = new byte[data.Length * 2];
                    for (int k = 0; k < data.Length; k++)
                    {
                        byte[] bs = BitConverter.GetBytes(data[k]);
                        byteData[k * 2] = bs[0];
                        byteData[k * 2 + 1] = bs[1];
                    }
                    data = new short[deviceCount];
                    for (int i = 0; i < deviceCount; i++)
                    {
                        data[i] = (short)((byteData[i / 8] >> (i % 8)) & 0x01);
                    }
                }

                return 0;
            }
            catch (Exception)
            {
                return -9999;
            }
        }

        public int WriteDeviceBlock(string devType, int firstDevIndex, int deviceCount, short[] data)
        {
            if (deviceCount >= data.Length)
            {
                throw new Exception("Length of data is shorter than the length we want to access.");
            }

            int num2 = 0;
            if ((0x2fff >= firstDevIndex) && ((0x2fff - ((firstDevIndex + deviceCount) - 1)) < 0))
            {
                deviceCount = (0x2fff - firstDevIndex) + 1;
            }
            try
            {
                do
                {
                    byte[] buffer;
                    short[] numArray;
                    if (deviceCount > 0x162)
                    {
                        numArray = new short[0x162];
                        for (int j = 0; j < 0x162; j++)
                        {
                            numArray[j] = data[num2 + j];
                        }
                        if (IsBitDevice(devType))
                        {
                            buffer = this.BuildWriteBitCmd(devType, firstDevIndex, 0x162, numArray);
                        }
                        else
                        {
                            buffer = this.BuildWriteCmd(devType, firstDevIndex, 0x162, numArray);
                        }
                        this.socket.SendTo(buffer, buffer.Length, SocketFlags.None, this.ipep_PLCS);
                        buffer = new byte[20];
                        if (this.socket.ReceiveFrom(buffer, ref this.ep_PLC) != 11)
                        {
                            break;
                        }
                        firstDevIndex = (short)(firstDevIndex + 0x162);
                        num2 += 0x162;
                        deviceCount -= 0x162;
                    }
                    if (deviceCount <= 0x162)
                    {
                        numArray = new short[deviceCount];
                        for (int k = 0; k < deviceCount; k++)
                        {
                            numArray[k] = data[num2 + k];
                        }
                        if (IsBitDevice(devType))
                        {
                            buffer = this.BuildWriteBitCmd(devType, firstDevIndex, (short)deviceCount, numArray);
                        }
                        else
                        {
                            buffer = this.BuildWriteCmd(devType, firstDevIndex, (short)deviceCount, numArray);
                        }
                        this.socket.SendTo(buffer, buffer.Length, SocketFlags.None, this.ipep_PLCS);
                        buffer = new byte[20];
                        if (this.socket.ReceiveFrom(buffer, ref this.ep_PLC) != 11)
                        {
                        }
                    }
                }
                while (deviceCount > 0x162);
                return 0;
            }
            catch (Exception)
            {
                return -9999;
            }
        }

        // Properties
        public int ActPortNumber
        {
            get
            {
                return this.i_RemotePortNumber;
            }
            set
            {
                if (this.i_RemotePortNumber != value)
                {
                    this.i_RemotePortNumber = value;
                }
            }
        }

        public string ActHostAddress
        {
            get
            {
                return this.s_PlcIPAddr;
            }
            set
            {
                if (!this.s_PlcIPAddr.Equals(value))
                {
                    this.s_PlcIPAddr = value;
                }
            }
        }

        public int ActTimeOut
        {
            get
            {
                return this.i_SocketTimeout;
            }
            set
            {
                if (this.i_SocketTimeout != value)
                {
                    this.i_SocketTimeout = value;
                }
            }
        }

        public int LocalPortNumber
        {
            get
            {
                return this.i_LocalPortNumber;
            }
            set
            {
                if (this.i_LocalPortNumber != value)
                {
                    this.i_LocalPortNumber = value;
                }
            }
        }

        public string LocalHostAddress
        {
            get
            {
                return this.sLocalHostAddress;
            }
            set
            {
                this.sLocalHostAddress = value;
            }
        }
    }
}
