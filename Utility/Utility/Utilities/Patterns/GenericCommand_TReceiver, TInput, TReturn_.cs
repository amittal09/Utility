using System;

namespace Vertica.Utilities_v4.Patterns
{
	public class GenericCommand<TReceiver, TInput, TReturn> : ICommand<TInput, TReturn>
	{
		private readonly TReceiver _receiver;

		private readonly Func<TReceiver, TInput, TReturn> _commandToExecute;

		public GenericCommand(TReceiver receiver, Func<TReceiver, TInput, TReturn> commandToExecute)
		{
			this._receiver = receiver;
			this._commandToExecute = commandToExecute;
		}

		public TReturn Execute(TInput input)
		{
			return this._commandToExecute(this._receiver, input);
		}
	}
}