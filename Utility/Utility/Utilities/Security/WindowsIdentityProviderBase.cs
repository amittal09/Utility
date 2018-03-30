using System;
using System.Security.Principal;

namespace Vertica.Utilities_v4.Security
{
	public abstract class WindowsIdentityProviderBase : IWindowsIdentityProvider, IIdentityProvider, IDisposable
	{
		protected WindowsIdentityProviderBase()
		{
		}

		public virtual void Dispose()
		{
		}

		public IIdentity GetIdentity()
		{
			return this.GetWindowsIdentity();
		}

		public abstract WindowsIdentity GetWindowsIdentity();
	}
}