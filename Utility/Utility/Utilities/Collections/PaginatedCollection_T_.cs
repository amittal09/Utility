using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Collections
{
	[Serializable]
	public class PaginatedCollection<T> : IPaginatedCollection<T>, IEnumerable<T>, IEnumerable
	{
		private readonly IEnumerable<T> _collection;

		private readonly Pagination _pagination;

		private uint? _numberOfPages;

		private IEnumerable<T> _paginatedCollection;

		public IEnumerable<T> Collection
		{
			get
			{
				this._paginatedCollection = this._paginatedCollection ?? this.initPagedResult();
				return this._paginatedCollection;
			}
		}

		public uint CurrentPage
		{
			get
			{
				return this._pagination.PageNumber;
			}
		}

		public uint NumberOfPages
		{
			get
			{
				uint? nullable = this._numberOfPages;
				this._numberOfPages = new uint?((nullable.HasValue ? nullable.GetValueOrDefault() : this.initNumberOfPages()));
				return this._numberOfPages.Value;
			}
		}

		public uint Pagesize
		{
			get
			{
				return this._pagination.PageSize;
			}
		}

		public PaginatedCollection(Pagination pagination, IEnumerable<T> collection)
		{
			this._pagination = pagination;
			this._collection = collection;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.Collection.GetEnumerator();
		}

		private uint initNumberOfPages()
		{
			uint num;
			if (this._collection != null)
			{
				Pagination pagination = this._pagination;
				num = pagination.PageCount(Convert.ToUInt32(this._collection.Count<T>()));
			}
			else
			{
				num = 0;
			}
			return num;
		}

		private IEnumerable<T> initPagedResult()
		{
			IEnumerable<T> ts = null;
			if (this._collection != null)
			{
				IEnumerable<T> ts1 = 
					from  t in this._collection
					select t;
				uint pageSize = this._pagination.PageSize;
				Pagination pagination = this._pagination;
				ts = ts1.Skip<T>((int)(pageSize * (pagination.PageNumber - 1))).Take<T>((int)this._pagination.PageSize);
			}
			return ts;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}