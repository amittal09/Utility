using System;
using System.Security.Principal;
using System.Web;

namespace Vertica.Utilities_v4.Security
{
	public class HttpContextSecurityProvider : IIdentityProvider, IPrincipalProvider, IDisposable
	{
		private readonly HttpContextBase _context;

		public HttpContextSecurityProvider() : this(new HttpContextWrapper(HttpContext.Current))
		{
		}

		public HttpContextSecurityProvider(HttpContextBase context)
		{
			this._context = context;
		}

		public void Dispose()
		{
		}

		public IIdentity GetIdentity()
		{
			return this._context.User.Identity;
		}

		public IPrincipal GetPrincipal()
		{
			return this._context.User;
		}
	}
}