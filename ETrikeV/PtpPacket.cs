using System;
using System.Net.Sockets;

namespace ETrikeV
{
	public class PtpPacket
	{
		public UInt32 Length { get; protected set; }
		public PacketType PacketType { get; protected set; }
		protected byte[] data;

		public PtpPacket () {}

		public virtual byte[] getBytes()
		{
			return data;
		}

		public virtual void send(NetworkStream ns)
		{
			if (data == null) {
				getBytes ();
			}
			ns.Write (data, 0, data.Length);
			ns.Flush ();
		}

		public virtual void recv(NetworkStream ns)
		{
			dataRecv (ns);
		}

		protected void dataRecv(NetworkStream ns)
		{
			byte[] lengthByte = new byte[4];

			ns.Read (lengthByte, 0, lengthByte.Length);
			Length = BitConverter.ToUInt32 (lengthByte, 0);

			data = new byte[Length];
			Array.Copy (lengthByte, data, lengthByte.Length);

			// 全データ受信する
			ns.Read (data, 4, (int)(Length - 4));

			// PacketType
			PacketType = (PacketType)BitConverter.ToInt32(data, 4);
		}
	}
}

