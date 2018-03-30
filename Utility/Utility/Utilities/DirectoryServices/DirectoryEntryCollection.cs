using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.DirectoryServices
{
	public class DirectoryEntryCollection : IDisposable, IEnumerable<DirectoryEntry>, IEnumerable
	{
		private readonly List<DirectoryEntry> _inner;

		public int Count
		{
			get
			{
				return this._inner.Count;
			}
		}

		public DirectoryEntryCollection(int capacity)
		{
			this._inner = new List<DirectoryEntry>(capacity);
		}

		public void Add(DirectoryEntry entry)
		{
			this._inner.Add(entry);
		}

		public void Close()
		{
			this._inner.ForEach((DirectoryEntry e) => e.Close());
		}

		public void Dispose()
		{
			this._inner.ForEach((DirectoryEntry e) => e.Dispose());
		}

		public IEnumerator<DirectoryEntry> GetEnumerator()
		{
			return this._inner.GetEnumerator();
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}