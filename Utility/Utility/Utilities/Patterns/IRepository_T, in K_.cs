using System;
using System.Linq;
using System.Linq.Expressions;
using Vertica.Utilities_v4.Collections;

namespace Vertica.Utilities_v4.Patterns
{
	public interface IRepository<T, in K>
	where T : IIdentifiable<K>
	{
		void Add(T entity);

		int Count();

		int Count(Expression<Func<T, bool>> expression);

		IQueryable<T> Find();

		IQueryable<T> Find(K id);

		IQueryable<T> Find(Expression<Func<T, bool>> expression);

		IReadOnlyList<T> FindAll();

		IReadOnlyList<T> FindAll(Expression<Func<T, bool>> expression);

		T FindOne(K id);

		T FindOne(Expression<Func<T, bool>> expression);

		void Remove(T entity);

		void Save(T entity);

		bool TryFindOne(Expression<Func<T, bool>> expression, out T entity);
	}
}