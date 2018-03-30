using System;
using System.Collections.Generic;
using Vertica.Utilities_v4.Extensions.ComparableExt;

namespace Vertica.Utilities_v4
{
	[Serializable]
	internal struct Closed<T> : IBound<T>, IEquatable<IBound<T>>
	where T : IComparable<T>
	{
		private readonly T _value;

		public bool IsClosed
		{
			get
			{
				return true;
			}
		}

		public T Value
		{
			get
			{
				return this._value;
			}
		}

		public Closed(T value)
		{
			this._value = value;
		}

		public bool Equals(IBound<T> other)
		{
			if (!(other is Closed<T>))
			{
				return false;
			}
			return EqualityComparer<T>.Default.Equals(this.Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (!(obj is Closed<T>))
			{
				return false;
			}
			return this.Equals((IBound<T>)(Closed<T>)obj);
		}

		public T Generate(Func<T, T> nextGenerator)
		{
			return this._value;
		}

		public override int GetHashCode()
		{
			return EqualityComparer<T>.Default.GetHashCode(this._value) * 397 ^ 1;
		}

		public bool LessThan(T other)
		{
			return this._value.IsAtMost<T>(other);
		}

		public string Lower()
		{
			return string.Concat("[", this._value);
		}

		public bool MoreThan(T other)
		{
			return this._value.IsAtLeast<T>(other);
		}

		public string ToAssertion()
		{
			return string.Concat(this._value, " (inclusive)");
		}

		public bool Touches(IBound<T> bound)
		{
			if (!bound.IsClosed)
			{
				return false;
			}
			return this.Value.IsEqualTo<T>(bound.Value);
		}

		public string Upper()
		{
			return string.Concat(this._value, "]");
		}
	}
}