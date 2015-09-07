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
        private static UInt32 sessionID = 1;
        private static UInt32 transactionID = 1;

        static void Main(string[] args)
        {
			EV3WiFiDevice wifi = new EV3WiFiDevice ();
//			// WiFi ON
//			bool f = wifi.IsLinkUp();
//
			wifi.TurnOn ("THETAXN00000010", "00000010", true);

            // Command/Data Connection
			CmdDataConnection cmdDataCon = new CmdDataConnection(THETA_ADDR, THETA_PORT);

            // Event Connection
			EventConnection evtCon = new EventConnection(THETA_ADDR, THETA_PORT, cmdDataCon.ConnectionNumber);

			// OpenSession
			OperationResponse res = cmdDataCon.operationRequest(
				DataPhaseInfo.NoDataOrDataInPhase, OperationCode.OpenSession, transactionID, sessionID);
			if (res.ResponseCode != ResponseCode.OK) {
				//LcdConsole.WriteLine("OpenSession Failed. [" + res.ResponseCode + "]");
				return;
			}

            // InitiateCapture
            transactionID++;
			res = cmdDataCon.operationRequest (
				DataPhaseInfo.NoDataOrDataInPhase, OperationCode.InitiateCapture, transactionID, 0, 0);
			if (res.ResponseCode != ResponseCode.OK) {
				//LcdConsole.WriteLine ("InitiateCapture Failed. [" + res.ResponseCode + "]");
				return;
			}

			// Close
			cmdDataCon.close();
			evtCon.close ();

			//LcdConsole.WriteLine ("Disconnected.");
        }
    }
}
