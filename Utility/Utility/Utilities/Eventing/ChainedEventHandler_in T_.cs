using System;

namespace Vertica.Utilities_v4.Eventing
{
	public delegate void ChainedEventHandler<in T>(object sender, T e)
	where T : IChainedEventArgs;
}