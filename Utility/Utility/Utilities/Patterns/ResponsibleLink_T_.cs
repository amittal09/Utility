using System;

namespace Vertica.Utilities_v4.Patterns
{
	public class ResponsibleLink<T> : ChainOfResponsibilityLink<T>
	{
		private readonly IChainOfResponsibilityLink<T> _link;

		public ResponsibleLink(IChainOfResponsibilityLink<T> link)
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

		protected override void DoHandle(T context)
		{
			if (this._link != null)
			{
				this._link.DoHandle(context);
			}
		}
	}
}