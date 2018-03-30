using System;

namespace Vertica.Utilities_v4.Web
{
	public class Pop3Port : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)110;
			}
		}

		public Pop3Port()
		{
		}

		public Pop3Port(ushort portNumber) : base(portNumber)
		{
		}
	}
}