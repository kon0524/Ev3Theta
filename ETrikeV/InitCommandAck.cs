using System;
using System.Net.Sockets;
using System.Text;

namespace ETrikeV
{
	public class InitCommandAck : PtpPacket
	{
		public UInt32 ConnectionNumber { get; private set; }
		public Guid ResponderGuid { get; private set; }
		public string ResponderFriendlyName { get; private set; }

		public InitCommandAck ()
		{
		}

		public override byte[] getBytes ()
		{
			return data;
		}

		public override void send (NetworkStream ns)
		{
			throw new NotImplementedException ();
		}

		public override void recv (NetworkStream ns)
		{
			// 受信する
			dataRecv(ns);

			// ConnectionNumber
			ConnectionNumber = BitConverter.ToUInt32(data, 8);

			// Responder GUID
			byte[] guidBytes = new byte[16];
			Array.Copy (data, 12, guidBytes, 0, guidBytes.Length);
			ResponderGuid = new Guid(guidBytes);

			// Responder Friendly Name
			ResponderFriendlyName = Encoding.Unicode.GetString(data, 28, (int)Length - 28);
		}
	}
}

