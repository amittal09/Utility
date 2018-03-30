using System;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Extensions.DelegateExt;

namespace Vertica.Utilities_v4.Patterns
{
	public class PredicateSpecification<T> : Specification<T>
	{
		private readonly Predicate<T> _predicate;

		public Func<T, bool> Function
		{
			get
			{
				return this.Predicate.Cast<Func<T, bool>>();
			}
		}

		public Predicate<T> Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		public PredicateSpecification(Predicate<T> predicate)
		{
			this._predicate = predicate;
		}

		public override bool IsSatisfiedBy(T item)
		{
			return this._predicate(item);
		}

		public static PredicateSpecification<T> operator &(PredicateSpecification<T> left, PredicateSpecification<T> right)
		{
			return new PredicateSpecification<T>((T t) => {
				if (!left.IsSatisfiedBy(t))
				{
					return false;
				}
				return right.IsSatisfiedBy(t);
			});
		}

		public static PredicateSpecification<T> operator |(PredicateSpecification<T> left, PredicateSpecification<T> right)
		{
			return new PredicateSpecification<T>((T t) => {
				if (left.IsSatisfiedBy(t))
				{
					return true;
				}
				return right.IsSatisfiedBy(t);
			});
		}

		public static bool operator false(PredicateSpecification<T> specification)
		{
			return false;
		}

		public static implicit operator Predicate<T>(PredicateSpecification<T> spec)
		{
			return spec.Predicate;
		}

		public static implicit operator Func<T, Boolean>(PredicateSpecification<T> spec)
		{
			return spec.Function;
		}

		public static PredicateSpecification<T> operator !(PredicateSpecification<T> specification)
		{
			return new PredicateSpecification<T>((T t) => !specification.IsSatisfiedBy(t));
		}

		public static bool operator true(PredicateSpecification<T> specification)
		{
			return false;
		}
	}
}