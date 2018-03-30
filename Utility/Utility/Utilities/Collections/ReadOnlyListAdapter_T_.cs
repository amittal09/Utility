using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Vertica.Utilities_v4.Collections
{
	public class ReadOnlyListAdapter<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable
	{
		private readonly IList<T> _adptee;

		public int Count
		{
			get
			{
				return this._adptee.Count;
			}
		}

		public T this[int index]
		{
			get
			{
                return this._adptee[index];
            }
			set
			{
                this._adptee[index] = value;
            }
		}

		public ReadOnlyListAdapter(IList<T> adaptee)
		{
			this._adptee = adaptee;
		}

		public bool Contains(T item)
		{
			return this._adptee.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this._adptee.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._adptee.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return this._adptee.IndexOf(item);
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}