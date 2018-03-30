using System;
using System.Configuration;
using System.Reflection;

namespace Vertica.Utilities_v4.Configuration
{
	public abstract class ConfigurationElementCollection<TKey, TValue> : ConfigurationElementCollection
	where TValue : ConfigurationElement, new()
	{
		public override ConfigurationElementCollectionType CollectionType
		{
			get;
		}

		protected override string ElementName
		{
			get;
		}

		public TValue this[TKey key]
		{
			get
			{
				return (TValue)base.BaseGet(key);
			}
		}

		public TValue this[int index]
		{
			get
			{
				return (TValue)base.BaseGet(index);
			}
		}

		protected ConfigurationElementCollection()
		{
		}

		public void Add(TValue path)
		{
			this.BaseAdd(path);
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return (ConfigurationElement)Activator.CreateInstance<TValue>();
		}

		protected abstract TKey GetElementKey(TValue element);

		protected override object GetElementKey(ConfigurationElement element)
		{
			return this.GetElementKey((TValue)element);
		}

		public void Remove(TKey key)
		{
			base.BaseRemove(key);
		}
	}
}