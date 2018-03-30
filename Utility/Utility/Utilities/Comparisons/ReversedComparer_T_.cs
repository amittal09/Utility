using System;
using System.Collections.Generic;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Comparisons
{
	public class ReversedComparer<T> : ChainableComparer<T>
	{
		private readonly IComparer<T> _inner;

		public ReversedComparer(IComparer<T> inner, Direction direction = 0) : base(direction)
		{
			Guard.AgainstNullArgument("inner", inner);
			this._inner = inner;
		}

		protected override int DoCompare(T x, T y)
		{
			return this._inner.Compare(y, x);
		}
	}
}