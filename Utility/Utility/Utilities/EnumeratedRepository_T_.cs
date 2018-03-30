using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vertica.Utilities_v4.Extensions.StringExt;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4
{
	internal class EnumeratedRepository<T>
	where T : IEnumerated
	{
		private readonly static IDictionary<string, IEnumerated> _inner;

		static EnumeratedRepository()
		{
			EnumeratedRepository<T>._inner = new Dictionary<string, IEnumerated>(StringComparer.Ordinal);
			EnumeratedRepository<T>.initializeType(typeof(T));
		}

		public EnumeratedRepository()
		{
		}

		public void Add(Enumerated<T> item)
		{
			string name = item.Name;
			if (EnumeratedRepository<T>._inner.ContainsKey(name))
			{
				ExceptionHelper.ThrowArgumentException("enunName", Exceptions.Enumerated_DuplicatedTemplate, new string[] { name });
			}
			EnumeratedRepository<T>._inner.Add(name, item);
		}

		public IEnumerable<T> FindAll()
		{
			return EnumeratedRepository<T>._inner.Values.Cast<T>();
		}

		private static void initializeType(Type t)
		{
			if (!t.IsSubclassOf(typeof(Enumerated<>)))
			{
				return;
			}
			FieldInfo[] fields = t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
			if ((int)fields.Length > 0)
			{
				fields[0].GetValue(null);
			}
			EnumeratedRepository<T>.initializeType(t.BaseType);
		}

		public bool TryFind(string enumName, out T value)
		{
			Guard.AgainstArgument("enumName", enumName.IsEmpty());
			bool flag = EnumeratedRepository<T>._inner.ContainsKey(enumName);
			value = default(T);
			if (flag)
			{
				value = (T)EnumeratedRepository<T>._inner[enumName];
			}
			return flag;
		}
	}
}