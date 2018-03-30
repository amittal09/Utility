using System;
using System.Linq.Expressions;

namespace Vertica.Utilities_v4.Reflection
{
	public static class Value
	{
		public static T Of<T>(Expression<Func<T>> argumentExpression)
		{
			return argumentExpression.Compile()();
		}
	}
}