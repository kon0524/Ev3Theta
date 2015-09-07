using System.Threading;
using MonoBrickFirmware.Connections;
using MonoBrickFirmware.Display;
using System.Net.Sockets;
using System;
using System.Text;

namespace ETrikeV
{
    class Program
    {
        private const string THETA_ADDR = "192.168.1.1";
        private const int THETA_PORT = 15740;
        private static string ipAddr = null;
        private static Guid guid = Guid.Empty;
        private const string FRIENDLY_NAME = "TEST";
        private static UInt32 sessionID = 1;
        private static UInt32 transactionID = 3;

        static void Main(string[] args)
        {
            // 各種初期化
            init();

            // WiFiをON
            WiFiDevice.TurnOn("ssid", "pass", true, 5000);

            // IPアドレスを取得
            ipAddr = WiFiDevice.GetIpAddress();
            LcdConsole.WriteLine(ipAddr);	// 192.168.1.5のはず

            // Command/Data Connection
            TcpClient cmd = new TcpClient(THETA_ADDR, THETA_PORT);
            NetworkStream cmdStream = cmd.GetStream();

            // Init Command Requestを送信
            InitCommandRequest initCmdReq = new InitCommandRequest(guid, FRIENDLY_NAME, 0x00010000);
            initCmdReq.send(cmdStream);

            // Init Command Ackを待つ
            InitCommandAck initCmdAck = new InitCommandAck();
            initCmdAck.recv(cmdStream);

            // Event Connection
            TcpClient evt = new TcpClient(THETA_ADDR, THETA_PORT);
            NetworkStream evtStream = evt.GetStream();

            // Init Event Commandを送信
            InitEventRequest initEvtReq = new InitEventRequest(initCmdAck.ConnectionNumber);
            initEvtReq.send(evtStream);

            // Init Event Ackを待つ
            PtpPacket initEvtAck = new PtpPacket();
            initEvtAck.recv(evtStream);

            // OpenSession
            OperationRequest openSessionReq = new OperationRequest(DataPhaseInfo.NoDataOrDataInPhase, OperationCode.OpenSession, transactionID, sessionID);
            openSessionReq.send(cmdStream);

            OperationResponse openSessionRes = new OperationResponse();
            openSessionRes.recv(cmdStream);

            // InitiateCapture
            transactionID++;
            OperationRequest initiateCaptureReq = new OperationRequest(DataPhaseInfo.NoDataOrDataInPhase, OperationCode.InitiateCapture, transactionID, 0, 0);
            initiateCaptureReq.send(cmdStream);

            OperationResponse initiateCaptureRes = new OperationResponse();
            initiateCaptureRes.recv(cmdStream);

            // WiFiをOFF
            WiFiDevice.TurnOff();
        }

        private static void init()
        {
            // GUID
            guid = Guid.NewGuid();
        }
    }
}
