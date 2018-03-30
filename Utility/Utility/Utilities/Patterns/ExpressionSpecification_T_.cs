using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Patterns
{
	public class ExpressionSpecification<T> : Specification<T>
	{
		private readonly Expression<Func<T, bool>> _predicateExpression;

		private Func<T, bool> _predicate;

		public Expression<Func<T, bool>> Expression
		{
			get
			{
				return this._predicateExpression;
			}
		}

		public Func<T, bool> Function
		{
			get
			{
				Func<T, bool> func = this._predicate ?? this._predicateExpression.Compile();
				Func<T, bool> func1 = func;
				this._predicate = func;
				return func1;
			}
		}

		public ExpressionSpecification(Expression<Func<T, bool>> expression)
		{
			this._predicateExpression = expression;
		}

		public ExpressionSpecification<T> And(ExpressionSpecification<T> right)
		{
			object obj = this;
			if (((ExpressionSpecification<T>)obj ? true : false))
			{
				obj = (ExpressionSpecification<T>)obj & right;
			}
			return (ExpressionSpecification<T>)obj;
		}

		public override bool IsSatisfiedBy(T entity)
		{
			return this.Function(entity);
		}

		private static BinaryExpression mergeIntoBinary(Expression<Func<T, bool>> right, Expression<Func<T, bool>> left, ExpressionType type)
		{
			InvocationExpression invocationExpression = System.Linq.Expressions.Expression.Invoke(right, left.Parameters);
			return System.Linq.Expressions.Expression.MakeBinary(type, left.Body, invocationExpression);
		}

		public new ExpressionSpecification<T> Not()
		{
			return !this;
		}

		public static ExpressionSpecification<T> operator &(ExpressionSpecification<T> leftSide, ExpressionSpecification<T> rightSide)
		{
			BinaryExpression binaryExpression = ExpressionSpecification<T>.mergeIntoBinary(rightSide.Expression, leftSide.Expression, ExpressionType.AndAlso);
			return new ExpressionSpecification<T>(ExpressionSpecification<T>.toLambda(binaryExpression, leftSide.Expression.Parameters));
		}

		public static ExpressionSpecification<T> operator |(ExpressionSpecification<T> leftSide, ExpressionSpecification<T> rightSide)
		{
			BinaryExpression binaryExpression = ExpressionSpecification<T>.mergeIntoBinary(rightSide.Expression, leftSide.Expression, ExpressionType.OrElse);
			return new ExpressionSpecification<T>(ExpressionSpecification<T>.toLambda(binaryExpression, leftSide.Expression.Parameters));
		}

		public static bool operator false(ExpressionSpecification<T> specification)
		{
			return false;
		}

		public static implicit operator Func<T, Boolean>(ExpressionSpecification<T> specification)
		{
			return specification.Function;
		}

		public static implicit operator Expression<Func<T, Boolean>>(ExpressionSpecification<T> specification)
		{
			return specification.Expression;
		}

		public static implicit operator Predicate<T>(ExpressionSpecification<T> spec)
		{
			return (T t) => spec.Function(t);
		}

		public static ExpressionSpecification<T> operator !(ExpressionSpecification<T> spec)
		{
			UnaryExpression unaryExpression = System.Linq.Expressions.Expression.MakeUnary(ExpressionType.Not, spec.Expression.Body, typeof(bool));
			return new ExpressionSpecification<T>(ExpressionSpecification<T>.toLambda(unaryExpression, spec.Expression.Parameters));
		}

		public static bool operator true(ExpressionSpecification<T> specification)
		{
			return false;
		}

		public ExpressionSpecification<T> Or(ExpressionSpecification<T> right)
		{
			object obj = this;
			if (!(ExpressionSpecification<T>)obj)
			{
				obj = (ExpressionSpecification<T>)obj | right;
			}
			return (ExpressionSpecification<T>)obj;
		}

		private static Expression<Func<T, bool>> toLambda(System.Linq.Expressions.Expression expression, IEnumerable<ParameterExpression> parameters)
		{
			return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(expression, parameters);
		}
	}
}