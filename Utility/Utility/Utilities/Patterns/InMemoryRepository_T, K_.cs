using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Collections;
using Vertica.Utilities_v4.Extensions.ObjectExt;

namespace Vertica.Utilities_v4.Patterns
{
	public class InMemoryRepository<T, K> : IRepository<T, K>
	where T : IIdentifiable<K>
	{
		private readonly List<T> _inner;

		public InMemoryRepository(IEnumerable<T> initialData)
		{
			this._inner = new List<T>();
			if (initialData != null)
			{
				this._inner.AddRange(initialData);
			}
		}

		public InMemoryRepository(params T[] initialData) : this(initialData.AsEnumerable<T>())
		{
		}

		public void Add(T entity)
		{
			this._inner.Add(entity);
		}

		public int Count()
		{
			return this._inner.Count;
		}

		public int Count(Expression<Func<T, bool>> expression)
		{
			return this._inner.Count<T>(expression.Compile());
		}

		public IQueryable<T> Find()
		{
			return this._inner.AsQueryable<T>();
		}

		public IQueryable<T> Find(K id)
		{
			return this._inner.FindAll((T e) => e.Id.Equals(id)).AsQueryable<T>();
		}

		public IQueryable<T> Find(Expression<Func<T, bool>> expression)
		{
			return this._inner.Where<T>(expression.Compile()).AsQueryable<T>();
		}

		public Vertica.Utilities_v4.Collections.IReadOnlyList<T> FindAll()
		{
			return new ReadOnlyListAdapter<T>(this._inner);
		}

		public Vertica.Utilities_v4.Collections.IReadOnlyList<T> FindAll(Expression<Func<T, bool>> expression)
		{
			return new ReadOnlyListAdapter<T>(this._inner.Where<T>(expression.Compile()).ToList<T>());
		}

		public T FindOne(K id)
		{
			return this._inner.Single<T>((T e) => e.Id.Equals(id));
		}

		public T FindOne(Expression<Func<T, bool>> expression)
		{
			return this._inner.Single<T>(expression.Compile());
		}

		public void Remove(T entity)
		{
			this._inner.RemoveAll((T e) => e.Id.Equals(entity.Id));
		}

		public void Save(T entity)
		{
		}

		public bool TryFindOne(Expression<Func<T, bool>> expression, out T entity)
		{
			try
			{
				entity = this._inner.SingleOrDefault<T>(expression.Compile());
			}
			catch (InvalidOperationException invalidOperationException)
			{
				entity = default(T);
			}
			return entity.IsNotDefault<T>();
		}
	}
}