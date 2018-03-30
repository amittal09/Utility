using System;
using System.Linq.Expressions;

namespace Vertica.Utilities_v4.Patterns
{
	public static class ExpressionSpecification
	{
		public static ExpressionSpecification<T> For<T>(Expression<Func<T, bool>> expression)
		{
			return new ExpressionSpecification<T>(expression);
		}
	}
}