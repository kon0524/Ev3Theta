using System;

namespace ETrikeV
{
	public class InitEventRequest : PtpPacket
	{
		private UInt32 connectionNumber;

		public InitEventRequest (UInt32 connectionNumber)
		{
			this.connectionNumber = connectionNumber;
            this.PacketType = ETrikeV.PacketType.InitEventRequest;
		}

		public override byte[] getBytes ()
		{
			if (data == null) {
				Length = (UInt32)(4 + 4 + 4);

				data = new byte[Length];
				Array.Copy (BitConverter.GetBytes (Length), data, 4);
				Array.Copy (BitConverter.GetBytes ((UInt32)PacketType), 0, data, 4, 4);
				Array.Copy (BitConverter.GetBytes (connectionNumber), 0, data, 8, 4);
			}

			return data;
		}

		public override void send (System.Net.Sockets.NetworkStream ns)
		{
			if (data == null) {
				getBytes ();
			}
			ns.Write (data, 0, data.Length);
			ns.Flush ();
		}

		public override void recv (System.Net.Sockets.NetworkStream ns)
		{
			throw new NotImplementedException ();
		}
	}
}

