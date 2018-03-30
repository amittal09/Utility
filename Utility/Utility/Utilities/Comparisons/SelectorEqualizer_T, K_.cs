using System;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Comparisons
{
	public class SelectorEqualizer<T, K> : ChainableEqualizer<T>
	{
		private readonly Func<T, K> _selector;

		public SelectorEqualizer(Func<T, K> selector)
		{
			this._selector = selector;
		}

		protected override bool DoEquals(T x, T y)
		{
			K k = this.keyFor(x);
			K k1 = this.keyFor(y);
			return EqualityComparer<K>.Default.Equals(k, k1);
		}

		protected override int DoGetHashCode(T obj)
		{
			K k = this.keyFor(obj);
			return EqualityComparer<K>.Default.GetHashCode(k);
		}

		private K keyFor(T obj)
		{
			return this._selector(obj);
		}
	}
}