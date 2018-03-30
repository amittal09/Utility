using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Collections;
using Vertica.Utilities_v4.Comparisons;

namespace Vertica.Utilities_v4.Extensions.QueryableExt
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> Paginate<T>(this IQueryable<T> nonPaginated, Pagination page)
		{
			Guard.AgainstNullArgument("nonPaginated", nonPaginated);
			return nonPaginated.Skip<T>((int)(page.FirstRecord - 1)).Take<T>((int)page.PageSize);
		}

		public static IOrderedQueryable<TSource> SortBy<TSource>(this IQueryable<TSource> unordered)
		{
			Guard.AgainstNullArgument("unordered", unordered);
			return unordered.SortBy<TSource, TSource>((TSource e) => e, new Direction?(Direction.Ascending));
		}

		public static IOrderedQueryable<TSource> SortBy<TSource>(this IQueryable<TSource> unordered, Direction? direction)
		{
			Guard.AgainstNullArgument("unordered", unordered);
			return unordered.SortBy<TSource, TSource>((TSource e) => e, direction);
		}

		public static IOrderedQueryable<TSource> SortBy<TSource, TKey>(this IQueryable<TSource> unordered, Expression<Func<TSource, TKey>> selector, Direction? direction)
		{
			Guard.AgainstNullArgument("unordered", unordered);
			if (!direction.HasValue)
			{
				return (IOrderedQueryable<TSource>)unordered;
			}
			if (!direction.Equals(Direction.Ascending))
			{
				return unordered.OrderByDescending<TSource, TKey>(selector);
			}
			return unordered.OrderBy<TSource, TKey>(selector);
		}
	}
}