using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HslCommunication.Language
{
    /// <summary>
    /// English Version Text
    /// </summary>
    public class English : DefaultLanguage
    {
#pragma warning disable 1591//CS1591 // 缺少对公共可见类型或成员的 XML 注释
        /***********************************************************************************
         * 
         *    Normal Info
         * 
         ************************************************************************************/

        public override string AuthorizationFailed { get { return "System authorization failed, need to use activation code authorization, thank you for your support."; } }
        public override string ConnectedFailed { get { return "Connected Failed: "; } }
        public override string ConnectedSuccess { get { return "Connect Success !"; } }
        public override string UnknownError { get { return "Unknown Error"; } }
        public override string ErrorCode { get { return "Error Code: "; } }
        public override string TextDescription { get { return "Description: "; } }
        public override string ExceptionMessage { get { return "Exception Info: "; } }
        public override string ExceptionSourse { get { return "Exception Sourse："; } }
        public override string ExceptionType { get { return "Exception Type："; } }
        public override string ExceptionStackTrace { get { return "Exception Stack: "; } }
        public override string ExceptopnTargetSite { get { return "Exception Method: "; } }
        public override string ExceprionCustomer { get { return "Error in user-defined method: "; } }
        public override string SuccessText { get { return "Success"; } }
        public override string TwoParametersLengthIsNotSame { get { return "Two Parameter Length is not same"; } }
        public override string NotSupportedDataType { get { return "Unsupported DataType, input again"; } }
        public override string NotSupportedFunction { get { return "The current feature logic does not support"; } }
        public override string DataLengthIsNotEnough { get { return "Receive length is not enough，Should:{0},Actual:{1}"; } }
        public override string ReceiveDataTimeout { get { return "Receive timeout: "; } }
        public override string ReceiveDataLengthTooShort { get { return "Receive length is too short: "; } }
        public override string MessageTip { get { return "Message prompt:"; } }
        public override string Close { get { return "Close"; } }
        public override string Time { get { return "Time:"; } }
        public override string SoftWare { get { return "Software:"; } }
        public override string BugSubmit { get { return "Bug submit"; } }
        public override string MailServerCenter { get { return "Mail Center System"; } }
        public override string MailSendTail { get { return "Mail Service system issued automatically, do not reply"; } }
        public override string IpAddresError { get { return "IP address input exception, format is incorrect"; } }
        public override string Send { get { return "Send"; } }
        public override string Receive { get { return "Receive"; } }

        /***********************************************************************************
         * 
         *    System about
         * 
         ************************************************************************************/

        public override string SystemInstallOperater { get { return "Install new software: ip address is"; } }
        public override string SystemUpdateOperater { get { return "Update software: ip address is"; } }


        /***********************************************************************************
         * 
         *    Socket-related Information description
         * 
         ************************************************************************************/

        public override string SocketIOException { get { return "Socket transport error: "; } }
        public override string SocketSendException { get { return "Synchronous Data Send exception: "; } }
        public override string SocketHeadReceiveException { get { return "Command header receive exception: "; } }
        public override string SocketContentReceiveException { get { return "Content Data Receive exception: "; } }
        public override string SocketContentRemoteReceiveException { get { return "Recipient content Data Receive exception: "; } }
        public override string SocketAcceptCallbackException { get { return "Asynchronously accepts an incoming connection attempt: "; } }
        public override string SocketReAcceptCallbackException { get { return "To re-accept incoming connection attempts asynchronously"; } }
        public override string SocketSendAsyncException { get { return "Asynchronous Data send Error: "; } }
        public override string SocketEndSendException { get { return "Asynchronous data end callback send Error"; } }
        public override string SocketReceiveException { get { return "Asynchronous Data send Error: "; } }
        public override string SocketEndReceiveException { get { return "Asynchronous data end receive instruction header error"; } }
        public override string SocketRemoteCloseException { get { return "An existing connection was forcibly closed by the remote host"; } }


        /***********************************************************************************
         * 
         *    File related information
         * 
         ************************************************************************************/


        public override string FileDownloadSuccess { get { return "File Download Successful"; } }
        public override string FileDownloadFailed { get { return "File Download exception"; } }
        public override string FileUploadFailed { get { return "File Upload exception"; } }
        public override string FileUploadSuccess { get { return "File Upload Successful"; } }
        public override string FileDeleteFailed { get { return "File Delete exception"; } }
        public override string FileDeleteSuccess { get { return "File deletion succeeded"; } }
        public override string FileReceiveFailed { get { return "Confirm File Receive exception"; } }
        public override string FileNotExist { get { return "File does not exist"; } }
        public override string FileSaveFailed { get { return "File Store failed"; } }
        public override string FileLoadFailed { get { return "File load failed"; } }
        public override string FileSendClientFailed { get { return "An exception occurred when the file was sent"; } }
        public override string FileWriteToNetFailed { get { return "File Write Network exception"; } }
        public override string FileReadFromNetFailed { get { return "Read file exceptions from the network"; } }
        public override string FilePathCreateFailed { get { return "Folder path creation failed: "; } }
        public override string FileRemoteNotExist { get { return "The other file does not exist, cannot receive!"; } }

        /***********************************************************************************
         * 
         *    Engine-related data for the server
         * 
         ************************************************************************************/

        public override string TokenCheckFailed { get { return "Receive authentication token inconsistency"; } }
        public override string TokenCheckTimeout { get { return "Receive authentication timeout: "; } }
        public override string CommandHeadCodeCheckFailed { get { return "Command header check failed"; } }
        public override string CommandLengthCheckFailed { get { return "Command length check failed"; } }
        public override string NetClientAliasFailed { get { return "Client's alias receive failed: "; } }
        public override string NetEngineStart { get { return "Start engine"; } }
        public override string NetEngineClose { get { return "Shutting down the engine"; } }
        public override string NetClientOnline { get { return "Online"; } }
        public override string NetClientOffline { get { return "Offline"; } }
        public override string NetClientBreak { get { return "Abnormal offline"; } }
        public override string NetClientFull { get { return "The server hosts the upper limit and receives an exceeded request connection."; } }
        public override string NetClientLoginFailed { get { return "Error in Client logon: "; } }
        public override string NetHeartCheckFailed { get { return "Heartbeat Validation exception: "; } }
        public override string NetHeartCheckTimeout { get { return "Heartbeat verification timeout, force offline: "; } }
        public override string DataSourseFormatError { get { return "Data source format is incorrect"; } }
        public override string ServerFileCheckFailed { get { return "Server confirmed file failed, please re-upload"; } }
        public override string ClientOnlineInfo { get { return "Client [ {0} ] Online"; } }
        public override string ClientOfflineInfo { get { return "Client [ {0} ] Offline"; } }
        public override string ClientDisableLogin { get { return "Client [ {0} ] is not trusted, login forbidden"; } }

        /***********************************************************************************
         * 
         *    Client related
         * 
         ************************************************************************************/

        public override string ReConnectServerSuccess { get { return "Re-connect server succeeded"; } }
        public override string ReConnectServerAfterTenSeconds { get { return "Reconnect the server after 10 seconds"; } }
        public override string KeyIsNotAllowedNull { get { return "The keyword is not allowed to be empty"; } }
        public override string KeyIsExistAlready { get { return "The current keyword already exists"; } }
        public override string KeyIsNotExist { get { return "The keyword for the current subscription does not exist"; } }
        public override string ConnectingServer { get { return "Connecting to Server..."; } }
        public override string ConnectFailedAndWait { get { return "Connection disconnected, wait {0} seconds to reconnect"; } }
        public override string AttemptConnectServer { get { return "Attempting to connect server {0} times"; } }
        public override string ConnectServerSuccess { get { return "Connection Server succeeded"; } }
        public override string GetClientIpaddressFailed { get { return "Client IP Address acquisition failed"; } }
        public override string ConnectionIsNotAvailable { get { return "The current connection is not available"; } }
        public override string DeviceCurrentIsLoginRepeat { get { return "ID of the current device duplicate login"; } }
        public override string DeviceCurrentIsLoginForbidden { get { return "The ID of the current device prohibits login"; } }
        public override string PasswordCheckFailed { get { return "Password validation failed"; } }
        public override string DataTransformError { get { return "Data conversion failed, source data: "; } }
        public override string RemoteClosedConnection { get { return "Remote shutdown of connection"; } }

        /***********************************************************************************
         * 
         *    Log related
         * 
         ************************************************************************************/
        public override string LogNetDebug { get { return "Debug"; } }
        public override string LogNetInfo { get { return "Info"; } }
        public override string LogNetWarn { get { return "Warn"; } }
        public override string LogNetError { get { return "Error"; } }
        public override string LogNetFatal { get { return "Fatal"; } }
        public override string LogNetAbandon { get { return "Abandon"; } }
        public override string LogNetAll { get { return "All"; } }


        /***********************************************************************************
         * 
         *    Modbus related
         * 
         ************************************************************************************/

        public override string ModbusTcpFunctionCodeNotSupport { get { return "Unsupported function code"; } }
        public override string ModbusTcpFunctionCodeOverBound { get { return "Data read out of bounds"; } }
        public override string ModbusTcpFunctionCodeQuantityOver { get { return "Read length exceeds maximum value"; } }
        public override string ModbusTcpFunctionCodeReadWriteException { get { return "Read and Write exceptions"; } }
        public override string ModbusTcpReadCoilException { get { return "Read Coil anomalies"; } }
        public override string ModbusTcpWriteCoilException { get { return "Write Coil exception"; } }
        public override string ModbusTcpReadRegisterException { get { return "Read Register exception"; } }
        public override string ModbusTcpWriteRegisterException { get { return "Write Register exception"; } }
        public override string ModbusAddressMustMoreThanOne { get { return "The address value must be greater than 1 in the case where the start address is 1"; } }
        public override string ModbusAsciiFormatCheckFailed { get { return "Modbus ASCII command check failed, not MODBUS-ASCII message"; } }
        public override string ModbusCRCCheckFailed { get { return "The CRC checksum check failed for Modbus"; } }
        public override string ModbusLRCCheckFailed { get { return "The LRC checksum check failed for Modbus"; } }
        public override string ModbusMatchFailed { get { return "Not the standard Modbus protocol"; } }


        /***********************************************************************************
         * 
         *    Melsec PLC related
         * 
         ************************************************************************************/
        public override string MelsecPleaseReferToManulDocument { get { return "Please check Mitsubishi's communication manual for details of the alarm."; } }
        public override string MelsecReadBitInfo { get { return "The read bit variable array can only be used for bit soft elements, if you read the word soft component, call the Read method"; } }
        public override string MelsecCurrentTypeNotSupportedWordOperate { get { return "The current type does not support word read and write"; } }
        public override string MelsecCurrentTypeNotSupportedBitOperate { get { return "The current type does not support bit read and write"; } }
        public override string MelsecFxReceiveZore { get { return "The received data length is 0"; } }
        public override string MelsecFxAckNagative { get { return "Invalid data from PLC feedback"; } }
        public override string MelsecFxAckWrong { get { return "PLC Feedback Signal Error: "; } }
        public override string MelsecFxCrcCheckFailed { get { return "PLC Feedback message and check failed!"; } }

        /***********************************************************************************
         * 
         *    Siemens PLC related
         * 
         ************************************************************************************/

        public override string SiemensDBAddressNotAllowedLargerThan255 { get { return "DB block data cannot be greater than 255"; } }
        public override string SiemensReadLengthMustBeEvenNumber { get { return "The length of the data read must be an even number"; } }
        public override string SiemensWriteError { get { return "Writes the data exception, the code name is: "; } }
        public override string SiemensReadLengthCannotLargerThan19 { get { return "The number of arrays read does not allow greater than 19"; } }
        public override string SiemensDataLengthCheckFailed { get { return "Block length checksum failed, please check if Put/get is turned on and DB block optimization is turned off"; } }
        public override string SiemensFWError { get { return "An exception occurred, the specific information to find the Fetch/write protocol document"; } }
        public override string SiemensReadLengthOverPlcAssign { get { return "The range of data read exceeds the setting of the PLC"; } }

        /***********************************************************************************
         * 
         *    Omron PLC related
         * 
         ************************************************************************************/

        public override string OmronAddressMustBeZeroToFiveteen { get { return "The bit address entered can only be between 0-15"; } }
        public override string OmronReceiveDataError { get { return "Data Receive exception"; } }
        public override string OmronStatus0 { get { return "Communication is normal."; } }
        public override string OmronStatus1 { get { return "The message header is not fins"; } }
        public override string OmronStatus2 { get { return "Data length too long"; } }
        public override string OmronStatus3 { get { return "This command does not support"; } }
        public override string OmronStatus20 { get { return "Exceeding connection limit"; } }
        public override string OmronStatus21 { get { return "The specified node is already in the connection"; } }
        public override string OmronStatus22 { get { return "Attempt to connect to a protected network node that is not yet configured in the PLC"; } }
        public override string OmronStatus23 { get { return "The current client's network node exceeds the normal range"; } }
        public override string OmronStatus24 { get { return "The current client's network node is already in use"; } }
        public override string OmronStatus25 { get { return "All network nodes are already in use"; } }



        /***********************************************************************************
         * 
         *    AB PLC related
         * 
         ************************************************************************************/


        public override string AllenBradley04 { get { return "The IOI could not be deciphered. Either it was not formed correctly or the match tag does not exist."; } }
        public override string AllenBradley05 { get { return "The particular item referenced (usually instance) could not be found."; } }
        public override string AllenBradley06 { get { return "The amount of data requested would not fit into the response buffer. Partial data transfer has occurred."; } }
        public override string AllenBradley0A { get { return "An error has occurred trying to process one of the attributes."; } }
        public override string AllenBradley13 { get { return "Not enough command data / parameters were supplied in the command to execute the service requested."; } }
        public override string AllenBradley1C { get { return "An insufficient number of attributes were provided compared to the attribute count."; } }
        public override string AllenBradley1E { get { return "A service request in this service went wrong."; } }
        public override string AllenBradley26 { get { return "The IOI word length did not match the amount of IOI which was processed."; } }

        public override string AllenBradleySessionStatus00 { get { return "success"; } }
        public override string AllenBradleySessionStatus01 { get { return "The sender issued an invalid or unsupported encapsulation command."; } }
        public override string AllenBradleySessionStatus02 { get { return "Insufficient memory resources in the receiver to handle the command. This is not an application error. Instead, it only results if the encapsulation layer cannot obtain memory resources that it need."; } }
        public override string AllenBradleySessionStatus03 { get { return "Poorly formed or incorrect data in the data portion of the encapsulation message."; } }
        public override string AllenBradleySessionStatus64 { get { return "An originator used an invalid session handle when sending an encapsulation message."; } }
        public override string AllenBradleySessionStatus65 { get { return "The target received a message of invalid length."; } }
        public override string AllenBradleySessionStatus69 { get { return "Unsupported encapsulation protocol revision."; } }

        /***********************************************************************************
         * 
         *    Panasonic PLC related
         * 
         ************************************************************************************/
        public override string PanasonicReceiveLengthMustLargerThan9 { get { return "The received data length must be greater than 9"; } }
        public override string PanasonicAddressParameterCannotBeNull { get { return "Address parameter is not allowed to be empty"; } }
        public override string PanasonicMewStatus20 { get { return "Error unknown"; } }
        public override string PanasonicMewStatus21 { get { return "Nack error, the remote unit could not be correctly identified, or a data error occurred."; } }
        public override string PanasonicMewStatus22 { get { return "WACK Error: The receive buffer for the remote unit is full."; } }
        public override string PanasonicMewStatus23 { get { return "Multiple port error: The remote unit number (01 to 16) is set to repeat with the local unit."; } }
        public override string PanasonicMewStatus24 { get { return "Transport format error: An attempt was made to send data that does not conform to the transport format, or a frame data overflow or a data error occurred."; } }
        public override string PanasonicMewStatus25 { get { return "Hardware error: Transport system hardware stopped operation."; } }
        public override string PanasonicMewStatus26 { get { return "Unit Number error: The remote unit's numbering setting exceeds the range of 01 to 63."; } }
        public override string PanasonicMewStatus27 { get { return "Error not supported: Receiver data frame overflow. An attempt was made to send data of different frame lengths between different modules."; } }
        public override string PanasonicMewStatus28 { get { return "No answer error: The remote unit does not exist. (timeout)."; } }
        public override string PanasonicMewStatus29 { get { return "Buffer Close error: An attempt was made to send or receive a buffer that is in a closed state."; } }
        public override string PanasonicMewStatus30 { get { return "Timeout error: Persisted in transport forbidden State."; } }
        public override string PanasonicMewStatus40 { get { return "BCC Error: A transmission error occurred in the instruction data."; } }
        public override string PanasonicMewStatus41 { get { return "Malformed: The sent instruction information does not conform to the transmission format."; } }
        public override string PanasonicMewStatus42 { get { return "Error not supported: An unsupported instruction was sent. An instruction was sent to a target station that was not supported."; } }
        public override string PanasonicMewStatus43 { get { return "Processing Step Error: Additional instructions were sent when the transfer request information was suspended."; } }
        public override string PanasonicMewStatus50 { get { return "Link Settings Error: A link number that does not actually exist is set."; } }
        public override string PanasonicMewStatus51 { get { return "Simultaneous operation error: When issuing instructions to other units, the transmit buffer for the local unit is full."; } }
        public override string PanasonicMewStatus52 { get { return "Transport suppression Error: Unable to transfer to other units."; } }
        public override string PanasonicMewStatus53 { get { return "Busy error: Other instructions are being processed when the command is received."; } }
        public override string PanasonicMewStatus60 { get { return "Parameter error: Contains code that cannot be used in the directive, or the code does not have a zone specified parameter (X, Y, D), and so on."; } }
        public override string PanasonicMewStatus61 { get { return "Data error: Contact number, area number, Data code format (BCD,HEX, etc.) overflow, overflow, and area specified error."; } }
        public override string PanasonicMewStatus62 { get { return "Register ERROR: Excessive logging of data in an unregistered state of operations (Monitoring records, tracking records, etc.). )。"; } }
        public override string PanasonicMewStatus63 { get { return "PLC mode error: When an instruction is issued, the run mode is not able to process the instruction."; } }
        public override string PanasonicMewStatus65 { get { return "Protection Error: Performs a write operation to the program area or system register in the storage protection state."; } }
        public override string PanasonicMewStatus66 { get { return "Address Error: Address (program address, absolute address, etc.) Data encoding form (BCD, hex, etc.), overflow, underflow, or specified range error."; } }
        public override string PanasonicMewStatus67 { get { return "Missing data error: The data to be read does not exist. (reads data that is not written to the comment register.)"; } }


        /***********************************************************************************
         * 
         *   Fatek PLC 永宏PLC相关
         * 
         ************************************************************************************/
        public override string FatekStatus02 { get { return "Illegal value"; } }
        public override string FatekStatus03 { get { return "Write disabled"; } }
        public override string FatekStatus04 { get { return "Invalid command code"; } }
        public override string FatekStatus05 { get { return "Cannot be activated (down RUN command but Ladder Checksum does not match)"; } }
        public override string FatekStatus06 { get { return "Cannot be activated (down RUN command but PLC ID ≠ Ladder ID)"; } }
        public override string FatekStatus07 { get { return "Cannot be activated (down RUN command but program syntax error)"; } }
        public override string FatekStatus09 { get { return "Cannot be activated (down RUN command, but the ladder program command PLC cannot be executed)"; } }
        public override string FatekStatus10 { get { return "Illegal address"; } }



        /***********************************************************************************
         * 
         *   Fuji PLC 富士PLC相关
         * 
         ************************************************************************************/
        public override string FujiSpbStatus01 { get { return "Write to the ROM"; } }
        public override string FujiSpbStatus02 { get { return "Received undefined commands or commands that could not be processed"; } }
        public override string FujiSpbStatus03 { get { return "There is a contradiction in the data part (parameter exception)"; } }
        public override string FujiSpbStatus04 { get { return "Unable to process due to transfer interlocks from other programmers"; } }
        public override string FujiSpbStatus05 { get { return "The module number is incorrect"; } }
        public override string FujiSpbStatus06 { get { return "Search item not found"; } }
        public override string FujiSpbStatus07 { get { return "An address that exceeds the module range (when writing) is specified"; } }
        public override string FujiSpbStatus09 { get { return "Unable to execute due to faulty program (RUN)"; } }
        public override string FujiSpbStatus0C { get { return "Inconsistent password"; } }


#pragma warning restore 1591//CS1591 // 缺少对公共可见类型或成员的 XML 注释

    }
}
