using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Extensions.Infrastructure;

namespace Vertica.Utilities_v4.Extensions.AnonymousExt
{
	public static class AnonymousExtensions
	{
		private static IEnumerable<TResult> anonymousAs<TAnonymous, TResult>(TAnonymous anonymous, Func<string, object, TResult> result)
		where TAnonymous : class
		{
			if (anonymous != null)
			{
				foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(anonymous))
				{
					object value = property.GetValue(anonymous);
					if (value == null)
					{
						continue;
					}
					yield return result(property.Name, value);
				}
			}
		}

		public static T AsAnonymous<T>(this IDictionary<string, object> dict, T anonymousPrototype)
		where T : class
		{
			ConstructorInfo constructorInfo = anonymousPrototype.GetType().GetConstructors().Single<ConstructorInfo>();
			Func<IDictionary<string, object>, string, object> func = (IDictionary<string, object> d, string key) => {
				object obj;
				if (!d.TryGetValue(key, out obj))
				{
					return null;
				}
				return obj;
			};
			IEnumerable<object> objs = (
				from  p in constructorInfo.GetParameters()
				select new { p = p, val = func(dict, p.Name) }).Select((a) => {
				if (a.val == null || !a.p.ParameterType.IsInstanceOfType(a.val))
				{
					return null;
				}
				return a.val;
			});
			return (T)constructorInfo.Invoke(objs.ToArray<object>());
		}

		public static IDictionary<string, object> AsDictionary<T>(this T anonymousObject)
		where T : class
		{
			return anonymousObject.AsKeyValuePairs<T>().ToDictionary<KeyValuePair<string, object>, string, object>((KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => p.Value);
		}

		public static IEnumerable<KeyValuePair<string, object>> AsKeyValuePairs<T>(this T anonymousObject)
		where T : class
		{
			return AnonymousExtensions.anonymousAs<T, KeyValuePair<string, object>>(anonymousObject, (string propName, object val) => new KeyValuePair<string, object>(propName, val));
		}

		public static IEnumerable<Tuple<string, object>> AsTuples<T>(this T anonymousObject)
		where T : class
		{
			return AnonymousExtensions.anonymousAs<T, Tuple<string, object>>(anonymousObject, new Func<string, object, Tuple<string, object>>(Tuple.Create<string, object>));
		}

		public static T ByExample<T>(this CastExtensionPoint<object> obj, T example)
		where T : class
		{
			return (T)obj.ExtendedValue;
		}
	}
}