using System;

namespace Vertica.Utilities_v4.Patterns
{
	public interface ISpecification<T>
	{
		ISpecification<T> And(ISpecification<T> other);

		bool IsSatisfiedBy(T item);

		ISpecification<T> Not();

		ISpecification<T> Or(ISpecification<T> other);
	}
}