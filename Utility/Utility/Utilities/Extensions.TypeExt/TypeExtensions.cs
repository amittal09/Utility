using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Vertica.Utilities_v4.Extensions.StringExt;

namespace Vertica.Utilities_v4.Extensions.TypeExt
{
	public static class TypeExtensions
	{
		public static IEnumerable<Type> AllInterfaces(this Type target)
		{
			Type[] interfaces = target.GetInterfaces();
			for (int i = 0; i < (int)interfaces.Length; i++)
			{
				Type type = interfaces[i];
				yield return type;
				foreach (Type type1 in type.AllInterfaces())
				{
					yield return type1;
				}
			}
		}

		public static IEnumerable<MethodInfo> AllMethods(this Type target)
		{
			Type[] typeArray = new Type[] { target };
			return typeArray.Concat<Type>(target.AllInterfaces()).SelectMany<Type, MethodInfo>((Type type) => type.GetMethods());
		}

		public static bool CanBeNulled(this Type type)
		{
			if (type == null)
			{
				return false;
			}
			if (!type.IsValueType)
			{
				return true;
			}
			return type.IsNullable();
		}

		public static T GetDefault<T>(this Type t)
		{
			return (T)t.GetDefault();
		}

		public static object GetDefault(this Type t)
		{
			if (!t.IsValueType)
			{
				return null;
			}
			return Activator.CreateInstance(t);
		}

		public static bool IsNullable(this Type type)
		{
			if (type == null || !type.IsValueType)
			{
				return false;
			}
			if (!type.IsGenericType)
			{
				return false;
			}
			return type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		public static string LongName(this Type target, bool includeNamespace = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!includeNamespace)
			{
				stringBuilder.Append(target.Name);
			}
			else
			{
				stringBuilder.Append(target.NameWithNamespace());
			}
			if (target.IsGenericType)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
				stringBuilder.Append("<");
				Type[] genericArguments = target.GetGenericArguments();
				if (target.IsGenericTypeDefinition)
				{
					for (int i = 0; i < (int)genericArguments.Length - 1; i++)
					{
						stringBuilder.Append(",");
					}
				}
				else
				{
					Type[] typeArray = genericArguments;
					for (int j = 0; j < (int)typeArray.Length; j++)
					{
						Type type = typeArray[j];
						if (!includeNamespace)
						{
							stringBuilder.Append(type.Name);
						}
						else
						{
							stringBuilder.Append(type.NameWithNamespace());
						}
						stringBuilder.Append(", ");
					}
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
				stringBuilder.Append(">");
			}
			return stringBuilder.ToString();
		}

		public static string NameWithNamespace(this Type target)
		{
			if (!target.Namespace.IsNotEmpty())
			{
				return target.Name;
			}
			return string.Concat(target.Namespace, ".", target.Name);
		}
	}
}