using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class MultiEventArgs<T, U> : ValueEventArgs<T>
	{
		public U Value2
		{
			get;
			private set;
		}

		public MultiEventArgs(T value, U value2) : base(value)
		{
			this.Value2 = value2;
		}
	}
}