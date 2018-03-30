using System;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Extensions.ComparableExt;

namespace Vertica.Utilities_v4.Extensions.RangeExt
{
	public static class RangeExtensions
	{
		public static IBound<T> Close<T>(this T value)
		where T : IComparable<T>
		{
			return new Closed<T>(value);
		}

		public static T Limit<T>(this T item, Range<T> range)
		where T : IComparable<T>
		{
			return range.Limit(item);
		}

		public static T LimitLower<T>(this T value, Range<T> range)
		where T : IComparable<T>
		{
			return range.LimitLower(value);
		}

		public static T LimitUpper<T>(this T value, Range<T> range)
		where T : IComparable<T>
		{
			return range.LimitUpper(value);
		}

		public static IBound<T> Open<T>(this T value)
		where T : IComparable<T>
		{
			return new Open<T>(value);
		}

		public static Range<T> To<T>(this T lowerBound, T upperBound)
		where T : IComparable<T>
		{
			Range<T> empty = Range<T>.Empty;
			if (lowerBound.IsLessThan<T>(upperBound))
			{
				empty = new Range<T>(lowerBound, upperBound);
			}
			return empty;
		}

		public static bool Within<T>(this T value, Range<T> range)
		where T : IComparable<T>
		{
			return range.Contains(value);
		}
	}
}