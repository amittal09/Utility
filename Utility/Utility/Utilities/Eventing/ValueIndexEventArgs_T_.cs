using System;

namespace Vertica.Utilities_v4.Eventing
{
	public class ValueIndexEventArgs<T> : ValueEventArgs<T>, IIndexEventArgs
	{
		private readonly int _index;

		public int Index
		{
			get
			{
				return this._index;
			}
		}

		public ValueIndexEventArgs(int index, T value) : base(value)
		{
			this._index = index;
		}
	}
}