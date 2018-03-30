using System;

namespace Vertica.Utilities_v4.Web
{
	public class TelnetPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)23;
			}
		}

		public TelnetPort()
		{
		}

		public TelnetPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}