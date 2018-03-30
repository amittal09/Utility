using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Comparisons
{
	public class DelegatedEqualizer<T> : ChainableEqualizer<T>
	{
		private readonly Func<T, T, bool> _equals;

		private readonly Func<T, int> _hasher;

		public DelegatedEqualizer(Func<T, T, bool> equals, Func<T, int> hasher)
		{
			this._equals = equals;
			this._hasher = hasher;
		}

		public DelegatedEqualizer(Comparison<T> comparison, Func<T, int> hasher) : this(new Vertica.Utilities_v4.Comparisons.ComparisonComparer<T>(comparison), hasher)
		{
		}

		public DelegatedEqualizer(IComparer<T> comparer, Func<T, int> hasher) : this((T x, T y) => comparer.Compare(x, y) == 0, hasher)
		{
		}

		protected override bool DoEquals(T x, T y)
		{
			return this._equals(x, y);
		}

		protected override int DoGetHashCode(T obj)
		{
			return this._hasher(obj);
		}
	}
}