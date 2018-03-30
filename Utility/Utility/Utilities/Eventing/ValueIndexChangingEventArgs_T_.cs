using System;

namespace Vertica.Utilities_v4.Eventing
{
	public class ValueIndexChangingEventArgs<T> : ValueChangingEventArgs<T>, IIndexEventArgs
	{
		private readonly int _index;

		public int Index
		{
			get
			{
				return this._index;
			}
		}

		public ValueIndexChangingEventArgs(int index, T oldValue, T newValue) : base(oldValue, newValue)
		{
			this._index = index;
		}
	}
}