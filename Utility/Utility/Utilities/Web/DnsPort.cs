using System;

namespace Vertica.Utilities_v4.Web
{
	public class DnsPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)53;
			}
		}

		public DnsPort()
		{
		}

		public DnsPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}