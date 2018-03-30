using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Vertica.Utilities_v4.Eventing;

namespace Vertica.Utilities_v4.Collections
{
	public class NotifyingList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		private readonly List<T> _list;

		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public T this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				T item = this._list[index];
				if (this.OnSetting(this._list[index], index, value))
				{
					this._list[index] = value;
					this.OnSet(value, index, item);
				}
			}
		}

		public NotifyingList()
		{
		}

		public void Add(T item)
		{
			this.Insert(this.Count, item);
		}

		public void Clear()
		{
			if (this.OnClearing())
			{
				this._list.Clear();
				this.OnCleared();
			}
		}

		public bool Contains(T item)
		{
			return this._list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this._list.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return this._list.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			if (this.OnInserting(item, index))
			{
				this._list.Insert(index, item);
				this.OnInserted(item, index);
			}
		}

		protected void OnCleared()
		{
			if (this.Cleared != null)
			{
				this.Cleared(this, EventArgs.Empty);
			}
		}

		protected bool OnClearing()
		{
			if (this.Clearing == null)
			{
				return true;
			}
			CancelEventArgs cancelEventArg = new CancelEventArgs();
			this.Clearing(this, cancelEventArg);
			return !cancelEventArg.IsCancelled;
		}

		protected void OnInserted(T item, int index)
		{
			if (this.Inserted != null)
			{
				ValueIndexEventArgs<T> valueIndexEventArg = new ValueIndexEventArgs<T>(index, item);
				this.Inserted(this, valueIndexEventArg);
			}
		}

		protected bool OnInserting(T item, int index)
		{
			if (this.Inserting == null)
			{
				return true;
			}
			ValueIndexCancelEventArgs<T> valueIndexCancelEventArg = new ValueIndexCancelEventArgs<T>(index, item);
			this.Inserting(this, valueIndexCancelEventArg);
			return !valueIndexCancelEventArg.IsCancelled;
		}

		protected void OnRemoved(T item, int index)
		{
			if (this.Removed != null)
			{
				ValueIndexEventArgs<T> valueIndexEventArg = new ValueIndexEventArgs<T>(index, item);
				this.Removed(this, valueIndexEventArg);
			}
		}

		protected bool OnRemoving(T item, int index)
		{
			if (this.Removing == null)
			{
				return true;
			}
			ValueIndexCancelEventArgs<T> valueIndexCancelEventArg = new ValueIndexCancelEventArgs<T>(index, item);
			this.Removing(this, valueIndexCancelEventArg);
			return !valueIndexCancelEventArg.IsCancelled;
		}

		protected void OnSet(T item, int index, T oldValue)
		{
			if (this.Set != null)
			{
				ValueIndexChangedEventArgs<T> valueIndexChangedEventArg = new ValueIndexChangedEventArgs<T>(index, oldValue, item);
				this.Set(this, valueIndexChangedEventArg);
			}
		}

		protected bool OnSetting(T item, int index, T newValue)
		{
			if (this.Setting == null)
			{
				return true;
			}
			ValueIndexChangingEventArgs<T> valueIndexChangingEventArg = new ValueIndexChangingEventArgs<T>(index, item, newValue);
			this.Setting(this, valueIndexChangingEventArg);
			return !valueIndexChangingEventArg.IsCancelled;
		}

		public bool Remove(T item)
		{
			bool flag = false;
			int num = this._list.IndexOf(item);
			if (num != -1 && this.OnRemoving(item, num))
			{
				this._list.RemoveAt(num);
				flag = true;
				this.OnRemoved(item, num);
			}
			return flag;
		}

		public void RemoveAt(int index)
		{
			T item = this._list[index];
			if (this.OnRemoving(item, index))
			{
				this._list.RemoveAt(index);
				this.OnRemoved(item, index);
			}
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		public event EventHandler<EventArgs, NotifyingList<T>> Cleared;

		public event EventHandler<CancelEventArgs, NotifyingList<T>> Clearing;

		public event EventHandler<ValueIndexEventArgs<T>, NotifyingList<T>> Inserted;

		public event EventHandler<ValueIndexCancelEventArgs<T>, NotifyingList<T>> Inserting;

		public event EventHandler<ValueIndexEventArgs<T>, NotifyingList<T>> Removed;

		public event EventHandler<ValueIndexCancelEventArgs<T>, NotifyingList<T>> Removing;

		public event EventHandler<ValueIndexChangedEventArgs<T>, NotifyingList<T>> Set;

		public event EventHandler<ValueIndexChangingEventArgs<T>, NotifyingList<T>> Setting;
	}
}