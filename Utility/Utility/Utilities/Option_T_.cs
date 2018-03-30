using System;
using System.Collections.Generic;

namespace Vertica.Utilities_v4
{
	public sealed class Option<T> : IEquatable<Option<T>>
	{
		private readonly T _value;

		private bool _defaultChanged;

		private T _default;

		private readonly static Option<T> _none;

		private T @default
		{
			get
			{
				return this._default;
			}
			set
			{
				this._default = value;
				this._defaultChanged = true;
			}
		}

		public bool IsNone
		{
			get
			{
				if (this == Option<T>._none)
				{
					return true;
				}
				return this._defaultChanged;
			}
		}

		public bool IsSome
		{
			get
			{
				return !this.IsNone;
			}
		}

		public static Option<T> None
		{
			get
			{
				return Option<T>._none;
			}
		}

		public T Value
		{
			get
			{
				if (!this.IsSome)
				{
					throw new InvalidOperationException();
				}
				return this._value;
			}
		}

		public T ValueOrDefault
		{
			get
			{
				if (!this.IsSome)
				{
					return this.@default;
				}
				return this._value;
			}
		}

		static Option()
		{
			Option<T>._none = new Option<T>();
		}

		private Option()
		{
		}

		private Option(T value)
		{
			this._value = value;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Option<T>))
			{
				return false;
			}
			return this.Equals((Option<T>)obj);
		}

		public bool Equals(Option<T> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.IsNone)
			{
				if (!other.IsNone)
				{
					return false;
				}
				return EqualityComparer<T>.Default.Equals(this._default, other._default);
			}
			if (!other.IsSome)
			{
				return false;
			}
			return EqualityComparer<T>.Default.Equals(this._value, other._value);
		}

		public override int GetHashCode()
		{
			if (this.IsNone)
			{
				return 0;
			}
			return EqualityComparer<T>.Default.GetHashCode(this._value);
		}

		public T GetValueOrDefault(T defaultValue)
		{
			if (!this.IsSome)
			{
				return defaultValue;
			}
			return this.Value;
		}

		public static Option<T> NoneWithDefault(T defaultValue)
		{
			return new Option<T>()
			{
				@default = defaultValue
			};
		}

		public static Option<T> Some(T value)
		{
			return new Option<T>(value);
		}
	}
}