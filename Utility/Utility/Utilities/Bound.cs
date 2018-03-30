using System;

namespace Vertica.Utilities_v4
{
	public static class Bound
	{
		public static IBound<T> Closed<T>(T value)
		where T : IComparable<T>
		{
			return new Closed<T>(value);
		}

		public static IBound<T> Open<T>(T value)
		where T : IComparable<T>
		{
			return new Open<T>(value);
		}
	}
}