using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Vertica.Utilities_v4.Collections
{
	public interface IReadOnlyList<T> : IEnumerable<T>, IEnumerable
	{
		int Count
		{
			get;
		}

		T this[int index]
		{
			get;
		}

		bool Contains(T item);

		void CopyTo(T[] array, int arrayIndex);

		int IndexOf(T item);
	}
}