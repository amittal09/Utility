using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Vertica.Utilities_v4.Security
{
	public class Impersonate
	{
		private readonly IWindowsIdentityProvider _provider;

		internal Impersonate(IWindowsIdentityProvider provider)
		{
			this._provider = provider;
		}

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern bool CloseHandle(IntPtr handle);

		public void Do(Action action)
		{
			using (this._provider)
			{
				using (WindowsIdentity windowsIdentity = this._provider.GetWindowsIdentity())
				{
					using (WindowsImpersonationContext windowsImpersonationContext = windowsIdentity.Impersonate())
					{
						try
						{
							action();
						}
						finally
						{
							windowsImpersonationContext.Undo();
							Impersonate.CloseHandle(windowsIdentity.Token);
						}
					}
				}
			}
		}

		public static Impersonate Using(IWindowsIdentityProvider provider)
		{
			return new Impersonate(provider);
		}
	}
}