using System;
using System.Collections.Generic;
using Vertica.Utilities_v4.Extensions.ComparableExt;

namespace Vertica.Utilities_v4
{
	[Serializable]
	internal struct Open<T> : IBound<T>, IEquatable<IBound<T>>
	where T : IComparable<T>
	{
		private readonly T _value;

		public bool IsClosed
		{
			get
			{
				return false;
			}
		}

		public T Value
		{
			get
			{
				return this._value;
			}
		}

		public Open(T value)
		{
			this._value = value;
		}

		public bool Equals(IBound<T> other)
		{
			if (!(other is Open<T>))
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
			if (!(obj is Open<T>))
			{
				return false;
			}
			return this.Equals((IBound<T>)(Open<T>)obj);
		}

		public T Generate(Func<T, T> nextGenerator)
		{
			return nextGenerator(this._value);
		}

		public override int GetHashCode()
		{
			return EqualityComparer<T>.Default.GetHashCode(this._value) * 397 ^ 2;
		}

		public bool LessThan(T other)
		{
			return this._value.IsLessThan<T>(other);
		}

		public string Lower()
		{
			return string.Concat("(", this._value);
		}

		public bool MoreThan(T other)
		{
			return this._value.IsMoreThan<T>(other);
		}

		public string ToAssertion()
		{
			return string.Concat(this._value, " (not inclusive)");
		}

		public bool Touches(IBound<T> bound)
		{
			return false;
		}

		public string Upper()
		{
			return string.Concat(this._value, ")");
		}
	}
}