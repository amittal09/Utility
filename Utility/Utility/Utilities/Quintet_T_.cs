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
	public struct Quintet<T> : IEnumerable<T>, IEnumerable, IEquatable<Quintet<T>>
	{
		public T Fifth
		{
			get;
			private set;
		}

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

		public Quintet(T first, T second, T third, T fourth, T fifth)
		{
			this = new Quintet<T>()
			{
				First = first,
				Second = second,
				Third = third,
				Fourth = fourth,
				Fifth = fifth
			};
		}

		public bool Equals(Quintet<T> other)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (!@default.Equals(this.First, other.First) || !@default.Equals(this.Second, other.Second) || !@default.Equals(this.Third, other.Third) || !@default.Equals(this.Fourth, other.Fourth))
			{
				return false;
			}
			return @default.Equals(this.Fifth, other.Fifth);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (!(obj is Quintet<T>))
			{
				return false;
			}
			return this.Equals((Quintet<T>)obj);
		}

		public IEnumerator<T> GetEnumerator()
		{
			yield return this.First;
			yield return this.Second;
			yield return this.Third;
			yield return this.Fourth;
			yield return this.Fifth;
		}

		public override int GetHashCode()
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int hashCode = @default.GetHashCode(this.First);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Second);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Third);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Fourth);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Fifth);
			return hashCode;
		}

		public static bool operator ==(Quintet<T> left, Quintet<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Quintet<T> left, Quintet<T> right)
		{
			return !left.Equals(right);
		}

		public static Quintet<T> Parse(string quintet, char tokenizer)
		{
			Quintet<T> ts = new Quintet<T>();
			if (quintet.IsNotEmpty())
			{
				string[] strArrays = quintet.Split(new char[] { tokenizer });
				string tuploidsParseTemplate = Exceptions.Tuploids_ParseTemplate;
				string[] str = new string[] { "5", tokenizer.ToString(CultureInfo.InvariantCulture) };
				Guard.AgainstArgument("pair", (int)strArrays.Length != 5, tuploidsParseTemplate, str);
				ts = new Quintet<T>(strArrays[0].Parse<T>(), strArrays[1].Parse<T>(), strArrays[2].Parse<T>(), strArrays[3].Parse<T>(), strArrays[5].Parse<T>());
			}
			return ts;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)(object)this).GetEnumerator();
		}
	}
}