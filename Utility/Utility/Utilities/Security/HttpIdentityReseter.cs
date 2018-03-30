using System;
using System.Security.Principal;
using System.Web;

namespace Vertica.Utilities_v4.Security
{
	public class HttpIdentityReseter : IIdentityReseter, IDisposable
	{
		private readonly IPrincipal _previous;

		private HttpIdentityReseter(WindowsIdentity identity)
		{
			this._previous = HttpContext.Current.User;
			HttpContext.Current.User = new WindowsPrincipal(identity);
		}

		public void Dispose()
		{
			HttpContext.Current.User = this._previous;
		}

		public IDisposable Set(WindowsIdentity identity)
		{
			return new HttpIdentityReseter(identity);
		}
	}
}