using System;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Comparisons
{
	public abstract class ChainableEqualizer<T> : IEqualityComparer<T>
	{
		private ChainableEqualizer<T> _nextEqualizer;

		private ChainableEqualizer<T> _lastEqualizer;

		protected ChainableEqualizer()
		{
		}

		private void chain(ChainableEqualizer<T> lastEqualizer)
		{
			if (this._nextEqualizer == null)
			{
				this._nextEqualizer = lastEqualizer;
				return;
			}
			this._nextEqualizer.chain(lastEqualizer);
		}

		protected abstract bool DoEquals(T x, T y);

		protected abstract int DoGetHashCode(T obj);

		public bool Equals(T x, T y)
		{
			bool? nullable = ChainableEqualizer<T>.handleNulls(x, y);
			if (nullable.HasValue)
			{
				return nullable.Value;
			}
			bool flag = this.DoEquals(x, y);
			if (this.needsToEvaluateNext(flag))
			{
				flag = this._nextEqualizer.Equals(x, y);
			}
			return flag;
		}

		public int GetHashCode(T x)
		{
			return this.DoGetHashCode(x);
		}

		private static bool? handleNulls(T x, T y)
		{
			bool? nullable = null;
			if (!typeof(T).IsValueType)
			{
				if (x == null)
				{
					nullable = new bool?(y == null);
				}
				else if (y == null)
				{
					nullable = new bool?(false);
				}
			}
			return nullable;
		}

		private bool needsToEvaluateNext(bool ret)
		{
			if (!ret)
			{
				return false;
			}
			return this._nextEqualizer != null;
		}

		public ChainableEqualizer<T> Then(ChainableEqualizer<T> equalizer)
		{
			if (this._nextEqualizer != null)
			{
				this._lastEqualizer.chain(equalizer);
			}
			else
			{
				this._nextEqualizer = equalizer;
			}
			this._lastEqualizer = equalizer;
			return this;
		}
	}
}