using System;

namespace Vertica.Utilities_v4.Web
{
	public class HttpPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)80;
			}
		}

		public HttpPort()
		{
		}

		public HttpPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}