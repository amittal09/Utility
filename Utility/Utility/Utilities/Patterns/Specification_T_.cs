using System;

namespace Vertica.Utilities_v4.Patterns
{
	public abstract class Specification<T> : ISpecification<T>
	{
		protected Specification()
		{
		}

		public virtual ISpecification<T> And(ISpecification<T> other)
		{
			return new Specification<T>.AndSpecification(this, other);
		}

		public abstract bool IsSatisfiedBy(T item);

		public virtual ISpecification<T> Not()
		{
			return new Specification<T>.NotSpecification(this);
		}

		public virtual ISpecification<T> Or(ISpecification<T> other)
		{
			return new Specification<T>.OrSpecification(this, other);
		}

		private class AndSpecification : Specification<T>
		{
			private readonly ISpecification<T> _leftSide;

			private readonly ISpecification<T> _rightSide;

			public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
			{
				this._leftSide = leftSide;
				this._rightSide = rightSide;
			}

			public override bool IsSatisfiedBy(T item)
			{
				if (!this._leftSide.IsSatisfiedBy(item))
				{
					return false;
				}
				return this._rightSide.IsSatisfiedBy(item);
			}
		}

		private class NotSpecification : Specification<T>
		{
			private readonly ISpecification<T> _specification;

			public NotSpecification(ISpecification<T> specification)
			{
				this._specification = specification;
			}

			public override bool IsSatisfiedBy(T item)
			{
				return !this._specification.IsSatisfiedBy(item);
			}
		}

		private class OrSpecification : Specification<T>
		{
			private readonly ISpecification<T> _leftSide;

			private readonly ISpecification<T> _rightSide;

			public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
			{
				this._leftSide = leftSide;
				this._rightSide = rightSide;
			}

			public override bool IsSatisfiedBy(T item)
			{
				if (this._leftSide.IsSatisfiedBy(item))
				{
					return true;
				}
				return this._rightSide.IsSatisfiedBy(item);
			}
		}
	}
}