using System;

namespace Vertica.Utilities_v4.Eventing
{
	public interface ICancelEventArgs
	{
		bool IsCancelled
		{
			get;
		}

		void Cancel();
	}
}