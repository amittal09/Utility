using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Comparisons
{
	public static class ChainableExtensions
	{
		public static ChainableComparer<T> Then<T>(this ChainableComparer<T> chainable, Comparison<T> next)
		{
			return chainable.Then(new ComparisonComparer<T>(next));
		}

		public static ChainableComparer<T> Then<T>(this ChainableComparer<T> chainable, Comparison<T> next, Direction sortDirection)
		{
			return chainable.Then(new ComparisonComparer<T>(next, sortDirection));
		}

		public static ChainableEqualizer<T> Then<T>(this ChainableEqualizer<T> chainable, Func<T, T, bool> equals, Func<T, int> hasher)
		{
			return chainable.Then(new DelegatedEqualizer<T>(equals, hasher));
		}

		public static ChainableEqualizer<T> Then<T, U>(this ChainableEqualizer<T> chainable, Func<T, U> keySelector)
		{
			return chainable.Then(new SelectorEqualizer<T, U>(keySelector));
		}

		public static ChainableComparer<T> Then<T, U>(this ChainableComparer<T> chainable, Func<T, U> keySelector)
		{
			return chainable.Then(new SelectorComparer<T, U>(keySelector, Direction.Ascending));
		}

		public static ChainableComparer<T> Then<T, U>(this ChainableComparer<T> chainable, Func<T, U> keySelector, Direction sortDirection)
		{
			return chainable.Then(new SelectorComparer<T, U>(keySelector, sortDirection));
		}
	}
}