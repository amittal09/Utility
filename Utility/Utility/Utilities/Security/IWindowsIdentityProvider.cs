using System;
using System.Security.Principal;

namespace Vertica.Utilities_v4.Security
{
	public interface IWindowsIdentityProvider : IIdentityProvider, IDisposable
	{
		WindowsIdentity GetWindowsIdentity();
	}
}