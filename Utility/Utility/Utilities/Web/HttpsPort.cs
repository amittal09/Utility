using System;

namespace Vertica.Utilities_v4.Web
{
	public class HttpsPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)443;
			}
		}

		public HttpsPort()
		{
		}

		public HttpsPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}