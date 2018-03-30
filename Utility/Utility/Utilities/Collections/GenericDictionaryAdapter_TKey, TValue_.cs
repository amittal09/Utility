using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vertica.Utilities_v4.Collections
{
	public class GenericDictionaryAdapter<TKey, TValue> : KeyValueAdapter<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> _adaptee;

		public override TValue this[TKey key]
		{
			get
			{
				return this._adaptee[key];
			}
			set
			{
				this._adaptee[key] = value;
			}
		}

		public GenericDictionaryAdapter(IDictionary<TKey, TValue> adaptee)
		{
			this._adaptee = adaptee;
		}
	}
}