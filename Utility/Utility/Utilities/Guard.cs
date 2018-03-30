using System;
using System.Linq.Expressions;
using System.Reflection;
using Vertica.Utilities_v4.Extensions.StringExt;

namespace Vertica.Utilities_v4
{
	public static class Guard
	{
		public static void Against(bool assertion)
		{
			Guard.Against<InvalidOperationException>(assertion);
		}

		public static void Against(bool assertion, string message, params string[] formattingArguments)
		{
			Guard.Against<InvalidOperationException>(assertion, message, formattingArguments);
		}

		public static void Against<TException>(bool assertion, string message, params string[] formattingArguments)
		where TException : Exception
		{
			if (!assertion)
			{
				return;
			}
			ExceptionHelper.Throw<TException>(message, formattingArguments);
		}

		public static void Against<TException>(bool assertion)
		where TException : Exception, new()
		{
			if (assertion)
			{

                throw (Exception)Activator.CreateInstance<TException>();
			}
		}

		public static void AgainstArgument(string paramName, bool assertion)
		{
			if (!assertion)
			{
				return;
			}
			ExceptionHelper.ThrowArgumentException(paramName);
		}

		public static void AgainstArgument(string paramName, bool assertion, string message, params string[] formattingArguments)
		{
			if (!assertion)
			{
				return;
			}
			ExceptionHelper.ThrowArgumentException(paramName, message, formattingArguments);
		}

		public static void AgainstArgument<TArgumentException>(string paramName, bool assertion)
		where TArgumentException : ArgumentException
		{
			if (!assertion)
			{
				return;
			}
			ExceptionHelper.ThrowArgumentException<TArgumentException>(paramName);
		}

		public static void AgainstArgument<TArgumentException>(string paramName, bool assertion, string message, params string[] formattingArguments)
		where TArgumentException : ArgumentException
		{
			if (!assertion)
			{
				return;
			}
			ExceptionHelper.ThrowArgumentException<TArgumentException>(paramName, message, formattingArguments);
		}

		public static void AgainstNullArgument(string paramName, object param)
		{
			Guard.AgainstArgument<ArgumentNullException>(paramName, param == null);
		}

		public static void AgainstNullArgument<T>(T container)
		where T : class
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			Guard.NullChecker<T>.Check(container);
		}

		private static class NullChecker<T>
		where T : class
		{
			private readonly static Func<T, string> _nullSeeker;

			static NullChecker()
			{
				Expression expression = Expression.Constant(null, typeof(string));
				ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "obj");
				PropertyInfo[] properties = typeof(T).GetProperties();
				for (int i = 0; i < (int)properties.Length; i++)
				{
					PropertyInfo propertyInfo = properties[i];
					Type propertyType = propertyInfo.PropertyType;
					if (!propertyType.IsValueType || !(Nullable.GetUnderlyingType(propertyType) == null))
					{
						expression = Expression.Condition(Expression.Equal(Expression.Property(parameterExpression, propertyInfo), Expression.Constant(null, propertyType)), Expression.Constant(propertyInfo.Name, typeof(string)), expression);
					}
				}
				ParameterExpression[] parameterExpressionArray = new ParameterExpression[] { parameterExpression };
				Guard.NullChecker<T>._nullSeeker = Expression.Lambda<Func<T, string>>(expression, parameterExpressionArray).Compile();
			}

			internal static void Check(T item)
			{
				string str = Guard.NullChecker<T>._nullSeeker(item);
				if (str.IsNotEmpty())
				{
					throw new ArgumentNullException(str);
				}
			}
		}
	}
}