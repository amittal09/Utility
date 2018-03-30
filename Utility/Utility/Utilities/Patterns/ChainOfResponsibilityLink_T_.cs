using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Patterns
{
	public abstract class ChainOfResponsibilityLink<T>
	{
		private ChainOfResponsibilityLink<T> _lastLink;

		public ChainOfResponsibilityLink<T> Next
		{
			get;
			private set;
		}

		protected ChainOfResponsibilityLink()
		{
		}

		public abstract bool CanHandle(T context);

		public ChainOfResponsibilityLink<T> Chain(IChainOfResponsibilityLink<T> lastHandler)
		{
			return this.Chain(new ResponsibleLink<T>(lastHandler));
		}

		public ChainOfResponsibilityLink<T> Chain(ChainOfResponsibilityLink<T> lastHandler)
		{
			if (this.Next != null)
			{
				this._lastLink.Chain(lastHandler);
			}
			else
			{
				this.Next = lastHandler;
			}
			this._lastLink = lastHandler;
			return this;
		}

		public ChainOfResponsibilityLink<T> Chain(params IChainOfResponsibilityLink<T>[] handlers)
		{
			return this.Chain(handlers.AsEnumerable<IChainOfResponsibilityLink<T>>());
		}

		public ChainOfResponsibilityLink<T> Chain(params ChainOfResponsibilityLink<T>[] handlers)
		{
			return this.Chain(handlers.AsEnumerable<ChainOfResponsibilityLink<T>>());
		}

		public ChainOfResponsibilityLink<T> Chain(IEnumerable<IChainOfResponsibilityLink<T>> handlers)
		{
			return this.Chain(
				from  h in handlers
				select new ResponsibleLink<T>(h));
		}

		public ChainOfResponsibilityLink<T> Chain(IEnumerable<ChainOfResponsibilityLink<T>> handlers)
		{
			ChainOfResponsibilityLink<T> chainOfResponsibilityLink = null;
			foreach (ChainOfResponsibilityLink<T> handler in handlers)
			{
				chainOfResponsibilityLink = this.Chain(handler);
			}
			return chainOfResponsibilityLink;
		}

		protected abstract void DoHandle(T context);

		public void Handle(T context)
		{
			if (this.CanHandle(context))
			{
				this.DoHandle(context);
				return;
			}
			if (this.Next != null)
			{
				this.Next.Handle(context);
			}
		}

		public bool TryHandle(T context)
		{
			bool flag = false;
			if (this.CanHandle(context))
			{
				this.DoHandle(context);
				flag = true;
			}
			else if (this.Next != null)
			{
				flag = this.Next.TryHandle(context);
			}
			return flag;
		}
	}
}