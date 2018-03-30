using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Extensions.AttributeExt
{
	public static class AttributeExtensions
	{
		public static T GetAttribute<T>(this object element, bool inherit = false)
		where T : Attribute
		{
			return (T)(Attribute.GetCustomAttribute(element.GetType(), typeof(T), inherit) as T);
		}

		public static T GetAttribute<T>(this Assembly element, bool inherit = false)
		where T : Attribute
		{
			return (T)(Attribute.GetCustomAttribute(element, typeof(T), inherit) as T);
		}

		public static T GetAttribute<T>(this MemberInfo element, bool inherit = false)
		where T : Attribute
		{
			return (T)(Attribute.GetCustomAttribute(element, typeof(T), inherit) as T);
		}

		public static T GetAttribute<T>(this Module element, bool inherit = false)
		where T : Attribute
		{
			return (T)(Attribute.GetCustomAttribute(element, typeof(T), inherit) as T);
		}

		public static T GetAttribute<T>(this ParameterInfo element, bool inherit = false)
		where T : Attribute
		{
			return (T)(Attribute.GetCustomAttribute(element, typeof(T), inherit) as T);
		}

		public static T[] GetAttributes<T>(this object element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.GetCustomAttributes(element.GetType(), typeof(T), inherit) as T[];
		}

		public static T[] GetAttributes<T>(this Assembly element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.GetCustomAttributes(element, typeof(T), inherit) as T[];
		}

		public static T[] GetAttributes<T>(this MemberInfo element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.GetCustomAttributes(element, typeof(T), inherit) as T[];
		}

		public static T[] GetAttributes<T>(this Module element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.GetCustomAttributes(element, typeof(T), inherit) as T[];
		}

		public static T[] GetAttributes<T>(this ParameterInfo element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.GetCustomAttributes(element, typeof(T), inherit) as T[];
		}

		public static bool HasAttribute<T>(this object element, bool inherit = false)
		where T : Attribute
		{
			if (element == null)
			{
				return false;
			}
			return Attribute.IsDefined(element.GetType(), typeof(T), inherit);
		}

		public static bool HasAttribute<T>(this Assembly element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.IsDefined(element, typeof(T), inherit);
		}

		public static bool HasAttribute<T>(this MemberInfo element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.IsDefined(element, typeof(T), inherit);
		}

		public static bool HasAttribute<T>(this Module element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.IsDefined(element, typeof(T), inherit);
		}

		public static bool HasAttribute<T>(this ParameterInfo element, bool inherit = false)
		where T : Attribute
		{
			return Attribute.IsDefined(element, typeof(T), inherit);
		}
	}
}