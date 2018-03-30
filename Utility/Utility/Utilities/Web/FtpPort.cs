using System;

namespace Vertica.Utilities_v4.Web
{
	public class FtpPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)21;
			}
		}

		public FtpPort()
		{
		}

		public FtpPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}