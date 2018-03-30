using System;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Comparisons
{
	public class StringRepresentationComparer<T> : ChainableComparer<string>
	{
		private readonly Func<string, T> _converter;

		private readonly Comparer<T> _comparer;

		public StringRepresentationComparer(Func<string, T> converter, Direction direction = 0) : base(direction)
		{
			this._converter = converter;
			this._comparer = Comparer<T>.Default;
		}

		protected override int DoCompare(string x, string y)
		{
			return this._comparer.Compare(this._converter(x), this._converter(y));
		}
	}
}