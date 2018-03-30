using System;

namespace Vertica.Utilities_v4.Collections
{
	public struct SmartEntry<T>
	{
		private readonly T _value;

		private readonly bool _isFirst;

		private readonly bool _isLast;

		private readonly int _index;

		public int Index
		{
			get
			{
				return this._index;
			}
		}

		public bool IsFirst
		{
			get
			{
				return this._isFirst;
			}
		}

		public bool IsLast
		{
			get
			{
				return this._isLast;
			}
		}

		public T Value
		{
			get
			{
				return this._value;
			}
		}

		internal SmartEntry(bool isFirst, bool isLast, T value, int index)
		{
			this._isFirst = isFirst;
			this._isLast = isLast;
			this._value = value;
			this._index = index;
		}
	}
}