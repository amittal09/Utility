using System;

namespace Vertica.Utilities_v4.Extensions.StringExt
{
	public delegate bool TryParseDelegate<T>(string s, out T result);
}