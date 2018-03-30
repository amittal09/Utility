using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Extensions.StringExt;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4
{
	public struct Pair<T> : IEnumerable<T>, IEnumerable, IEquatable<Pair<T>>
	{
		public T First
		{
			get;
			private set;
		}

		public T Second
		{
			get;
			private set;
		}

		public Pair(T first, T second)
		{
			this = new Pair<T>()
			{
				First = first,
				Second = second
			};
		}

		public bool Equals(Pair<T> other)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (!@default.Equals(this.First, other.First))
			{
				return false;
			}
			return @default.Equals(this.Second, other.Second);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (!(obj is Pair<T>))
			{
				return false;
			}
			return this.Equals((Pair<T>)obj);
		}

		public IEnumerator<T> GetEnumerator()
		{
			yield return this.First;
			yield return this.Second;
		}

		public override int GetHashCode()
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			return @default.GetHashCode(this.First) * 397 ^ @default.GetHashCode(this.Second);
		}

		public static bool operator ==(Pair<T> left, Pair<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Pair<T> left, Pair<T> right)
		{
			return !left.Equals(right);
		}

		public static Pair<T> Parse(string pair, char tokenizer)
		{
			Pair<T> ts = new Pair<T>();
			if (pair.IsNotEmpty())
			{
				string[] strArrays = pair.Split(new char[] { tokenizer });
				string tuploidsParseTemplate = Exceptions.Tuploids_ParseTemplate;
				string[] str = new string[] { "2", tokenizer.ToString(CultureInfo.InvariantCulture) };
				Guard.AgainstArgument("pair", (int)strArrays.Length != 2, tuploidsParseTemplate, str);
				ts = new Pair<T>(strArrays[0].Parse<T>(), strArrays[1].Parse<T>());
			}
			return ts;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)(object)this).GetEnumerator();
		}

		public KeyValuePair<T, T> ToKeyValuePair()
		{
			return new KeyValuePair<T, T>(this.First, this.Second);
		}
	}
}