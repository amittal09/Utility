using System;

namespace Vertica.Utilities_v4
{
	public class DisposableAction : IDisposable
	{
		private readonly Action _onDispose;

		public DisposableAction(Action onDispose)
		{
			this._onDispose = onDispose;
		}

		public void Dispose()
		{
			this._onDispose();
		}
	}
}