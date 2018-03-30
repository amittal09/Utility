using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class PropertyValueChangedEventArgs<T> : PropertyChangedEventArgs, IOldValueEventArgs<T>, INewValueEventArgs<T>
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

		public PropertyValueChangedEventArgs(string propertyName) : base(propertyName)
		{
		}

		public PropertyValueChangedEventArgs(string propertyName, T oldValue, T newValue) : this(propertyName)
		{
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
	}
}