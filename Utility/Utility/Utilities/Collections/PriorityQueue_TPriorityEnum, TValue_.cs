using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Collections
{
	public class PriorityQueue<TPriorityEnum, TValue> : IEnumerable<TValue>, IEnumerable
	where TPriorityEnum : struct, IComparable, IFormattable, IConvertible
	{
		private readonly SortedList<TPriorityEnum, Queue<TValue>> _list;

		private int _count;

		public int Count
		{
			get
			{
				return this._count;
			}
		}

		public PriorityQueue()
		{
			this._list = new SortedList<TPriorityEnum, Queue<TValue>>(new PriorityQueue<TPriorityEnum, TValue>.PriorityComparer());
			foreach (TPriorityEnum value in Enumeration.GetValues<TPriorityEnum>())
			{
				this._list.Add(value, new Queue<TValue>());
			}
		}

		public TValue Dequeue()
		{
			Guard.Against(this.Count == 0, "This queue is empty.", new string[0]);
			TValue tValue = default(TValue);
			lock (this._list)
			{
				foreach (Queue<TValue> value in this._list.Values)
				{
					if (value.Count <= 0)
					{
						continue;
					}
					tValue = value.Dequeue();
					Interlocked.Decrement(ref this._count);
					break;
				}
			}
			return tValue;
		}

		public void Enqueue(TPriorityEnum priority, TValue item)
		{
			Enumeration.AssertDefined<TPriorityEnum>(priority);
			lock (this._list)
			{
				this._list[priority].Enqueue(item);
				Interlocked.Increment(ref this._count);
			}
		}

		public IEnumerator<TValue> GetEnumerator()
		{
			return this._list.Values.Aggregate<Queue<TValue>, IEnumerable<TValue>>(Enumerable.Empty<TValue>(), (IEnumerable<TValue> current, Queue<TValue> queue) => current.Concat<TValue>(queue)).GetEnumerator();
		}

		public TValue Peek()
		{
			TValue tValue = default(TValue);
			lock (this._list)
			{
				foreach (Queue<TValue> value in this._list.Values)
				{
					if (value.Count <= 0)
					{
						continue;
					}
					tValue = value.Peek();
					break;
				}
			}
			return tValue;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private class PriorityComparer : IComparer<TPriorityEnum>
		{
			public PriorityComparer()
			{
			}

			public int Compare(TPriorityEnum x, TPriorityEnum y)
			{
				return y.CompareTo(x);
			}
		}
	}
}