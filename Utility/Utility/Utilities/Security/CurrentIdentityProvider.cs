using System;
using System.Security.Principal;

namespace Vertica.Utilities_v4.Security
{
	public class CurrentIdentityProvider : WindowsIdentityProviderBase
	{
		public CurrentIdentityProvider()
		{
		}

		public override WindowsIdentity GetWindowsIdentity()
		{
			return WindowsIdentity.GetCurrent();
		}
	}
}