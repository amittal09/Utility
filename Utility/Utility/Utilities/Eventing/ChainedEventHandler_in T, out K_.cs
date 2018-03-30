using System;

namespace Vertica.Utilities_v4.Eventing
{
	public delegate K ChainedEventHandler<in T, out K>(object sender, T e)
	where T : IMutableValueEventArgs<K>;
}