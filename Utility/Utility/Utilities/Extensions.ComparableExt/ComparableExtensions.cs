using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Comparisons;

namespace Vertica.Utilities_v4.Extensions.ComparableExt
{
	public static class ComparableExtensions
	{
		public static bool IsAtLeast<T>(this T first, T second)
		where T : IComparable<T>
		{
			return first.IsAtLeast<T>(second, Comparer<T>.Default);
		}

		public static bool IsAtLeast<T>(this T first, T second, IComparer<T> comparer)
		{
			return comparer.Compare(first, second) >= 0;
		}

		public static bool IsAtLeast(this IComparable first, object second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static bool IsAtMost<T>(this T first, T second)
		where T : IComparable<T>
		{
			return first.IsAtMost<T>(second, Comparer<T>.Default);
		}

		public static bool IsAtMost<T>(this T first, T second, IComparer<T> comparer)
		{
			return comparer.Compare(first, second) <= 0;
		}

		public static bool IsAtMost(this IComparable first, object second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static bool IsDifferentFrom<T>(this T first, T second)
		where T : IComparable<T>
		{
			return !first.IsEqualTo<T>(second);
		}

		public static bool IsDifferentFrom<T>(this T first, T second, IComparer<T> comparer)
		{
			return !first.IsEqualTo<T>(second, comparer);
		}

		public static bool IsDifferentFrom(this IComparable first, object second)
		{
			return !first.IsEqualTo(second);
		}

		public static bool IsEqualTo<T>(this T first, T second)
		where T : IComparable<T>
		{
			return first.IsEqualTo<T>(second, Comparer<T>.Default);
		}

		public static bool IsEqualTo<T>(this T first, T second, IComparer<T> comparer)
		{
			return comparer.Compare(first, second) == 0;
		}

		public static bool IsEqualTo(this IComparable first, object second)
		{
			return Comparer<object>.Default.Compare(first, second) == 0;
		}

		public static bool IsLessThan<T>(this T first, T second)
		where T : IComparable<T>
		{
			return first.IsLessThan<T>(second, Comparer<T>.Default);
		}

		public static bool IsLessThan<T>(this T first, T second, IComparer<T> comparer)
		{
			return comparer.Compare(first, second) < 0;
		}

		public static bool IsLessThan(this IComparable first, object second)
		{
			return first.CompareTo(second) < 0;
		}

		public static bool IsMoreThan<T>(this T first, T second)
		where T : IComparable<T>
		{
			return first.IsMoreThan<T>(second, Comparer<T>.Default);
		}

		public static bool IsMoreThan<T>(this T first, T second, IComparer<T> comparer)
		{
			return comparer.Compare(first, second) > 0;
		}

		public static bool IsMoreThan(this IComparable first, object second)
		{
			return first.CompareTo(second) > 0;
		}

		public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
		{
			return new ReversedComparer<T>(comparer, Direction.Ascending);
		}
	}
}