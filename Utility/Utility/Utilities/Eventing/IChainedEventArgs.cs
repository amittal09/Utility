using System;

namespace Vertica.Utilities_v4.Eventing
{
	public interface IChainedEventArgs
	{
		bool Handled
		{
			get;
			set;
		}
	}
}