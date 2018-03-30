using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class ValueChangedEventArgs<T> : ValueEventArgs<T>, IOldValueEventArgs<T>
	{
		public T OldValue
		{
			get
			{
				return JustDecompileGenerated_get_OldValue();
			}
			set
			{
				JustDecompileGenerated_set_OldValue(value);
			}
		}

		private T JustDecompileGenerated_OldValue_k__BackingField;

		public T JustDecompileGenerated_get_OldValue()
		{
			return this.JustDecompileGenerated_OldValue_k__BackingField;
		}

		private void JustDecompileGenerated_set_OldValue(T value)
		{
			this.JustDecompileGenerated_OldValue_k__BackingField = value;
		}

		public ValueChangedEventArgs(T value, T oldValue) : base(value)
		{
			this.OldValue = oldValue;
		}
	}
}