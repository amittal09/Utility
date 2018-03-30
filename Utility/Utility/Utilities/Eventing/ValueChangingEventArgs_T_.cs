using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class ValueChangingEventArgs<T> : ValueCancelEventArgs<T>, INewValueEventArgs<T>
	{
		public T NewValue
		{
			get
			{
				return JustDecompileGenerated_get_NewValue();
			}
			set
			{
				JustDecompileGenerated_set_NewValue(value);
			}
		}

		private T JustDecompileGenerated_NewValue_k__BackingField;

		public T JustDecompileGenerated_get_NewValue()
		{
			return this.JustDecompileGenerated_NewValue_k__BackingField;
		}

		private void JustDecompileGenerated_set_NewValue(T value)
		{
			this.JustDecompileGenerated_NewValue_k__BackingField = value;
		}

		public ValueChangingEventArgs(T value, T newValue) : base(value)
		{
			this.NewValue = newValue;
		}
	}
}