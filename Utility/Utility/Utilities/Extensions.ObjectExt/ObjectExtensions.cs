using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Extensions.TypeExt;

namespace Vertica.Utilities_v4.Extensions.ObjectExt
{
	public static class ObjectExtensions
	{
		public static T Cast<T>(this T instance)
		{
			return instance;
		}

		public static bool IsBoxedDefault(this object instance)
		{
			bool flag = true;
			if (instance != null)
			{
				Type type = instance.GetType();
				flag = (!type.IsValueType ? false : instance.Equals(type.GetDefault()));
			}
			return flag;
		}

		public static bool IsDefault<T>(this T instance)
		{
			bool flag = true;
			if (instance != null)
			{
				flag = instance.Equals(default(T));
			}
			return flag;
		}

		public static bool IsIntegral<T>(this T o)
		{
			if ((object)o is byte || (object)o is sbyte || (object)o is short || (object)o is ushort || (object)o is int || (object)o is uint || (object)o is long)
			{
				return true;
			}
			return (object)o is ulong;
		}

		public static bool IsNotDefault<T>(this T instance)
		{
			return !instance.IsDefault<T>();
		}

		private static bool notSafe<T>(T argument, Predicate<T> isSafe)
		where T : class
		{
			return (isSafe != null ? !isSafe(argument) : EqualityComparer<T>.Default.Equals(argument, default(T)));
		}

		public static U Safe<T, U>(this T argument, Func<T, U> func, Predicate<T> isSafe = null)
		where T : class
		where U : class
		{
			if (!ObjectExtensions.notSafe<T>(argument, isSafe))
			{
				return func(argument);
			}
			return default(U);
		}

		public static string SafeToString<T>(this T instance, string @default = null)
		where T : class
		{
			if (instance == null)
			{
				return @default;
			}
			return instance.ToString();
		}

		public static Nullable<U> SafeValue<T, U>(this T argument, Func<T, U> func, Predicate<T> isSafe = null)
		where T : class
		where U : struct
		{
			if (ObjectExtensions.notSafe<T>(argument, isSafe))
			{
				return null;
			}
			return new Nullable<U>(func(argument));
		}

		public static Nullable<U> SafeValue<T, U>(this T argument, Func<T, Nullable<U>> func, Predicate<T> isSafe = null)
		where T : class
		where U : struct
		{
			if (!ObjectExtensions.notSafe<T>(argument, isSafe))
			{
				return func(argument);
			}
			return null;
		}

		public static Nullable<T> Unbox<T>(this object o)
		where T : struct
		{
			Nullable<T> nullable = null;
			try
			{
				if (o != null)
				{
					nullable = (Nullable<T>)Convert.ChangeType(o, typeof(T));
				}
			}
			catch (FormatException formatException)
			{
			}
			catch (OverflowException overflowException)
			{
			}
			catch (InvalidCastException invalidCastException)
			{
			}
			return nullable;
		}

		public static bool? UnboxBool(this object o)
		{
			bool flag;
			bool? nullable = null;
			try
			{
				if (o != DBNull.Value)
				{
					if (o is bool)
					{
						nullable = new bool?((bool)o);
					}
					else if (o.IsIntegral<object>())
					{
						long num = Convert.ToInt64(o);
						if (num == (long)0)
						{
							nullable = new bool?(false);
						}
						if (num == (long)1)
						{
							nullable = new bool?(true);
						}
					}
					else if (!(o is char))
					{
						string str = o as string;
						if (!bool.TryParse(str, out flag))
						{
							IEqualityComparer<string> ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
							if (ordinalIgnoreCase.Equals(str, "1"))
							{
								nullable = new bool?(true);
							}
							else if (ordinalIgnoreCase.Equals(str, "0"))
							{
								nullable = new bool?(false);
							}
							else if (ordinalIgnoreCase.Equals(str, "t"))
							{
								nullable = new bool?(true);
							}
							else if (ordinalIgnoreCase.Equals(str, "f"))
							{
								nullable = new bool?(false);
							}
						}
						else
						{
							nullable = new bool?(flag);
						}
					}
					else
					{
						char chr = (char)o;
						if (chr.Equals('0'))
						{
							nullable = new bool?(false);
						}
						if (chr.Equals('1'))
						{
							nullable = new bool?(true);
						}
						if (chr.Equals('t') || chr.Equals('T'))
						{
							nullable = new bool?(true);
						}
						if (chr.Equals('f') || chr.Equals('F'))
						{
							nullable = new bool?(false);
						}
					}
				}
			}
			catch
			{
			}
			return nullable;
		}
	}
}