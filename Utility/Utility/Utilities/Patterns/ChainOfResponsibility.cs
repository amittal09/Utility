using System;

namespace Vertica.Utilities_v4.Patterns
{
	public static class ChainOfResponsibility
	{
		public static ChainOfResponsibilityLink<T> Empty<T>()
		{
			return new ChainOfResponsibility.EmptyLink<T>();
		}

		public static ChainOfResponsibilityLink<T, TResult> Empty<T, TResult>()
		{
			return new ChainOfResponsibility.EmptyLink<T, TResult>();
		}

		private class EmptyLink<T> : ChainOfResponsibilityLink<T>
		{
			public EmptyLink()
			{
			}

			public override bool CanHandle(T context)
			{
				return false;
			}

			protected override void DoHandle(T context)
			{
			}
		}

		private class EmptyLink<T, TResult> : ChainOfResponsibilityLink<T, TResult>
		{
			public EmptyLink()
			{
			}

			public override bool CanHandle(T context)
			{
				return false;
			}

			protected override TResult DoHandle(T context)
			{
				return default(TResult);
			}
		}
	}
}