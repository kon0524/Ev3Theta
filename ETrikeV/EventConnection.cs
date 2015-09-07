using System;
using System.Net.Sockets;

namespace ETrikeV
{
	public class EventConnection
	{
		private TcpClient evt;
		private NetworkStream evtStream;

		public EventConnection (string ip, int port, UInt32 connectionNumber)
		{
			evt = new TcpClient(ip, port);
			evtStream = evt.GetStream();

			// Init Event Commandを送信
			InitEventRequest initEvtReq = new InitEventRequest(connectionNumber);
			initEvtReq.send(evtStream);

			// Init Event Ackを待つ
			PtpPacket initEvtAck = new PtpPacket();
			initEvtAck.recv(evtStream);
		}

		public void close()
		{
			evtStream.Close ();
			evtStream = null;

			evt.Close ();
			evt = null;
		}
	}
}

