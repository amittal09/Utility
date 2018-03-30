using System;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Comparisons
{
	public class SelectorComparer<T, K> : ChainableComparer<T>
	{
		private readonly Func<T, K> _keySelector;

		public Comparison<T> Comparison
		{
			get
			{
				return new Comparison<T>(this.Compare);
			}
		}

		public SelectorComparer(Func<T, K> keySelector, Direction sortDirection = 0) : base(sortDirection)
		{
			this._keySelector = keySelector;
		}

		protected override int DoCompare(T x, T y)
		{
			Comparer<K> @default = Comparer<K>.Default;
			return @default.Compare(this._keySelector(x), this._keySelector(y));
		}
	}
}