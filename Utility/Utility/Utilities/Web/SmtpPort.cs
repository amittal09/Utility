using System;

namespace Vertica.Utilities_v4.Web
{
	public class SmtpPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)25;
			}
		}

		public SmtpPort()
		{
		}

		public SmtpPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}