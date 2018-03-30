using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Comparisons
{
	public class OperatorComparer<T> : ChainableComparer<T>
	{
		public OperatorComparer(Direction direction = 0) : base(direction)
		{
		}

		protected override int DoCompare(T x, T y)
		{
			return this.throwingMeaningfulException(() => {
				if (OperatorComparer<T>.Operations.gt(x, y))
				{
					return 1;
				}
				if (OperatorComparer<T>.Operations.lt(x, y))
				{
					return -1;
				}
				return 0;
			});
		}

		private int throwingMeaningfulException(Func<int> comparison)
		{
			int num;
			try
			{
				num = comparison();
			}
			catch (TypeInitializationException typeInitializationException1)
			{
				TypeInitializationException typeInitializationException = typeInitializationException1;
				if (typeInitializationException.InnerException != null)
				{
					throw typeInitializationException.InnerException;
				}
				throw;
			}
			return num;
		}

		private static class Operations
		{
			internal readonly static Func<T, T, bool> gt;

			internal readonly static Func<T, T, bool> lt;

			static Operations()
			{
				Type type = typeof(T);
				ParameterExpression parameterExpression = Expression.Parameter(type, "x");
				ParameterExpression parameterExpression1 = Expression.Parameter(type, "y");
				BinaryExpression binaryExpression = Expression.GreaterThan(parameterExpression, parameterExpression1);
				ParameterExpression[] parameterExpressionArray = new ParameterExpression[] { parameterExpression, parameterExpression1 };
				OperatorComparer<T>.Operations.gt = Expression.Lambda<Func<T, T, bool>>(binaryExpression, parameterExpressionArray).Compile();
				BinaryExpression binaryExpression1 = Expression.LessThan(parameterExpression, parameterExpression1);
				ParameterExpression[] parameterExpressionArray1 = new ParameterExpression[] { parameterExpression, parameterExpression1 };
				OperatorComparer<T>.Operations.lt = Expression.Lambda<Func<T, T, bool>>(binaryExpression1, parameterExpressionArray1).Compile();
			}
		}
	}
}