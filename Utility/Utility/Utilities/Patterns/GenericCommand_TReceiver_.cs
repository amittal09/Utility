using System;

namespace Vertica.Utilities_v4.Patterns
{
	public class GenericCommand<TReceiver> : ICommand
	{
		private readonly TReceiver _receiver;

		private readonly Action<TReceiver> _commandToExecute;

		public GenericCommand(TReceiver receiver, Action<TReceiver> commandToExecute)
		{
			this._receiver = receiver;
			this._commandToExecute = commandToExecute;
		}

		public void Execute()
		{
			this._commandToExecute(this._receiver);
		}
	}
}