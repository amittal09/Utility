using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Collections
{
	public class TreeNode<TModel> : IEnumerable<TreeNode<TModel>>, IEnumerable
	{
		private readonly IEnumerator<TreeNode<TModel>> _children;

		private readonly Func<int, TreeNode<TModel>> _childNodeAt;

		private readonly Func<TreeNode<TModel>> _parent;

		public TreeNode<TModel> this[int index]
		{
			get
			{
				return this._childNodeAt(index);
			}
		}

		public TModel Model
		{
			get;
			private set;
		}

		public TreeNode<TModel> Parent
		{
			get
			{
				return this._parent();
			}
		}

		internal TreeNode(TModel model, IEnumerator<TreeNode<TModel>> children, Func<int, TreeNode<TModel>> childNodeAt, Func<TreeNode<TModel>> parent)
		{
			this.Model = model;
			this._children = children;
			this._childNodeAt = childNodeAt;
			this._parent = parent;
		}

		public TModel[] Breadcrumb()
		{
			Stack<TModel> tModels = new Stack<TModel>();
			for (TreeNode<TModel> i = this; i != null; i = i.Parent)
			{
				tModels.Push(i.Model);
			}
			return tModels.ToArray();
		}

		public IEnumerator<TreeNode<TModel>> GetEnumerator()
		{
			return this._children;
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}