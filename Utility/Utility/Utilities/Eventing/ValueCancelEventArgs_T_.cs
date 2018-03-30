using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class ValueCancelEventArgs<T> : CancelEventArgs, IValueEventArgs<T>
	{
		public T Value
		{
			get
			{
				return JustDecompileGenerated_get_Value();
			}
			set
			{
				JustDecompileGenerated_set_Value(value);
			}
		}

		private T JustDecompileGenerated_Value_k__BackingField;

		public T JustDecompileGenerated_get_Value()
		{
			return this.JustDecompileGenerated_Value_k__BackingField;
		}

		private void JustDecompileGenerated_set_Value(T value)
		{
			this.JustDecompileGenerated_Value_k__BackingField = value;
		}

		public ValueCancelEventArgs(T value)
		{
			this.Value = value;
		}
	}
}