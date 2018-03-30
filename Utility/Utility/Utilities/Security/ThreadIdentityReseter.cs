using System;
using System.Security.Principal;
using System.Threading;

namespace Vertica.Utilities_v4.Security
{
	public class ThreadIdentityReseter : IIdentityReseter, IDisposable
	{
		private readonly IPrincipal _previous;

		private ThreadIdentityReseter(WindowsIdentity identity)
		{
			this._previous = Thread.CurrentPrincipal;
			Thread.CurrentPrincipal = new WindowsPrincipal(identity);
		}

		public void Dispose()
		{
			Thread.CurrentPrincipal = this._previous;
		}

		public IDisposable Set(WindowsIdentity identity)
		{
			return new ThreadIdentityReseter(identity);
		}
	}
}