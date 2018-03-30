using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Collections
{
	public class Tree<TItem, TModel, TKey> : IEnumerable<TreeNode<TModel>>, IEnumerable
	{
		private readonly Dictionary<TKey, Tuple<TKey, Tree<TItem, TModel, TKey>.Parent.Key, TModel, List<TKey>>> _tree;

		private readonly List<TKey> _root;

		private readonly HashSet<TKey> _orphans;

		public TreeNode<TModel> this[int index]
		{
			get
			{
				return this.Get(this._root[index]);
			}
		}

		protected internal Tree(IEnumerable<TItem> items, Func<TItem, TKey> key, Func<TItem, Tree<TItem, TModel, TKey>.Parent, Tree<TItem, TModel, TKey>.Parent.Key> parentKey, Func<TItem, TModel> projection, IEqualityComparer<TKey> comparer = null)
		{
			Tuple<TKey, Tree<TItem, TModel, TKey>.Parent.Key, TModel, List<TKey>> tuple;
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (parentKey == null)
			{
				throw new ArgumentNullException("parentKey");
			}
			if (projection == null)
			{
				throw new ArgumentNullException("projection");
			}
			this._tree = new Dictionary<TKey, Tuple<TKey, Tree<TItem, TModel, TKey>.Parent.Key, TModel, List<TKey>>>(Tree<TItem, TModel, TKey>.TreeCapacityOr(items as ICollection<TModel>, 0), comparer);
			this._root = new List<TKey>();
			this._orphans = new HashSet<TKey>(comparer);
			Tree<TItem, TModel, TKey>.Parent parent = new Tree<TItem, TModel, TKey>.Parent();
			foreach (TItem item in items)
			{
				TKey tKey = key(item);
				Tree<TItem, TModel, TKey>.Parent.Key key1 = parentKey(item, parent);
				this._tree[tKey] = Tuple.Create<TKey, Tree<TItem, TModel, TKey>.Parent.Key, TModel, List<TKey>>(tKey, key1, projection(item), new List<TKey>());
			}
			foreach (Tuple<TKey, Tree<TItem, TModel, TKey>.Parent.Key, TModel, List<TKey>> value in this._tree.Values)
			{
				if (value.Item2 == null)
				{
					this._root.Add(value.Item1);
				}
				else if (!this._tree.TryGetValue(value.Item2.Value, out tuple))
				{
					this._orphans.Add(value.Item1);
				}
				else
				{
					tuple.Item4.Add(value.Item1);
				}
			}
		}

		public TreeNode<TModel> Get(TKey key)
		{
			TreeNode<TModel> tModels;
			if (!this.TryGet(key, out tModels))
			{
				throw new KeyNotFoundException(string.Format("Node with key {0} was not found.", key));
			}
			return tModels;
		}

		public IEnumerator<TreeNode<TModel>> GetEnumerator()
		{
			return this.GetEnumerator(this._root);
		}

		private IEnumerator<TreeNode<TModel>> GetEnumerator(List<TKey> nodes)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			return nodes.Select<TKey, TreeNode<TModel>>((TKey x) => {
				TreeNode<TModel> tModels;
				this.TryGet(x, out tModels);
				return tModels;
			}).Where<TreeNode<TModel>>((TreeNode<TModel> x) => x != null).GetEnumerator();
		}

		public IEnumerable<TModel> Orphans()
		{
			return 
				from  x in this._orphans
				select this._tree[x].Item3;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private static int TreeCapacityOr(ICollection<TModel> collection, int defaultCapacity)
		{
			if (collection == null)
			{
				return defaultCapacity;
			}
			return collection.Count;
		}

		public bool TryGet(TKey key, out TreeNode<TModel> node)
		{
			Tuple<TKey, Tree<TItem, TModel, TKey>.Parent.Key, TModel, List<TKey>> tuple;
			node = null;
			if (!this._orphans.Contains(key) && this._tree.TryGetValue(key, out tuple))
			{
				node = new TreeNode<TModel>(this._tree[key].Item3, this.GetEnumerator(tuple.Item4), (int index) => this.Get(tuple.Item4[index]), () => {
					if (tuple.Item2 == null)
					{
						return null;
					}
					return this.Get(tuple.Item2.Value);
				});
			}
			return node != null;
		}

		public class Parent
		{
			public Tree<TItem, TModel, TKey>.Parent.Key None
			{
				get
				{
					return null;
				}
			}

			public Parent()
			{
			}

			public Tree<TItem, TModel, TKey>.Parent.Key Value(TKey key)
			{
				return new Tree<TItem, TModel, TKey>.Parent.Key(key);
			}

			public class Key
			{
				internal TKey Value
				{
					get;
					private set;
				}

				internal Key(TKey key)
				{
					this.Value = key;
				}
			}
		}
	}
}