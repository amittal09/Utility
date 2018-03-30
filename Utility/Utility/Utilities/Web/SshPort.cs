using System;

namespace Vertica.Utilities_v4.Web
{
	public class SshPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)22;
			}
		}

		public SshPort()
		{
		}

		public SshPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}