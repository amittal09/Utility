using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class ChainedEventArgs : EventArgs, IChainedEventArgs
	{
		public bool Handled
		{
			get;
			set;
		}

		public ChainedEventArgs()
		{
		}
	}
}