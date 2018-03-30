using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Collections;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4.Extensions.EnumerableExt
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params T[] toTheEnd)
		{
			return source.Concat<T>(toTheEnd.EmptyIfNull<T>());
		}

		private static TSource compareBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer, Func<int, bool> candidateComparison)
		{
			TSource tSource;
			Guard.AgainstNullArgument("source", source);
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException(Exceptions.EnumerableExtensions_EmptyCollection);
				}
				TSource current = enumerator.Current;
				TKey tKey = selector(current);
				while (enumerator.MoveNext())
				{
					TSource current1 = enumerator.Current;
					TKey tKey1 = selector(current1);
					if (!candidateComparison(comparer.Compare(tKey1, tKey)))
					{
						continue;
					}
					current = current1;
					tKey = tKey1;
				}
				tSource = current;
			}
			return tSource;
		}

		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}

		public static void For<T>(this IEnumerable<T> collection, Action<T, int> action)
		{
			int num = 0;
			foreach (T t in collection.EmptyIfNull<T>())
			{
				int num1 = num;
				num = num1 + 1;
				action(t, num1);
			}
		}

		public static void For<T>(this IEnumerable<T> collection, Action<T, int> action, IEnumerable<int> indexes)
		{
			HashSet<int> nums = new HashSet<int>(indexes);
			int num = 0;
			foreach (T t in collection.EmptyIfNull<T>())
			{
				if (nums.Contains(num))
				{
					action(t, num);
				}
				num++;
			}
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T t in source.EmptyIfNull<T>())
			{
				action(t);
			}
		}

		public static bool HasAtLeast<T>(this IEnumerable<T> source, uint matchingCount)
		{
			bool flag = false;
			if (source != null)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = source.GetEnumerator())
				{
					while ((long)num <= (long)matchingCount && enumerator.MoveNext())
					{
						num++;
					}
				}
				flag = (long)num >= (long)matchingCount;
			}
			return flag;
		}

		public static bool HasOne<T>(this IEnumerable<T> source)
		{
			bool flag = false;
			if (source != null)
			{
				IEnumerator<T> enumerator = source.GetEnumerator();
				flag = (!enumerator.MoveNext() ? false : !enumerator.MoveNext());
			}
			return flag;
		}

		public static IEnumerable<IEnumerable<T>> InBatchesOf<T>(this IEnumerable<T> items, uint batchSize)
		{
			Guard.AgainstArgument<ArgumentOutOfRangeException>("batchSize", batchSize == 0, Exceptions.EnumerableExtensions_NonZeroBatch, new string[0]);
			int num = 0;
			T[] tArray = new T[batchSize];
			foreach (T t in items)
			{
				T[] tArray1 = tArray;
				int num1 = num;
				int num2 = num1;
				num = num1 + 1;
				tArray1[checked((int)((long)num2 % (long)batchSize))] = t;
				if ((long)num % (long)batchSize != (long)0)
				{
					continue;
				}
				yield return tArray;
			}
			if ((long)num % (long)batchSize > (long)0)
			{
				yield return tArray.Take<T>((int)(num % batchSize));
			}
		}

		public static IEnumerable<T> Interlace<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			using (IEnumerator<T> enumerator = first.EmptyIfNull<T>().GetEnumerator())
			{
				using (IEnumerator<T> enumerator1 = second.EmptyIfNull<T>().GetEnumerator())
				{
					while (enumerator.MoveNext() && enumerator1.MoveNext())
					{
						yield return enumerator.Current;
						yield return enumerator1.Current;
					}
				}
			}
		}

		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			return source.MaxBy<TSource, TKey>(selector, Comparer<TKey>.Default);
		}

		public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			return EnumerableExtensions.compareBy<TSource, TKey>(source, selector, comparer, (int i) => i > 0);
		}

		public static IEnumerable<Pair<T>> Merge<T>(this IEnumerable<T> firsts, IEnumerable<T> seconds)
		{
			bool i;
			using (IEnumerator<T> enumerator = firsts.EmptyIfNull<T>().GetEnumerator())
			{
				using (IEnumerator<T> enumerator1 = seconds.EmptyIfNull<T>().GetEnumerator())
				{
					bool flag = enumerator.MoveNext();
					for (i = enumerator1.MoveNext(); flag && i; i = enumerator1.MoveNext())
					{
						yield return new Pair<T>(enumerator.Current, enumerator1.Current);
						flag = enumerator.MoveNext();
					}
					if (!flag && !i)
					{
						goto Label0;
					}
					throw new ArgumentException("firsts && seconds not same length", "seconds");
				}
			}
		Label0:
			yield break;
		}

		public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			return source.MinBy<TSource, TKey>(selector, Comparer<TKey>.Default);
		}

		public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
		{
			return EnumerableExtensions.compareBy<TSource, TKey>(source, selector, comparer, (int i) => i < 0);
		}

		public static IEnumerable<T> NullIfEmpty<T>(this IEnumerable<T> source)
		{
			if (source != null && source.Any<T>())
			{
				return source;
			}
			return null;
		}

		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, params T[] inTheBeginning)
		{
			return inTheBeginning.EmptyIfNull<T>().Concat<T>(source);
		}

		public static IEnumerable<T> Selecting<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T t in source.EmptyIfNull<T>())
			{
				action(t);
				yield return t;
			}
		}

		public static IEnumerable<T> Selecting<T>(this IEnumerable<T> collection, Action<T, int> action)
		{
			int num = 0;
			foreach (T t in collection.EmptyIfNull<T>())
			{
				Action<T, int> action1 = action;
				T t1 = t;
				int num1 = num;
				int num2 = num1;
				num = num1 + 1;
				action1(t1, num2);
				yield return t;
			}
		}

		public static IEnumerable<T> Selecting<T>(this IEnumerable<T> collection, Action<T, int> action, IEnumerable<int> indexes)
		{
			HashSet<int> nums = new HashSet<int>(indexes);
			int num = 0;
			foreach (T t in collection.EmptyIfNull<T>())
			{
				if (nums.Contains(num))
				{
					action(t, num);
				}
				num++;
				yield return t;
			}
		}

		public static IEnumerable<T> Shuffle<T>(this IList<T> collection)
		{
			return collection.Shuffle<T>(new FrameworkRandomizer());
		}

		public static IEnumerable<T> Shuffle<T>(this IList<T> collection, int itemsToTake)
		{
			return collection.Shuffle<T>(new FrameworkRandomizer(), itemsToTake);
		}

		public static IEnumerable<T> Shuffle<T>(this IList<T> collection, IRandomizer provider)
		{
			return collection.Shuffle<T>(provider, (collection ?? (IList<T>)(new T[0])).Count);
		}

		public static IEnumerable<T> Shuffle<T>(this IList<T> collection, IRandomizer provider, int itemsToTake)
		{
			IList<T> ts;
			ts = (collection == null ? new List<T>(0) : collection.ToList<T>());
			IList<T> ts1 = ts;
			int num = Math.Min(ts1.Count, itemsToTake);
			if (ts1.Count > 0)
			{
				for (int i = 1; i <= num; i++)
				{
					int num1 = provider.Next(ts1.Count);
					yield return ts1[num1];
					ts1.RemoveAt(num1);
				}
			}
		}

		public static IEnumerable<T> SkipNulls<T>(this IEnumerable<T> source)
		where T : class
		{
			return 
				from s in source.EmptyIfNull<T>()
				where s != null
				select s;
		}

		public static IEnumerable<T> SortBy<T, U>(this IEnumerable<T> toBeSorted, Func<T, U> selector, IEnumerable<U> sorter)
		{
			return toBeSorted.SortBy<T, U>(selector, sorter, EqualityComparer<U>.Default);
		}

		public static IEnumerable<T> SortBy<T, U>(this IEnumerable<T> toBeSorted, Func<T, U> selector, IEnumerable<U> sorter, IEqualityComparer<U> comparer)
		{
			T t;
			Dictionary<U, T> dictionary = toBeSorted.ToDictionary<T, U, T>(selector, (T e) => e, comparer);
			foreach (U u in sorter)
			{
				if (!dictionary.TryGetValue(u, out t))
				{
					continue;
				}
				yield return t;
			}
		}

		public static IEnumerable<T> ToCircular<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable.EmptyIfNull<T>().Any<T>())
			{
				while (true)
				{
					foreach (T t in enumerable)
					{
						yield return t;
					}
				}
			}
		}

		public static string ToCsv<T>(this IEnumerable<T> source)
		{
			return source.ToDelimitedString<T>(",");
		}

		public static string ToCsv<T>(this IEnumerable<T> source, Func<T, string> toString)
		{
			return source.ToDelimitedString<T>(",", toString);
		}

		public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter, Func<T, string> toString)
		{
			return string.Join(delimiter, source.EmptyIfNull<T>().Select<T, string>(toString));
		}

		public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter)
		{
			return source.ToDelimitedString<T>(delimiter, (T t) => t.ToString());
		}

		public static string ToDelimitedString<T>(this IEnumerable<T> source, Func<T, string> toString)
		{
			return source.ToDelimitedString<T>(", ", toString);
		}

		public static string ToDelimitedString<T>(this IEnumerable<T> source)
		{
			return source.ToDelimitedString<T>(", ", (T t) => t.ToString());
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
		{
			return source.ToHashSet<T, T>((T e) => e);
		}

		public static HashSet<U> ToHashSet<T, U>(this IEnumerable<T> source, Func<T, U> selector)
		{
			return source.ToHashSet<T, U>(selector, null);
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
		{
			return source.ToHashSet<T, T>((T e) => e, comparer);
		}

		public static HashSet<U> ToHashSet<T, U>(this IEnumerable<T> source, Func<T, U> selector, IEqualityComparer<U> comparer)
		{
			Guard.AgainstNullArgument(new { source = source, selector = selector });
			return new HashSet<U>(source.Select<T, U>(selector), comparer);
		}

		public static IEnumerable<T> ToStepped<T>(this IEnumerable<T> enumerable, uint step)
		{
			Guard.AgainstArgument<ArgumentOutOfRangeException>("step", step < 1, "Cannot be negative or zero.", new string[0]);
			using (IEnumerator<T> enumerator = enumerable.EmptyIfNull<T>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					yield return enumerator.Current;
					uint num = step;
					while (num > 1 && enumerator.MoveNext())
					{
						num--;
					}
				}
			}
		}

		public static Tree<TItem, TModel, TKey> ToTree<TItem, TModel, TKey>(this IEnumerable<TItem> source, Func<TItem, TKey> key, Func<TItem, Tree<TItem, TModel, TKey>.Parent, Tree<TItem, TModel, TKey>.Parent.Key> parentKey, Func<TItem, TModel> projection, IEqualityComparer<TKey> comparer = null)
		{
			return new Tree<TItem, TModel, TKey>(source, key, parentKey, projection, comparer);
		}

		public static Tree<TModel, TKey> ToTree<TModel, TKey>(this IEnumerable<TModel> source, Func<TModel, TKey> key, Func<TModel, Tree<TModel, TModel, TKey>.Parent, Tree<TModel, TModel, TKey>.Parent.Key> parentKey, IEqualityComparer<TKey> comparer = null)
		{
			return new Tree<TModel, TKey>(source, key, parentKey, comparer);
		}

		public static IEnumerable<TResult> Zip<T1, T2, TResult>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, int, TResult> selector)
		{
			return first.Zip(second, (T1 a, T2 b) => new { a = a, b = b }).Select((pair, i) => selector(pair.a, pair.b, i));
		}

		public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
		{
			bool flag;
			IEnumerator<T1> enumerator = first.EmptyIfNull<T1>().GetEnumerator();
			IEnumerator<T2> enumerator1 = second.EmptyIfNull<T2>().GetEnumerator();
			do
			{
				flag = enumerator.MoveNext();
				bool flag1 = enumerator1.MoveNext();
				if (flag != flag1)
				{
					throw new InvalidOperationException(Exceptions.EnumerableExtensions_Zip_SameLength);
				}
				if (!flag1)
				{
					continue;
				}
				yield return Tuple.Create<T1, T2>(enumerator.Current, enumerator1.Current);
			}
			while (flag);
		}
	}
}