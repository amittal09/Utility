using System;

namespace Vertica.Utilities_v4
{
	public interface IBound<T> : IEquatable<IBound<T>>
	where T : IComparable<T>
	{
		bool IsClosed
		{
			get;
		}

		T Value
		{
			get;
		}

		T Generate(Func<T, T> nextGenerator);

		bool LessThan(T other);

		string Lower();

		bool MoreThan(T other);

		string ToAssertion();

		bool Touches(IBound<T> bound);

		string Upper();
	}
}