using System;
using System.Text;
using System.Net.Sockets;

namespace ETrikeV
{
	public class InitCommandRequest : PtpPacket
	{
		private Guid guid;
		private string friendlyName;
		private UInt32 protocolVersion;

		public InitCommandRequest (Guid guid, string friendlyName, UInt32 protocolVersion)
		{
			this.guid = guid;
			this.friendlyName = friendlyName;
			this.protocolVersion = protocolVersion;
			this.PacketType = PacketType.InitCommandRequest;
		}

		public override byte[] getBytes ()
		{
			if (data == null) {
				byte[] friendlyNameBytes = Encoding.Unicode.GetBytes (friendlyName);
				Length = (UInt32)(4 + 4 + 16 + friendlyNameBytes.Length + 4);

				data = new byte[Length];
				Array.Copy (BitConverter.GetBytes (Length), data, 4);
				Array.Copy (BitConverter.GetBytes ((UInt32)PacketType), 0, data, 4, 4);
				Array.Copy (guid.ToByteArray (), 0, data, 8, 16);
				Array.Copy (friendlyNameBytes, 0, data, 24, friendlyNameBytes.Length);
				Array.Copy (BitConverter.GetBytes (protocolVersion), 0, data, 24 + friendlyNameBytes.Length, 4);
			}

			return data;
		}
	}
}

