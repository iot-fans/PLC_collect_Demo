using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MC_test
{
    public class QsLsFXsAscTcp : IMelsecMaster
    {
        // Fields
        private const string CmdR = "0401";
        private const string CmdW = "1401";
        private const string DataLength = "0018";
        private EndPoint ep_PLC;
        private int i_LocalPortNumber = 0;
        private int i_RemotePortNumber = 0;
        private int i_SocketTimeout = 6000;
        private const string IONo = "03FF";
        private IPEndPoint ipep_PLC;
        private const short Maximum = 0x2fff;
        private const short MonitorTime = 0xa00;
        private const string NetworkNo = "00";
        private const string PCNo = "FF";
        private string s_PlcIPAddr = "";
        private Socket socket;
        private string sLocalHostAddress = "";
        private const string StationNo = "00";
        private const short Step = 0x162;
        private const string Subcmd = "0000";
        private const string SubHeader = "5000";

        // Methods
        public QsLsFXsAscTcp()
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

        private bool IsBitDevice(string devType)
        {
            devType = devType.ToUpper();
            switch (devType)
            {
                case "D":
                case "W":
                case "Z":
                case "R":
                case "ZR":
                case "SW":
                case "SD":
                    return false;
                case "X":
                case "Y":
                case "M":
                case "L":
                case "B":
                case "T":
                case "C":
                case "SB":
                case "SM":
                    return true;
                default:
                    throw new Exception("Unknown device type.");
            }
        }

        private bool IsHexAddrDevice(string devType)
        {
            devType = devType.ToUpper();
            switch (devType)
            {
                case "D":
                case "M":
                case "Z":
                case "R":
                case "ZR":
                case "SD":
                case "L":
                case "T":
                case "C":
                case "SM":
                    return false;
                case "W":
                case "SW":
                case "X":
                case "Y":
                case "B":
                case "SB":
                    return true;
                default:
                    throw new Exception("Unknown device type.");
            }
        }

        private byte[] BuildReadCmd(string devType, int firstDevIndex, short iStep)
        {
            string str = "500000FF03FF000018" + this.GetMonitorTime() + CmdR + Subcmd + this.GetStartDevice(devType, firstDevIndex) +
                this.GetRWRange(devType, iStep).ToString("X").PadLeft(4, '0');
            return Encoding.ASCII.GetBytes(str.ToCharArray());
        }

        private byte[] BuildWriteCmd(string devType, int firstDevIndex, short iStep, short[] iWData)
        {
            string str = "500000FF03FF00" + this.GetDataLength(iStep) + this.GetMonitorTime() + CmdW + Subcmd +
                this.GetStartDevice(devType, firstDevIndex) + iStep.ToString("X").PadLeft(4, '0') + this.GetWriteData(iWData);
            return Encoding.ASCII.GetBytes(str.ToCharArray());
        }

        private byte[] BuildWriteBitCmd(string devType, int firstDevIndex, short iStep, short[] iBData)
        {
            string str = "500000FF03FF00" + (24 + iStep).ToString("X").PadLeft(4, '0') + this.GetMonitorTime() + CmdW + "0001" +
                this.GetStartDevice(devType, firstDevIndex) + iStep.ToString("X").PadLeft(4, '0') + this.GetWriteBitData(iBData);
            return Encoding.ASCII.GetBytes(str.ToCharArray());
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
            int num = 0x18 + (iStep * 4);
            return num.ToString("X").PadLeft(4, '0');
        }

        private string GetMonitorTime()
        {
            return BitConverter.ToString(BitConverter.GetBytes((short)0xa00)).Replace("-", "");
        }

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
            string devIndex = "";
            if (IsHexAddrDevice(devType))
            {
                devIndex = ((ushort)firstDevIndex).ToString("X").PadLeft(6, '0');
            }
            else
            {
                devIndex = firstDevIndex.ToString().PadLeft(6, '0');
            }

            if (devType.Length == 1)
            {
                return (devType + "*" + devIndex);
            }

            if (devType.Length == 2)
            {
                return (devType + devIndex);
            }

            throw new Exception("Device type is wrong.");
        }

        private string GetWriteData(short[] iWData)
        {
            string str = "";
            for (int i = 0; i < iWData.Length; i++)
            {
                byte[] bytes = BitConverter.GetBytes(iWData[i]);
                Array.Reverse(bytes);
                str = str + BitConverter.ToString(bytes);
            }
            return str.Replace("-", "");
        }

        private string GetWriteBitData(short[] iBData)
        {
            string str = "";
            for (int i = 0; i < iBData.Length; i++)
            {
                if (iBData[i] > 0)
                {
                    str += '1';
                }
                else
                {
                    str += '0';
                }
            }
            return str;
        }

        public int Open()
        {
            try
            {
                this.ipep_PLC = new IPEndPoint(IPAddress.Parse(this.ActHostAddress), this.ActPortNumber);
                this.ep_PLC = this.ipep_PLC;
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (this.ActTimeOut <= 0)
                {
                    this.ActTimeOut = 3000;
                }
                this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, this.ActTimeOut);
                string str = "500000FF03FF000018000A04010000D*0000010001";
                byte[] buffer = new byte[0x20];
                this.socket.Connect(this.ipep_PLC);
                this.socket.Send(Encoding.ASCII.GetBytes(str.ToCharArray()));
                this.socket.Receive(buffer);
                if (!Encoding.ASCII.GetString(buffer).Contains("D00000FF03FF000008"))
                {
                    throw new Exception();
                }
                return 0;
            }
            catch (Exception)
            {
                if (this.socket != null)
                {
                    this.socket.Close();
                }
                return -9999;
            }
        }

        private void ClearSocketBuffer()
        {
            byte[] buffer = new byte[512];
            while (this.socket.Available > 0)
            {
                this.socket.Receive(buffer);
            }
        }

        public int ReadDeviceBlock(string devType, int firstDevIndex, int deviceCount, out short[] data)
        {
            short[] tempData = new short[GetRWRange(devType, (short)deviceCount)];
            data = tempData;
            int num2 = 0;
            if ((0x2fff >= firstDevIndex) && ((0x2fff - ((firstDevIndex + deviceCount) - 1)) < 0))
            {
                deviceCount = (0x2fff - firstDevIndex) + 1;
            }

            do
            {
                byte[] buffer;
                int insLen;
                string str2;
                int num4;
                if (deviceCount > 0x162)
                {
                    buffer = this.BuildReadCmd(devType, firstDevIndex, 0x162);
                    ClearSocketBuffer();
                    this.socket.Send(buffer);
                    buffer = new byte[0x59e];
                    insLen = 0;
                    while(true)//for (int i = 0; i < 8; i++)
                    {
                        insLen += this.socket.Receive(buffer, insLen, buffer.Length - insLen, SocketFlags.None);//Will throw SocketException when timeout
                        if (insLen >= 22)
                        {
                            string errCode = Encoding.ASCII.GetString(buffer, 18, 4);
                            if (errCode != "0000")
                            {
                                throw new Exception("Failed to read data. Error code: " + errCode);
                            }
                        }
                        if (buffer.Length <= insLen)
                        {
                            break;
                        }
                        //Thread.Sleep(20);
                    }
                    if (buffer.Length != insLen)
                    {
                        break;
                    }
                    str2 = Encoding.Default.GetString(buffer);
                    for (num4 = 0; num4 < 0x162; num4++)
                    {
                        data[num2 + num4] = Convert.ToInt16(str2.Substring((num4 * 4) + 0x16, 4), 0x10);
                    }
                    firstDevIndex = (short)(firstDevIndex + 0x162);
                    num2 += 0x162;
                    deviceCount -= 0x162;
                }
                if (deviceCount <= 0x162)
                {
                    buffer = this.BuildReadCmd(devType, firstDevIndex, (short)deviceCount);
                    ClearSocketBuffer();
                    this.socket.Send(buffer);
                    buffer = new byte[(tempData.Length * 4) + 0x16];
                    insLen = 0;
                    while (true)//for (int i = 0; i < 8; i++)
                    {
                        insLen += this.socket.Receive(buffer, insLen, buffer.Length - insLen, SocketFlags.None);//Will throw SocketException when timeout
                        if (insLen >= 22)
                        {
                            string errCode = Encoding.ASCII.GetString(buffer, 18, 4);
                            if (errCode != "0000")
                            {
                                throw new Exception("Failed to read data. Error code: " + errCode);
                            }
                        }
                        if (buffer.Length <= insLen)
                        {
                            break;
                        }
                        //Thread.Sleep(20);
                    }
                    if (buffer.Length != insLen)
                    {
                        break;
                    }
                    str2 = Encoding.Default.GetString(buffer);
                    if (buffer.Length == insLen)
                    {
                        for (num4 = 0; num4 < tempData.Length; num4++)
                        {
                            data[num2 + num4] = Convert.ToInt16(str2.Substring((num4 * 4) + 0x16, 4), 0x10);
                        }
                    }
                }
            }
            while (deviceCount > 0x162);

            if (IsBitDevice(devType))
            {
                data = new short[deviceCount];
                for (int i = 0; i < deviceCount; i++)
                {
                    data[i] = (short)((tempData[i / 16] >> (i % 16)) & 0x01);
                }
            }

            return 0;
        }

        public int WriteDeviceBlock(string devType, int firstDevIndex, int deviceCount, short[] data)
        {
            if (deviceCount > data.Length)
            {
                throw new Exception("Length of data is shorter than the length we want to access.");
            }

            int num2 = 0;
            if ((0x2fff >= firstDevIndex) && ((0x2fff - ((firstDevIndex + deviceCount) - 1)) < 0))
            {
                deviceCount = (0x2fff - firstDevIndex) + 1;
            }

            do
            {
                byte[] buffer;
                short[] numArray;
                int insLen;
                string str2;
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
                    ClearSocketBuffer();
                    this.socket.Send(buffer);
                    buffer = new byte[0x20];
                    insLen = this.socket.Receive(buffer);
                    str2 = Encoding.Default.GetString(buffer);
                    if ((insLen != 0x12) && (insLen != 0x16))
                    {
                        break;
                    }
                    firstDevIndex = (short)(firstDevIndex + 0x162);
                    num2 += 0x162;
                    deviceCount -= 0x162;
                }
                if (deviceCount <= 0x162)
                {
                    if (IsBitDevice(devType))
                    {
                        buffer = this.BuildWriteBitCmd(devType, firstDevIndex, (short)deviceCount, data);
                    }
                    else
                    {
                        buffer = this.BuildWriteCmd(devType, firstDevIndex, (short)deviceCount, data);
                    }
                    ClearSocketBuffer();
                    this.socket.Send(buffer);
                    buffer = new byte[512];
                    insLen = this.socket.Receive(buffer);
                    str2 = Encoding.Default.GetString(buffer);
                    if ((insLen != 0x12) && (insLen != 0x16))
                    {
                        break;
                    }
                }
            }
            while (deviceCount > 0x162);

            return 0;
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
