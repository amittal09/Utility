using System;

namespace Vertica.Utilities_v4.Eventing
{
	public interface IMutableValueEventArgs<T>
	{
		T Value
		{
			get;
			set;
		}
	}
}