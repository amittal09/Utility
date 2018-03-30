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
	public struct Triplet<T> : IEnumerable<T>, IEnumerable, IEquatable<Triplet<T>>
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

		public T Third
		{
			get;
			private set;
		}

		public Triplet(T first, T second, T third)
		{
			this = new Triplet<T>()
			{
				First = first,
				Second = second,
				Third = third
			};
		}

		public bool Equals(Triplet<T> other)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (!@default.Equals(this.First, other.First) || !@default.Equals(this.Second, other.Second))
			{
				return false;
			}
			return @default.Equals(this.Third, other.Third);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (!(obj is Triplet<T>))
			{
				return false;
			}
			return this.Equals((Triplet<T>)obj);
		}

		public IEnumerator<T> GetEnumerator()
		{
			yield return this.First;
			yield return this.Second;
			yield return this.Third;
		}

		public override int GetHashCode()
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int hashCode = @default.GetHashCode(this.First);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Second);
			hashCode = hashCode * 397 ^ @default.GetHashCode(this.Third);
			return hashCode;
		}

		public static bool operator ==(Triplet<T> left, Triplet<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Triplet<T> left, Triplet<T> right)
		{
			return !left.Equals(right);
		}

		public static Triplet<T> Parse(string triplet, char tokenizer)
		{
			Triplet<T> ts = new Triplet<T>();
			if (triplet.IsNotEmpty())
			{
				string[] strArrays = triplet.Split(new char[] { tokenizer });
				string tuploidsParseTemplate = Exceptions.Tuploids_ParseTemplate;
				string[] str = new string[] { "3", tokenizer.ToString(CultureInfo.InvariantCulture) };
				Guard.AgainstArgument("pair", (int)strArrays.Length != 2, tuploidsParseTemplate, str);
				ts = new Triplet<T>(strArrays[0].Parse<T>(), strArrays[1].Parse<T>(), strArrays[2].Parse<T>());
			}
			return ts;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)(object)this).GetEnumerator();
		}
	}
}