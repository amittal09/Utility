using System;
using System.Security.Principal;
using System.Web;

namespace Vertica.Utilities_v4.Security
{
	public class HttpContextPrincipalProvider : IPrincipalProvider, IDisposable
	{
		private readonly HttpContextBase _context;

		public HttpContextPrincipalProvider() : this(new HttpContextWrapper(HttpContext.Current))
		{
		}

		public HttpContextPrincipalProvider(HttpContextBase context)
		{
			this._context = context;
		}

		public void Dispose()
		{
		}

		public IPrincipal GetPrincipal()
		{
			return this._context.User;
		}
	}
}