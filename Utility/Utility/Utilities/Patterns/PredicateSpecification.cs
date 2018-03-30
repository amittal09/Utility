using System;

namespace Vertica.Utilities_v4.Patterns
{
	public static class PredicateSpecification
	{
		public static PredicateSpecification<T> For<T>(Predicate<T> predicate)
		{
			return new PredicateSpecification<T>(predicate);
		}
	}
}