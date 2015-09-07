using System;
using System.Net.Sockets;

namespace ETrikeV
{
	public class CmdDataConnection
	{
		// フィールド
		private TcpClient cmd;
		private NetworkStream cmdStream;
		private Guid guid = Guid.Empty;
		private const string FRIENDLY_NAME = "TEST";

		// プロパティ
		public UInt32 ConnectionNumber { get; private set; }

		public CmdDataConnection (string ip, int port)
		{
			// Command/Data Connection
			cmd = new TcpClient(ip, port);
			cmdStream = cmd.GetStream();

			// Init Command Requestを送信
			InitCommandRequest initCmdReq = new InitCommandRequest(guid, FRIENDLY_NAME, 0x00010000);
			initCmdReq.send(cmdStream);

			// Init Command Ackを待つ
			InitCommandAck initCmdAck = new InitCommandAck();
			initCmdAck.recv(cmdStream);
			ConnectionNumber = initCmdAck.ConnectionNumber;
		}

		public OperationResponse operationRequest(
			DataPhaseInfo dpi, OperationCode code, UInt32 tid, 
			UInt32 param1 = 0, UInt32 param2 = 0, UInt32 param3 = 0, UInt32 param4 = 0, UInt32 param5 = 0)
		{
			OperationRequest request = new OperationRequest(
				dpi, code, tid, param1, param2, param3, param4, param5);
			request.send(cmdStream);

			OperationResponse response = new OperationResponse();
			response.recv(cmdStream);

			return response;
		}

		public void close()
		{
			cmdStream.Close ();
			cmdStream = null;

			cmd.Close ();
			cmd = null;
		}
	}
}

