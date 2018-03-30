using System;

namespace Vertica.Utilities_v4.Patterns
{
	public class GenericCommand<TReceiver, TReturn> : ICommand<TReturn>
	{
		private readonly TReceiver _receiver;

		private readonly Func<TReceiver, TReturn> _commandToExecute;

		public GenericCommand(TReceiver receiver, Func<TReceiver, TReturn> commandToExecute)
		{
			this._receiver = receiver;
			this._commandToExecute = commandToExecute;
		}

		public TReturn Execute()
		{
			return this._commandToExecute(this._receiver);
		}
	}
}