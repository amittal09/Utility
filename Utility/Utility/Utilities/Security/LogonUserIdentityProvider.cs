using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4.Security
{
	internal class LogonUserIdentityProvider : WindowsIdentityProviderBase
	{
		private const uint LOGON32_LOGON_NETWORK = 3;

		private const uint LOGON32_PROVIDER_DEFAULT = 0;

		private readonly Credential _credential;

		private static IntPtr _token;

		static LogonUserIdentityProvider()
		{
			LogonUserIdentityProvider._token = IntPtr.Zero;
		}

		internal LogonUserIdentityProvider(Credential credential)
		{
			this._credential = credential;
		}

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern bool CloseHandle(IntPtr handle);

		public override void Dispose()
		{
			base.Dispose();
			LogonUserIdentityProvider.CloseHandle(LogonUserIdentityProvider._token);
		}

		public override WindowsIdentity GetWindowsIdentity()
		{
			if (!LogonUserIdentityProvider.LogonUser(this._credential.UserName, this._credential.Domain, this._credential.Password, 3, 0, out LogonUserIdentityProvider._token))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				string logonUserIdentityProviderLogonUserErrorTemplate = Exceptions.LogonUserIdentityProvider_LogonUserErrorTemplate;
				string[] str = new string[] { lastWin32Error.ToString(CultureInfo.InvariantCulture), this._credential.ToString() };
				ExceptionHelper.Throw<InvalidOperationException>(logonUserIdentityProviderLogonUserErrorTemplate, str);
			}
			return new WindowsIdentity(LogonUserIdentityProvider._token);
		}

		[DllImport("advapi32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern bool LogonUser(string principal, string authority, string password, uint logonType, uint logonProvider, out IntPtr token);
	}
}