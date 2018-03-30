using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Vertica.Utilities_v4
{
	public class DomainOfValues<T> : IEnumerable<T>, IEnumerable
	{
		private readonly IEnumerable<T> _expectedValues;

		public DomainOfValues(params T[] expectedValues) : this(expectedValues.AsEnumerable<T>())
		{
		}

		public DomainOfValues(IEnumerable<T> expectedValues)
		{
			this._expectedValues = expectedValues ?? Enumerable.Empty<T>();
		}

		public void AssertContains(T actualValue)
		{
			if (!this.CheckContains(actualValue))
			{
				throw new InvalidDomainException<T>(actualValue, this._expectedValues);
			}
		}

		public void AssertContains(T actualValue, IEqualityComparer<T> comparer)
		{
			if (!this.CheckContains(actualValue, comparer))
			{
				throw new InvalidDomainException<T>(actualValue, this._expectedValues);
			}
		}

		public bool CheckContains(T actualValue)
		{
			if (this._expectedValues == null)
			{
				return false;
			}
			return this._expectedValues.Contains<T>(actualValue);
		}

		public bool CheckContains(T actualValue, IEqualityComparer<T> comparer)
		{
			if (this._expectedValues == null)
			{
				return false;
			}
			return this._expectedValues.Contains<T>(actualValue, comparer);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._expectedValues.GetEnumerator();
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}