using System;

namespace Vertica.Utilities_v4.Comparisons
{
	public class ComparisonComparer<T> : ChainableComparer<T>
	{
		private readonly Comparison<T> _comparison;

		public Comparison<T> Comparison
		{
			get
			{
				return new Comparison<T>(this.Compare);
			}
		}

		public ComparisonComparer(Comparison<T> comparison) : this(comparison, Direction.Ascending)
		{
		}

		public ComparisonComparer(Comparison<T> comparison, Direction sortDirection) : base(sortDirection)
		{
			this._comparison = comparison;
		}

		protected override int DoCompare(T x, T y)
		{
			return this._comparison(x, y);
		}
	}
}