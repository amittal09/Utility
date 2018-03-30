using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Vertica.Utilities_v4.Reflection
{
	public static class Name
	{
		public static string Of<T>(Expression<Func<T>> argumentExpression)
		{
			return ((MemberExpression)argumentExpression.Body).Member.Name;
		}

		public static string Of<T, U>(Expression<Func<T, U>> argumentExpression)
		{
			return ((MemberExpression)argumentExpression.Body).Member.Name;
		}
	}
}