using System;
using System.Collections;
using System.Reflection;
using System.Web;

namespace Vertica.Utilities_v4
{
	public static class Storage
	{
		private readonly static IStorage _current;

		public readonly static object LocalStorageKey;

		public static IStorage Data
		{
			get
			{
				return Storage._current;
			}
		}

		public static bool RunningInWeb
		{
			get
			{
				return HttpContext.Current != null;
			}
		}

		static Storage()
		{
			Storage._current = new Storage.StorageData();
			Storage.LocalStorageKey = new object();
		}

		private class StorageData : IStorage
		{
			[ThreadStatic]
			private static Hashtable _threadHashtable;

			public int Count
			{
				get
				{
					return Storage.StorageData.LocalHashtable.Count;
				}
			}

			public object this[object key]
			{
				get
				{
					return Storage.StorageData.LocalHashtable[key];
				}
				set
				{
					Storage.StorageData.LocalHashtable[key] = value;
				}
			}

			private static Hashtable LocalHashtable
			{
				get
				{
					if (!Storage.RunningInWeb)
					{
						Hashtable hashtables = Storage.StorageData._threadHashtable;
						if (hashtables == null)
						{
							hashtables = new Hashtable();
							Storage.StorageData._threadHashtable = hashtables;
						}
						return hashtables;
					}
					Hashtable item = HttpContext.Current.Items[Storage.LocalStorageKey] as Hashtable;
					if (item == null)
					{
						IDictionary items = HttpContext.Current.Items;
						object localStorageKey = Storage.LocalStorageKey;
						Hashtable hashtables1 = new Hashtable();
						item = hashtables1;
						items[localStorageKey] = hashtables1;
					}
					return item;
				}
			}

			public StorageData()
			{
			}

			public void Clear()
			{
				Storage.StorageData.LocalHashtable.Clear();
			}
		}
	}
}