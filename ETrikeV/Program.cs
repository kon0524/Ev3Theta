using System.Threading;
using MonoBrickFirmware.Connections;
using MonoBrickFirmware.Display;
using System.Net.Sockets;
using System;
using System.Text;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Movement;

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
			// ThetaとWifi接続する
			EV3WiFiDevice wifi = new EV3WiFiDevice ();
			if (!wifi.IsLinkUp ()) {
				// SSIDとパスワードは適宜変更してください
				wifi.TurnOn ("THETAXN00000010", "00000010", true);
			}

			//---------- ▼ PTPIPの接続開始ここから ▼ ----------
            // Command/Data Connection
			CmdDataConnection cmdDataCon = new CmdDataConnection(THETA_ADDR, THETA_PORT);

            // Event Connection
			EventConnection evtCon = new EventConnection(THETA_ADDR, THETA_PORT, cmdDataCon.ConnectionNumber);
			//---------- ▲ PTPIPの接続開始ここまで ▲ ----------

			// ここでThetaのAPIを実行すれば動作する（はず）



			// PTPIPの接続を終了する
			cmdDataCon.close();
			evtCon.close ();
        }
    }
}
