using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Extensions.Infrastructure
{
	public static class EntryPoints
	{
		public static CastExtensionPoint<object> Cast(this object subject)
		{
			return new CastExtensionPoint<object>(subject);
		}
	}
}