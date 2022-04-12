using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Fins.IO
{
    public enum StopBits
    {
        //
        // 摘要: 
        //     使用一个停止位。
        One = 0,
        //
        // 摘要: 
        //     使用 1.5 个停止位。
        OnePointFive = 1,
        //
        // 摘要: 
        //     使用两个停止位。
        Two = 2,
    }

    public enum Parity
    {
        // 摘要: 
        //     不发生奇偶校验检查。
        None = 0,
        //
        // 摘要: 
        //     设置奇偶校验位，使位数等于奇数。
        Odd = 1,
        //
        // 摘要: 
        //     设置奇偶校验位，使位数等于偶数。
        Even = 2,
        //
        // 摘要: 
        //     将奇偶校验位保留为 1。
        Mark = 3,
        //
        // 摘要: 
        //     将奇偶校验位保留为 0。
        Space = 4,
    }

    /// <summary>
    /// 串口
    /// </summary>
    public class SerialPort : IDisposable
    {
        // 摘要: 
        //     指示不应该发生超时。
        public const int InfiniteTimeout = -1;

        public string PortNum = "COM1";//COM1,COM2......
        public int BaudRate = 9600;
        public int DataBits = 8;
        public Parity Parity; // 0-4=no,odd,even,mark,space    
        public StopBits StopBits; // 0,1,2 = 1, 1.5, 2    
        public int ReadTimeout = 1;//0=infinity
        public int WriteTimeout = 200;//0=infinity

        public SerialPort(string portNum)
        {
            PortNum = portNum;
        }

        //comm port win32 file handle    
        private int hComm = -1;

        public bool Opened = false;

        //win32 api constants    
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const int OPEN_EXISTING = 3;
        private const int INVALID_HANDLE_VALUE = -1;

        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            //taken from c struct in platform sdk    
            public int DCBlength; // sizeof(DCB)    
            public int BaudRate; // current baud rate    
            /* these are the c struct bit fields, bit twiddle flag to set  
            public int fBinary; // binary mode, no EOF check  
            public int fParity; // enable parity checking  
            public int fOutxCtsFlow; // CTS output flow control  
            public int fOutxDsrFlow; // DSR output flow control  
            public int fDtrControl; // DTR flow control type  
            public int fDsrSensitivity; // DSR sensitivity  
            public int fTXContinueOnXoff; // XOFF continues Tx  
            public int fOutX; // XON/XOFF out flow control  
            public int fInX; // XON/XOFF in flow control  
            public int fErrorChar; // enable error replacement  
            public int fNull; // enable null stripping  
            public int fRtsControl; // RTS flow control  
            public int fAbortOnError; // abort on error  
            public int fDummy2; // reserved  
            */
            public uint flags;
            public ushort wReserved; // not currently used    
            public ushort XonLim; // transmit XON threshold    
            public ushort XoffLim; // transmit XOFF threshold    
            public byte ByteSize; // number of bits/byte, 4-8    
            public byte Parity; // 0-4=no,odd,even,mark,space    
            public byte StopBits; // 0,1,2 = 1, 1.5, 2    
            public char XonChar; // Tx and Rx XON character    
            public char XoffChar; // Tx and Rx XOFF character    
            public char ErrorChar; // error replacement character    
            public char EofChar; // end of input character    
            public char EvtChar; // received event character    
            public ushort wReserved1; // reserved; do not use    
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        [DllImport("kernel32.dll")]
        private static extern int CreateFile(
        string lpFileName, // file name    
        uint dwDesiredAccess, // access mode    
        int dwShareMode, // share mode    
        int lpSecurityAttributes, // SD    
        int dwCreationDisposition, // how to create    
        int dwFlagsAndAttributes, // file attributes    
        int hTemplateFile // handle to template file    
        );

        [DllImport("kernel32.dll")]
        private static extern bool GetCommState(
        int hFile, // handle to communications device    
        ref DCB lpDCB // device-control block    
        );

        //Not found in kernel32.dll of windows 10
        //[DllImport("kernel32.dll")]
        //private static extern bool BuildCommDCB(
        //string lpDef, // device-control string    
        //ref DCB lpDCB // device-control block    
        //);

        [DllImport("kernel32.dll")]
        private static extern bool SetCommState(
        int hFile, // handle to communications device    
        ref DCB lpDCB // device-control block    
        );

        [DllImport("kernel32.dll")]
        private static extern bool SetupComm(
        int hFile, // handle to communications device    
        uint dwInQueue, // input buffer size
        uint dwOutQueue // output buffer size
        );

        private const uint PURGE_TXABORT = 0x0001;//Stop sending immediately though it is not completed.
        private const uint PURGE_RXABORT = 0x0002;//Stop receiving immediately though it is not completed.
        private const uint PURGE_TXCLEAR = 0x0004;//Clear output buffer.
        private const uint PURGE_RXCLEAR = 0x0008;//Clear input buffer.
        [DllImport("kernel32.dll")]
        private static extern bool PurgeComm(
        int hFile, // handle to communications device    
        uint dwFlags // operation type
        );

        [DllImport("kernel32.dll")]
        private static extern bool GetCommTimeouts(
        int hFile, // handle to comm device    
        ref COMMTIMEOUTS lpCommTimeouts // time-out values    
        );

        [DllImport("kernel32.dll")]
        private static extern bool SetCommTimeouts(
        int hFile, // handle to comm device    
        ref COMMTIMEOUTS lpCommTimeouts // time-out values    
        );

        [DllImport("kernel32.dll")]
        private static extern bool ReadFile(
        int hFile, // handle to file    
        byte[] lpBuffer, // data buffer    
        int nNumberOfBytesToRead, // number of bytes to read    
        ref int lpNumberOfBytesRead, // number of bytes read    
        ref OVERLAPPED lpOverlapped // overlapped buffer    
        );

        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(
        int hFile, // handle to file    
        byte[] lpBuffer, // data buffer    
        int nNumberOfBytesToWrite, // number of bytes to write    
        ref int lpNumberOfBytesWritten, // number of bytes written    
        ref OVERLAPPED lpOverlapped // overlapped buffer    
        );

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(
        int hObject // handle to object    
        );

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            DCB dcbCommPort = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();

            // OPEN THE COMM PORT.    
            hComm = CreateFile("\\\\.\\" + PortNum, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);

            // IF THE PORT CANNOT BE OPENED, BAIL OUT.    
            if (hComm == INVALID_HANDLE_VALUE)
            {
                throw (new ApplicationException(PortNum + " can not be opened."));
            }

            // SET THE COMM TIMEOUTS.    
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadTotalTimeoutConstant = ReadTimeout;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutConstant = WriteTimeout;
            SetCommTimeouts(hComm, ref ctoCommPort);

            // SET BAUD RATE, PARITY, WORD SIZE, AND STOP BITS.    
            GetCommState(hComm, ref dcbCommPort);
            dcbCommPort.BaudRate = BaudRate;
            dcbCommPort.flags = 0;
            //dcb.fBinary=1;    
            dcbCommPort.flags |= 1;
            if (Parity > 0)
            {
                //dcb.fParity=1    
                dcbCommPort.flags |= 2;
            }
            dcbCommPort.Parity = (byte)Parity;
            dcbCommPort.ByteSize = (byte)DataBits;
            dcbCommPort.StopBits = (byte)StopBits;
            if (!SetCommState(hComm, ref dcbCommPort))
            {
                Close();
                //uint ErrorNum=GetLastError();    
                throw (new ApplicationException(PortNum + " can not be initialized. (" + 
                    dcbCommPort.BaudRate.ToString() + "," + dcbCommPort.ByteSize.ToString() + "," + 
                    dcbCommPort.StopBits.ToString() + "," + dcbCommPort.Parity.ToString() + ")"));
            }
            //unremark to see if setting took correctly    
            //DCB dcbCommPort2 = new DCB();    
            //GetCommState(hComm, ref dcbCommPort2);    
            Opened = true;

        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                CloseHandle(hComm);
                hComm = INVALID_HANDLE_VALUE;
            }
        }

        public void Dispose()
        {
            Close();
        }

        //
        // 摘要: 
        //     从 System.IO.Ports.SerialPort 输入缓冲区读取一些字节并将那些字节写入字节数组中指定的偏移量处。
        //
        // 参数: 
        //   buffer:
        //     将输入写入到其中的字节数组。
        //
        //   offset:
        //     缓冲区数组中开始写入的偏移量。
        //
        //   count:
        //     要读取的字节数。
        //
        // 返回结果: 
        //     读取的字节数。
        //
        public int Read(byte[] buffer, int offset, int count)
        {
            byte[] BufBytes = new byte[count];
            int bytesRead = 0;
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                ReadFile(hComm, BufBytes, count, ref bytesRead, ref ovlCommPort);
                Array.Copy(BufBytes, 0, buffer, offset, bytesRead);
            }
            else
            {
                throw (new ApplicationException(PortNum + " is invalid."));
            }
            return bytesRead;
        }

        //
        // 摘要: 
        //     使用缓冲区的数据将指定数量的字节写入串行端口。
        //
        // 参数: 
        //   buffer:
        //     包含要写入端口的数据的字节数组。
        //
        //   offset:
        //     buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口。
        //
        //   count:
        //     要写入的字节数。
        //
        public void Write(byte[] buffer, int offset, int count)
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesWritten = 0;
                byte[] data = buffer;
                if (offset > 0)
                {
                    data = new byte[count];
                    Array.Copy(buffer, offset, data, 0, count);
                }
                WriteFile(hComm, data, count, ref BytesWritten, ref ovlCommPort);
            }
            else
            {
                throw (new ApplicationException(PortNum + " is invalid."));
            }
        }

        //
        // 摘要: 
        //     丢弃来自串行驱动程序的接收缓冲区的数据。
        //
        public void DiscardInBuffer()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(hComm, PURGE_RXCLEAR);
            }
            else
            {
                throw (new ApplicationException(PortNum + " is invalid."));
            }
        }
    }

}

