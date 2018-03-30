using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Extensions.EnumerableExt;

namespace Vertica.Utilities_v4.Patterns
{
	public class Pipeline<T>
	{
		private readonly List<IOperation<T>> _operations;

		public Pipeline()
		{
			this._operations = new List<IOperation<T>>();
		}

		public Pipeline(params IOperation<T>[] operations) : this(operations.AsEnumerable<IOperation<T>>())
		{
		}

		public Pipeline(IEnumerable<IOperation<T>> operations) : this()
		{
			operations.ForEach<IOperation<T>>((IOperation<T> o) => this.Register(o));
		}

		public void Execute()
		{
			IEnumerable<T> ts = Enumerable.Empty<T>();
			ts = this._operations.Aggregate<IOperation<T>, IEnumerable<T>>(ts, (IEnumerable<T> input, IOperation<T> operation) => operation.Execute(input));
			foreach (T t in ts)
			{
			}
		}

		public Pipeline<T> Register(IOperation<T> operation)
		{
			if (operation != null)
			{
				this._operations.Add(operation);
			}
			return this;
		}
	}
}