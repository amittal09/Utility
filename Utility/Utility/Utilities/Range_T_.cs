using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Extensions.ComparableExt;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4
{
	[Serializable]
	public class Range<T> : IEquatable<Range<T>>
	where T : IComparable<T>
	{
		private readonly IBound<T> _lowerBound;

		private readonly IBound<T> _upperBound;

		private static Func<T, T> _nextGenerator;

		public static Range<T> Empty
		{
			get
			{
				return Range<T>.EmptyRange<T>.Instance;
			}
		}

		public T LowerBound
		{
			get
			{
				return this._lowerBound.Value;
			}
		}

		public T UpperBound
		{
			get
			{
				return this._upperBound.Value;
			}
		}

		private Range()
		{
			this._lowerBound = new Open<T>(default(T));
			this._upperBound = new Open<T>(default(T));
		}

		public Range(IBound<T> lowerBound, IBound<T> upperBound)
		{
			Range<T>.AssertBounds(lowerBound, upperBound);
			this._lowerBound = lowerBound;
			this._upperBound = upperBound;
		}

		public Range(T lowerBound, T upperBound)
		{
			Range<T>.AssertBounds(lowerBound, upperBound);
			this._lowerBound = new Closed<T>(lowerBound);
			this._upperBound = new Closed<T>(upperBound);
		}

		public void AssertArgument(string paramName, T value)
		{
			if (!this.Contains(value))
			{
				string str = string.Format(Exceptions.Range_ArgumentAssertion_Template, this._lowerBound.ToAssertion(), this._upperBound.ToAssertion(), this);
				throw new ArgumentOutOfRangeException(paramName, (object)value, str);
			}
		}

		public void AssertArgument(string paramName, IEnumerable<T> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (T value in values)
			{
				this.AssertArgument(paramName, value);
			}
		}

		public static void AssertBounds(T lowerBound, T upperBound)
		{
			if (!Range<T>.CheckBounds(lowerBound, upperBound))
			{
				throw Range<T>.exception(lowerBound, upperBound);
			}
		}

		public static void AssertBounds(IBound<T> lowerBound, IBound<T> upperBound)
		{
			if (!Range<T>.CheckBounds(lowerBound, upperBound))
			{
				throw Range<T>.exception(lowerBound.Value, upperBound.Value);
			}
		}

		public static bool CheckBounds(T lowerBound, T upperBound)
		{
			return lowerBound.IsAtMost<T>(upperBound);
		}

		public static bool CheckBounds(IBound<T> lowerBound, IBound<T> upperBound)
		{
			return lowerBound.LessThan(upperBound.Value);
		}

		public virtual bool Contains(T item)
		{
			if (!this._lowerBound.LessThan(item))
			{
				return false;
			}
			return this._upperBound.MoreThan(item);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != typeof(Range<T>))
			{
				return false;
			}
			return this.Equals((Range<T>)obj);
		}

		public bool Equals(Range<T> other)
		{
			if (object.ReferenceEquals(null, other))
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (!object.Equals(other._lowerBound, this._lowerBound))
			{
				return false;
			}
			return object.Equals(other._upperBound, this._upperBound);
		}

		private static ArgumentOutOfRangeException exception(T lowerBound, T upperBound)
		{
			string str = string.Format(Exceptions.Range_UnorderedBounds_Template, lowerBound, upperBound);
			return new ArgumentOutOfRangeException("upperBound", (object)upperBound, str);
		}

		public virtual IEnumerable<T> Generate(Func<T, T> nextGenerator)
		{
			T t = default(T);
			for (T i = this._lowerBound.Generate(nextGenerator); this._upperBound.MoreThan(i); i = t)
			{
				yield return i;
				t = nextGenerator(i);
				if (t.IsAtMost<T>(i))
				{
					throw new ArgumentException(Exceptions.Range_NotIncrementingGenerator, "nextGenerator");
				}
			}
		}

		public virtual IEnumerable<T> Generate(T increment)
		{
			Range<T>._nextGenerator = Range<T>._nextGenerator ?? Range<T>.initNextGenerator(increment);
			return this.Generate(Range<T>._nextGenerator);
		}

		public override int GetHashCode()
		{
			return this._lowerBound.GetHashCode() * 397 ^ this._upperBound.GetHashCode();
		}

		private static Func<T, T> initNextGenerator(T step)
		{
            //return ((T current) => current + step);
            return null;
		}

		public virtual Range<T> Intersect(Range<T> range)
		{
			Range<T> empty = Range<T>.Empty;
			if (range != null && !object.ReferenceEquals(range, Range<T>.Empty))
			{
				if (this._lowerBound.Touches(range._upperBound))
				{
					empty = Range.Degenerate<T>(this.LowerBound);
				}
				else if (this._upperBound.Touches(range._lowerBound))
				{
					empty = Range.Degenerate<T>(this.UpperBound);
				}
				else if (this.LowerBound.IsLessThan<T>(range.UpperBound) && this.UpperBound.IsMoreThan<T>(range.LowerBound))
				{
					IBound<T> bound = Range<T>.max(this._lowerBound, range._lowerBound, new Func<IBound<T>, IBound<T>, IBound<T>>(Range<T>.Restrictive.More));
					IBound<T> bound1 = Range<T>.min(this._upperBound, range._upperBound, new Func<IBound<T>, IBound<T>, IBound<T>>(Range<T>.Restrictive.More));
					empty = new Range<T>(bound, bound1);
				}
			}
			return empty;
		}

		public virtual Range<T> Join(Range<T> range)
		{
			if (range == null || object.ReferenceEquals(range, Range<T>.Empty))
			{
				return this;
			}
			IBound<T> bound = Range<T>.min(this._lowerBound, range._lowerBound, new Func<IBound<T>, IBound<T>, IBound<T>>(Range<T>.Restrictive.Less));
			IBound<T> bound1 = Range<T>.max(this._upperBound, range._upperBound, new Func<IBound<T>, IBound<T>, IBound<T>>(Range<T>.Restrictive.Less));
			return new Range<T>(bound, bound1);
		}

		public virtual T Limit(T value)
		{
			return Range<T>.Limit(value, this.LowerBound, this.UpperBound);
		}

		private static T Limit(T value, T lowerBound, T upperBound)
		{
			T t = value;
			if (value.IsMoreThan<T>(upperBound))
			{
				t = upperBound;
			}
			if (value.IsLessThan<T>(lowerBound))
			{
				t = lowerBound;
			}
			return t;
		}

		public virtual T LimitLower(T value)
		{
			return Range<T>.Limit(value, this.LowerBound, value);
		}

		public virtual T LimitUpper(T value)
		{
			return Range<T>.Limit(value, value, this.UpperBound);
		}

		private static IBound<T> max(IBound<T> x, IBound<T> y, Func<IBound<T>, IBound<T>, IBound<T>> equalSelection)
		{
			IBound<T> bound;
			if (!x.Value.IsEqualTo<T>(y.Value))
			{
				bound = (x.Value.IsMoreThan<T>(y.Value) ? x : y);
			}
			else
			{
				bound = equalSelection(x, y);
			}
			return bound;
		}

		private static IBound<T> min(IBound<T> x, IBound<T> y, Func<IBound<T>, IBound<T>, IBound<T>> equalSelection)
		{
			IBound<T> bound;
			if (!x.Value.IsEqualTo<T>(y.Value))
			{
				bound = (x.Value.IsLessThan<T>(y.Value) ? x : y);
			}
			else
			{
				bound = equalSelection(x, y);
			}
			return bound;
		}

		public virtual bool Overlaps(Range<T> range)
		{
			bool flag;
			if (range == null || object.ReferenceEquals(range, Range<T>.Empty))
			{
				return false;
			}
			if (this._lowerBound.Touches(range._upperBound) || this._upperBound.Touches(range._lowerBound))
			{
				flag = true;
			}
			else
			{
				flag = (!this.LowerBound.IsLessThan<T>(range.UpperBound) ? false : this.UpperBound.IsMoreThan<T>(range.LowerBound));
			}
			return flag;
		}

		public override string ToString()
		{
			return string.Format("{0}..{1}", this._lowerBound.Lower(), this._upperBound.Upper());
		}

		private sealed class EmptyRange<U> : Range<U>
		where U : IComparable<U>
		{
			public static Range<U> Instance
			{
				get
				{
					return Range<T>.EmptyRange<U>.Nested.instance;
				}
			}

			private EmptyRange()
			{
			}

			public override bool Contains(U item)
			{
				return false;
			}

			public override IEnumerable<U> Generate(U increment)
			{
				return Enumerable.Empty<U>();
			}

			public override IEnumerable<U> Generate(Func<U, U> increment)
			{
				return Enumerable.Empty<U>();
			}

			public override Range<U> Intersect(Range<U> range)
			{
				return this;
			}

			public override Range<U> Join(Range<U> range)
			{
				object obj = range;
				if (obj == null)
				{
					obj = this;
				}
				return (Range<U>)obj;
			}

			public override U Limit(U value)
			{
				return value;
			}

			public override U LimitLower(U value)
			{
				return value;
			}

			public override U LimitUpper(U value)
			{
				return value;
			}

			private class Nested
			{
				internal readonly static Range<U> instance;

				static Nested()
				{
					Range<T>.EmptyRange<U>.Nested.instance = new Range<U>.EmptyRange<U>();
				}

				public Nested()
				{
				}
			}
		}

		internal static class Restrictive
		{
			private static void assertArguments(IBound<T> x, IBound<T> y)
			{
				Guard.AgainstArgument("y", !x.Value.IsEqualTo<T>(y.Value), "Bound values need to be equal to check restrictiveness.", new string[0]);
			}

			public static IBound<T> Less(IBound<T> x, IBound<T> y)
			{
				Range<T>.Restrictive.assertArguments(x, y);
				if (x.IsClosed)
				{
					return x;
				}
				if (!y.IsClosed)
				{
					return x;
				}
				return y;
			}

			public static IBound<T> More(IBound<T> x, IBound<T> y)
			{
				Range<T>.Restrictive.assertArguments(x, y);
				if (x.IsClosed)
				{
					return y;
				}
				if (!y.IsClosed)
				{
					return y;
				}
				return x;
			}
		}
	}
}