using System;

namespace ETrikeV
{
	public class OperationResponse : PtpPacket
	{
        public ResponseCode ResponseCode { get; private set; }
		public UInt32 TransactionID { get; private set; }
		public UInt32 Parameter1 { get; private set; }
		public UInt32 Parameter2 { get; private set; }
		public UInt32 Parameter3 { get; private set; }
		public UInt32 Parameter4 { get; private set; }
		public UInt32 Parameter5 { get; private set; }

		public OperationResponse ()
		{
		}

		public override void recv (System.Net.Sockets.NetworkStream ns)
		{
			dataRecv (ns);
			ResponseCode = (ResponseCode)BitConverter.ToUInt16 (data, 8);
			TransactionID = BitConverter.ToUInt32 (data, 10);
			if (Length > 14) Parameter1 = BitConverter.ToUInt32(data, 14);
            if (Length > 18) Parameter2 = BitConverter.ToUInt32(data, 18);
            if (Length > 22) Parameter3 = BitConverter.ToUInt32(data, 22);
            if (Length > 26) Parameter4 = BitConverter.ToUInt32(data, 26);
            if (Length > 30) Parameter5 = BitConverter.ToUInt32(data, 30);
		}
	}
}

