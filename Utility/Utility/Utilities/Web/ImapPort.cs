using System;

namespace Vertica.Utilities_v4.Web
{
	public class ImapPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)143;
			}
		}

		public ImapPort()
		{
		}

		public ImapPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}