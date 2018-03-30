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
	public struct Quartet<T> : IEnumerable<T>, IEnumerable, IEquatable<Quartet<T>>
	{
		public T First
		{
			get;
			private set;
		}

		public T Fourth
		{
			get;
			private set;
		}

		public T Second
		{
			get;
			private set;
		}

		public T Third
		{
			get;
			private set;
		}

		public Quartet(T first, T second, T third, T fourth)
		{
			this = new Quartet<T>()
			{
				First = first,
				Second = second,
				Third = third,
				Fourth = fourth
			};
		}

		public bool Equals(Quartet<T> other)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (!@default.Equals(this.First, other.First) || !@default.Equals(this.Second, other.Second) || !@default.Equals(this.Third, other.Third))
			{
				return false;
			}
			return @default.Equals(this.Fourth, other.Fourth);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (!(obj is Quartet<T>))
			{
				return false;
			}
			return this.Equals((Quartet<T>)obj);
		}

		public IEnumerator<T> GetEnumerator()
		{
			yield return this.First;
			yield return this.Second;
			yield return this.Third;
			yield return this.Fourth;
		}

		public override int GetHashCode()
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int hashCode = @default.GetHashCode(this.First);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Second);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Third);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Fourth);
			return hashCode;
		}

		public static bool operator ==(Quartet<T> left, Quartet<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Quartet<T> left, Quartet<T> right)
		{
			return !left.Equals(right);
		}

		public static Quartet<T> Parse(string quintet, char tokenizer)
		{
			Quartet<T> ts = new Quartet<T>();
			if (quintet.IsNotEmpty())
			{
				string[] strArrays = quintet.Split(new char[] { tokenizer });
				string tuploidsParseTemplate = Exceptions.Tuploids_ParseTemplate;
				string[] str = new string[] { "4", tokenizer.ToString(CultureInfo.InvariantCulture) };
				Guard.AgainstArgument("pair", (int)strArrays.Length != 4, tuploidsParseTemplate, str);
				ts = new Quartet<T>(strArrays[0].Parse<T>(), strArrays[1].Parse<T>(), strArrays[2].Parse<T>(), strArrays[3].Parse<T>());
			}
			return ts;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)(object)this).GetEnumerator();
		}
	}
}