using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Collections
{
	public class Tree<TModel, TKey> : Tree<TModel, TModel, TKey>
	{
		protected internal Tree(IEnumerable<TModel> items, Func<TModel, TKey> key, Func<TModel, Tree<TModel, TModel, TKey>.Parent, Tree<TModel, TModel, TKey>.Parent.Key> parentKey, IEqualityComparer<TKey> comparer = null) : base(items, key, parentKey, (TModel x) => x, comparer)
		{
		}
	}
}