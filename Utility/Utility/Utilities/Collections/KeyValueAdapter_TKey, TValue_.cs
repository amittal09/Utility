using System;
using System.Reflection;

namespace Vertica.Utilities_v4.Collections
{
	public abstract class KeyValueAdapter<TKey, TValue>
	{
		public abstract TValue this[TKey key]
		{
			get;
			set;
		}

		protected KeyValueAdapter()
		{
		}
	}
}