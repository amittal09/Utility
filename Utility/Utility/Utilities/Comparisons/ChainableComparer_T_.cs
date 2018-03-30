using System;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Comparisons
{
	public abstract class ChainableComparer<T> : IComparer<T>
	{
		private readonly Direction _direction;

		private ChainableComparer<T> _nextComparer;

		private ChainableComparer<T> _lastComparer;

		public Direction SortDirection
		{
			get
			{
				return this._direction;
			}
		}

		protected ChainableComparer(Direction sortDirection = 0)
		{
			this._direction = sortDirection;
		}

		public void chain(ChainableComparer<T> lastComparer)
		{
			if (this._nextComparer == null)
			{
				this._nextComparer = lastComparer;
				return;
			}
			this._nextComparer.chain(lastComparer);
		}

		public int Compare(T x, T y)
		{
			int? nullable = ChainableComparer<T>.handleNulls(x, y);
			if (nullable.HasValue)
			{
				return nullable.Value;
			}
			int num = this.DoCompare(x, y);
			if (this.needsToEvaluateNext(num))
			{
				num = this._nextComparer.Compare(x, y);
			}
			if (this._direction == Direction.Descending)
			{
				ChainableComparer<T>.invert(ref num);
			}
			return num;
		}

		protected abstract int DoCompare(T x, T y);

		private static int? handleNulls(T x, T y)
		{
			int? nullable = null;
			if (!typeof(T).IsValueType)
			{
				if (x == null)
				{
					nullable = new int?((y == null ? 0 : -1));
				}
				else if (y == null)
				{
					nullable = new int?(1);
				}
			}
			return nullable;
		}

		private static void invert(ref int result)
		{
			result *= -1;
		}

		private bool needsToEvaluateNext(int ret)
		{
			if (ret != 0)
			{
				return false;
			}
			return this._nextComparer != null;
		}

		public ChainableComparer<T> Then(ChainableComparer<T> comparer)
		{
			if (this._nextComparer != null)
			{
				this._lastComparer.chain(comparer);
			}
			else
			{
				this._nextComparer = comparer;
			}
			this._lastComparer = comparer;
			return this;
		}
	}
}