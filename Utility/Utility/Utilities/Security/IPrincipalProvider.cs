using System;
using System.Security.Principal;

namespace Vertica.Utilities_v4.Security
{
	public interface IPrincipalProvider : IDisposable
	{
		IPrincipal GetPrincipal();
	}
}