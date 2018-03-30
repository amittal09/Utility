using System;

namespace Vertica.Utilities_v4.Web
{
	public class EchoPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)7;
			}
		}

		public EchoPort()
		{
		}

		public EchoPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}