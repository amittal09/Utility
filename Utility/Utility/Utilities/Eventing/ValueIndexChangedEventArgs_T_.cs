using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class ValueIndexChangedEventArgs<T> : ValueChangedEventArgs<T>, IIndexEventArgs
	{
		public int Index
		{
			get
			{
				return JustDecompileGenerated_get_Index();
			}
			set
			{
				JustDecompileGenerated_set_Index(value);
			}
		}

		private int JustDecompileGenerated_Index_k__BackingField;

		public int JustDecompileGenerated_get_Index()
		{
			return this.JustDecompileGenerated_Index_k__BackingField;
		}

		private void JustDecompileGenerated_set_Index(int value)
		{
			this.JustDecompileGenerated_Index_k__BackingField = value;
		}

		public ValueIndexChangedEventArgs(int index, T oldValue, T newValue) : base(newValue, oldValue)
		{
			this.Index = index;
		}
	}
}