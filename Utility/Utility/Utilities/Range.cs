using System;
using Vertica.Utilities_v4.Extensions.RangeExt;

namespace Vertica.Utilities_v4
{
	public static class Range
	{
		public static void AssertBounds<T>(T lowerBound, T upperBound)
		where T : IComparable<T>
		{
			Range<T>.AssertBounds(lowerBound, upperBound);
		}

		public static bool CheckBounds<T>(T lowerBound, T upperBound)
		where T : IComparable<T>
		{
			return Range<T>.CheckBounds(lowerBound, upperBound);
		}

		public static Range<T> Closed<T>(T lowerBound, T upperbound)
		where T : IComparable<T>
		{
			return new Range<T>(lowerBound.Close<T>(), upperbound.Close<T>());
		}

		public static Range<T> Degenerate<T>(T value)
		where T : IComparable<T>
		{
			return Range.Closed<T>(value, value);
		}

		public static Range<T> Empty<T>()
		where T : IComparable<T>
		{
			return Range<T>.Empty;
		}

		public static Range<T> HalfClosed<T>(T lowerBound, T upperbound)
		where T : IComparable<T>
		{
			return new Range<T>(lowerBound.Open<T>(), upperbound.Close<T>());
		}

		public static Range<T> HalfOpen<T>(T lowerBound, T upperbound)
		where T : IComparable<T>
		{
			return new Range<T>(lowerBound.Close<T>(), upperbound.Open<T>());
		}

		public static Range<T> New<T>(T lowerBound, T upperBound)
		where T : IComparable<T>
		{
			return Range.Closed<T>(lowerBound, upperBound);
		}

		public static Range<T> Open<T>(T lowerBound, T upperbound)
		where T : IComparable<T>
		{
			return new Range<T>(lowerBound.Open<T>(), upperbound.Open<T>());
		}

		public static string StringGenerator(string s)
		{
			int num = -1;
			for (int i = s.Length - 1; i >= 0 && num == -1; i--)
			{
				if (char.IsLetterOrDigit(s[i]))
				{
					num = i;
				}
			}
			if (num == s.Length - 1 || num == -1)
			{
				return Range.succ(s, s.Length);
			}
			return string.Concat(Range.succ(s, num + 1), s.Substring(num + 1));
		}

		private static string succ(string val, int length)
		{
			char chr = val[length - 1];
			char chr1 = chr;
			if (chr1 == '9')
			{
				return string.Concat((length > 1 ? Range.succ(val, length - 1) : "1"), '0');
			}
			if (chr1 == 'Z')
			{
				return string.Concat((length > 1 ? Range.succ(val, length - 1) : "A"), 'A');
			}
			if (chr1 != 'z')
			{
				return string.Concat(val.Substring(0, length - 1), (char)(chr + 1));
			}
			return string.Concat((length > 1 ? Range.succ(val, length - 1) : "a"), 'a');
		}
	}
}