using System;

namespace ETrikeV
{
	public class OperationRequest : PtpPacket
	{
		public DataPhaseInfo DataPhaseInfo { get; private set; }
		public OperationCode OperationCode { get; private set; }
		public UInt32 TransactionID { get; private set; }
		public UInt32 Parameter1 { get; private set; }
		public UInt32 Parameter2 { get; private set; }
		public UInt32 Parameter3 { get; private set; }
		public UInt32 Parameter4 { get; private set; }
		public UInt32 Parameter5 { get; private set; }

		public OperationRequest (DataPhaseInfo dpi, OperationCode ope, UInt32 tid, 
			UInt32 param1 = 0, UInt32 param2 = 0, UInt32 param3 = 0, UInt32 param4 = 0, UInt32 param5 = 0)
		{
			DataPhaseInfo = dpi;
			OperationCode = ope;
			TransactionID = tid;
			Parameter1 = param1;
			Parameter2 = param2;
			Parameter3 = param3;
			Parameter4 = param4;
			Parameter5 = param5;
			PacketType = PacketType.OperationRequest;
		}

		public override byte[] getBytes ()
		{
			if (data == null) {
				Length = (UInt32)(4 + 4 + 4 + 2 + 4 + 4 + 4 + 4 + 4 + 4);
				data = new byte[Length];
				Array.Copy (BitConverter.GetBytes (Length), data, 4);
				Array.Copy (BitConverter.GetBytes ((UInt32)PacketType), 0, data, 4, 4);
				Array.Copy (BitConverter.GetBytes ((UInt32)DataPhaseInfo), 0, data, 8, 4);
				Array.Copy (BitConverter.GetBytes ((UInt16)OperationCode), 0, data, 12, 2);
				Array.Copy (BitConverter.GetBytes (TransactionID), 0, data, 14, 4);
				Array.Copy (BitConverter.GetBytes (Parameter1), 0, data, 18, 4);
				Array.Copy (BitConverter.GetBytes (Parameter2), 0, data, 22, 4);
				Array.Copy (BitConverter.GetBytes (Parameter3), 0, data, 26, 4);
				Array.Copy (BitConverter.GetBytes (Parameter4), 0, data, 30, 4);
				Array.Copy (BitConverter.GetBytes (Parameter5), 0, data, 34, 4);
			}

			return data;
		}
	}
}

