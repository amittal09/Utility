using System;

namespace Vertica.Utilities_v4.Patterns
{
	public class ResponsibleLink<T, TResult> : ChainOfResponsibilityLink<T, TResult>
	{
		private readonly IChainOfResponsibilityLink<T, TResult> _link;

		public ResponsibleLink(IChainOfResponsibilityLink<T, TResult> link)
		{
			this._link = link;
		}

		public override bool CanHandle(T context)
		{
			if (this._link == null)
			{
				return false;
			}
			return this._link.CanHandle(context);
		}

		protected override TResult DoHandle(T context)
		{
			if (this._link == null)
			{
				return default(TResult);
			}
			return this._link.DoHandle(context);
		}
	}
}