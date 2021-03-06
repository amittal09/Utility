using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Eventing
{
	public class ValueIndexCancelEventArgs<T> : ValueIndexEventArgs<T>, ICancelEventArgs
	{
		public bool IsCancelled
		{
			get
			{
				return JustDecompileGenerated_get_IsCancelled();
			}
			set
			{
				JustDecompileGenerated_set_IsCancelled(value);
			}
		}

		private bool JustDecompileGenerated_IsCancelled_k__BackingField;

		public bool JustDecompileGenerated_get_IsCancelled()
		{
			return this.JustDecompileGenerated_IsCancelled_k__BackingField;
		}

		private void JustDecompileGenerated_set_IsCancelled(bool value)
		{
			this.JustDecompileGenerated_IsCancelled_k__BackingField = value;
		}

		public ValueIndexCancelEventArgs(int index, T value) : base(index, value)
		{
		}

		public void Cancel()
		{
			this.IsCancelled = true;
		}
	}
}