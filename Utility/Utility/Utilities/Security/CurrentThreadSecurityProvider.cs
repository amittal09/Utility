using System;
using System.Security.Principal;
using System.Threading;

namespace Vertica.Utilities_v4.Security
{
	public class CurrentThreadSecurityProvider : IWindowsIdentityProvider, IIdentityProvider, IPrincipalProvider, IDisposable
	{
		public CurrentThreadSecurityProvider()
		{
		}

		public void Dispose()
		{
		}

		public IIdentity GetIdentity()
		{
			return Thread.CurrentPrincipal.Identity;
		}

		public IPrincipal GetPrincipal()
		{
			return Thread.CurrentPrincipal;
		}

		public WindowsIdentity GetWindowsIdentity()
		{
			return (WindowsIdentity)this.GetIdentity();
		}
	}
}