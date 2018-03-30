using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Patterns
{
	public abstract class ChainOfResponsibilityLink<T, TResult>
	{
		private ChainOfResponsibilityLink<T, TResult> _lastLink;

		public ChainOfResponsibilityLink<T, TResult> Next
		{
			get;
			private set;
		}

		protected ChainOfResponsibilityLink()
		{
		}

		public abstract bool CanHandle(T context);

		public ChainOfResponsibilityLink<T, TResult> Chain(ChainOfResponsibilityLink<T, TResult> lastHandler)
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

		public ChainOfResponsibilityLink<T, TResult> Chain(IChainOfResponsibilityLink<T, TResult> lastHandler)
		{
			return this.Chain(new ResponsibleLink<T, TResult>(lastHandler));
		}

		public ChainOfResponsibilityLink<T, TResult> Chain(params ChainOfResponsibilityLink<T, TResult>[] handlers)
		{
			return this.Chain((IEnumerable<ChainOfResponsibilityLink<T, TResult>>)handlers);
		}

		public ChainOfResponsibilityLink<T, TResult> Chain(params IChainOfResponsibilityLink<T, TResult>[] handlers)
		{
			return this.Chain((IEnumerable<IChainOfResponsibilityLink<T, TResult>>)handlers);
		}

		public ChainOfResponsibilityLink<T, TResult> Chain(IEnumerable<IChainOfResponsibilityLink<T, TResult>> handlers)
		{
			return this.Chain(
				from  h in handlers
				select new ResponsibleLink<T, TResult>(h));
		}

		public ChainOfResponsibilityLink<T, TResult> Chain(IEnumerable<ChainOfResponsibilityLink<T, TResult>> handlers)
		{
			ChainOfResponsibilityLink<T, TResult> chainOfResponsibilityLink = null;
			foreach (ChainOfResponsibilityLink<T, TResult> handler in handlers)
			{
				chainOfResponsibilityLink = this.Chain(handler);
			}
			return chainOfResponsibilityLink;
		}

		protected abstract TResult DoHandle(T context);

		public TResult Handle(T context)
		{
			TResult tResult = default(TResult);
			if (this.CanHandle(context))
			{
				tResult = this.DoHandle(context);
			}
			else if (this.Next != null)
			{
				tResult = this.Next.Handle(context);
			}
			return tResult;
		}

		public bool TryHandle(T context, out TResult result)
		{
			result = default(TResult);
			bool flag = false;
			if (this.CanHandle(context))
			{
				result = this.DoHandle(context);
				flag = true;
			}
			else if (this.Next != null)
			{
				flag = this.Next.TryHandle(context, out result);
			}
			return flag;
		}
	}
}