using System;

namespace Vertica.Utilities_v4.Web
{
	public class LdapPort : TcpPort
	{
		public override ushort DefaultPort
		{
			get
			{
				return (ushort)389;
			}
		}

		public LdapPort()
		{
		}

		public LdapPort(ushort portNumber) : base(portNumber)
		{
		}
	}
}