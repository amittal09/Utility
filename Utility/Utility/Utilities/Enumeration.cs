using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4
{
	public static class Enumeration
	{
		private static Enum asEnum<T>(T t)
		{
			return (Enum)(object)t;
		}

		public static void AssertDefined<TEnum>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, TEnum>(value);
			}
		}

		public static void AssertDefined<TEnum>(byte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, byte>(value);
			}
		}

		public static void AssertDefined<TEnum>(sbyte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, sbyte>(value);
			}
		}

		public static void AssertDefined<TEnum>(short value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, short>(value);
			}
		}

		public static void AssertDefined<TEnum>(ushort value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, ushort>(value);
			}
		}

		public static void AssertDefined<TEnum>(int value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, int>(value);
			}
		}

		public static void AssertDefined<TEnum>(uint value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, uint>(value);
			}
		}

		public static void AssertDefined<TEnum>(long value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, long>(value);
			}
		}

		public static void AssertDefined<TEnum>(ulong value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(value))
			{
				Enumeration.throwNotDefined<TEnum, ulong>(value);
			}
		}

		public static void AssertDefined<TEnum>(string name, bool ignoreCase = false)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsDefined<TEnum>(name, ignoreCase))
			{
				Enumeration.throwNotDefined<TEnum, string>(name);
			}
		}

		public static void AssertEnum<TEnum>()
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsEnum<TEnum>())
			{
				string enumerationNotEnumTemplate = Exceptions.Enumeration_NotEnumTemplate;
				string[] str = new string[] { typeof(TEnum).ToString() };
				ExceptionHelper.Throw<ArgumentException>(enumerationNotEnumTemplate, str);
			}
		}

		public static void AssertFlags<TFlags>()
		where TFlags : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TFlags>();
			if (!Enumeration.IsFlags<TFlags>())
			{
				ExceptionHelper.Throw<ArgumentException>(Exceptions.Enumeration_NoFlagsTemplate, new string[] { typeof(TFlags).Name });
			}
		}

		public static TEnum Cast<TEnum>(byte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, byte>(value);
			}
			return nullable.Value;
		}

		public static TEnum Cast<TEnum>(sbyte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, sbyte>(value);
			}
			return nullable.Value;
		}

		public static TEnum Cast<TEnum>(short value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, short>(value);
			}
			return nullable.Value;
		}

		public static TEnum Cast<TEnum>(ushort value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, ushort>(value);
			}
			return nullable.Value;
		}

		public static TEnum Cast<TEnum>(int value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, int>(value);
			}
			return nullable.Value;
		}

		public static TEnum Cast<TEnum>(uint value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, uint>(value);
			}
			return nullable.Value;
		}

		public static TEnum Cast<TEnum>(long value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, long>(value);
			}
			return nullable.Value;
		}

		public static TEnum Cast<TEnum>(ulong value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryCast<TEnum>(value, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, ulong>(value);
			}
			return nullable.Value;
		}

		public static U GetAttribute<TEnum, U>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		where U : Attribute
		{
			Enumeration.AssertEnum<TEnum>();
			return (U)Attribute.GetCustomAttribute(Enumeration.GetField<TEnum>(value), typeof(U), false);
		}

		public static IEqualityComparer<TEnum> GetComparer<TEnum>()
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			return Enumeration.FastEnumComparer<TEnum>.Instance;
		}

		public static string GetDescription<TEnum>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			DescriptionAttribute attribute = Enumeration.GetAttribute<TEnum, DescriptionAttribute>(value);
			if (attribute == null)
			{
				return null;
			}
			return attribute.Description;
		}

		public static FieldInfo GetField<TEnum>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			return typeof(TEnum).GetField(value.ToString(CultureInfo.InvariantCulture));
		}

		public static IEnumerable<TFlags> GetFlags<TFlags>(this TFlags flag, bool ignoreZeroValue = false)
		where TFlags : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertFlags<TFlags>();
			return Enumeration.GetValues<TFlags>().Where<TFlags>((TFlags t) => {
				if (!ignoreZeroValue)
				{
					return true;
				}
				return !object.Equals(t, default(TFlags));
			}).Where<TFlags>((TFlags t) => Enumeration.asEnum<TFlags>(flag).HasFlag(Enumeration.asEnum<TFlags>(t)));
		}

		private static string getName<TEnum, U>(U value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		where U : struct, IComparable, IFormattable, IConvertible
		{
			return Enum.GetName(typeof(TEnum), value);
		}

		public static string GetName<TEnum>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, TEnum>(value);
		}

		public static string GetName<TEnum>(byte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, byte>(value);
		}

		public static string GetName<TEnum>(sbyte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, sbyte>(value);
		}

		public static string GetName<TEnum>(short value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, short>(value);
		}

		public static string GetName<TEnum>(ushort value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, ushort>(value);
		}

		public static string GetName<TEnum>(int value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, int>(value);
		}

		public static string GetName<TEnum>(uint value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, uint>(value);
		}

		public static string GetName<TEnum>(long value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, long>(value);
		}

		public static string GetName<TEnum>(ulong value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertDefined<TEnum>(value);
			return Enumeration.getName<TEnum, ulong>(value);
		}

		public static IEnumerable<string> GetNames<TEnum>()
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			return Enum.GetNames(typeof(TEnum)).AsEnumerable<string>();
		}

		public static Type GetUnderlyingType<TEnum>()
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			return Enum.GetUnderlyingType(typeof(TEnum));
		}

		public static TNumeric GetValue<TEnum, TNumeric>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		where TNumeric : struct, IComparable, IFormattable, IConvertible, IComparable<TNumeric>, IEquatable<TNumeric>
		{
			Enumeration.AssertDefined<TEnum>(value);
			return (TNumeric)Convert.ChangeType(value, typeof(TNumeric));
		}

		public static IEnumerable<TEnum> GetValues<TEnum>()
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
		}

		public static bool HasAttribute<TEnum, U>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		where U : Attribute
		{
			return Enumeration.GetAttribute<TEnum, U>(value) != null;
		}

		public static IEnumerable<TEnum> Invert<TEnum>(IEnumerable<TEnum> valuesToRemove)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			IEnumerable<TEnum> values = Enumeration.GetValues<TEnum>();
			if (valuesToRemove == null)
			{
				return values;
			}
			return values.Except<TEnum>(valuesToRemove);
		}

		public static IEnumerable<TEnum> Invert<TEnum>(params TEnum[] valuesToRemove)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.Invert<TEnum>((IEnumerable<TEnum>)valuesToRemove);
		}

		private static bool isDefined<TEnum, U>(U value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		where U : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			return Enum.IsDefined(typeof(TEnum), value);
		}

		public static bool IsDefined<TEnum>(TEnum value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			return Enum.IsDefined(typeof(TEnum), value);
		}

		public static bool IsDefined<TEnum>(byte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, byte>(value);
		}

		public static bool IsDefined<TEnum>(sbyte value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, sbyte>(value);
		}

		public static bool IsDefined<TEnum>(short value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, short>(value);
		}

		public static bool IsDefined<TEnum>(ushort value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, ushort>(value);
		}

		public static bool IsDefined<TEnum>(int value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, int>(value);
		}

		public static bool IsDefined<TEnum>(uint value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, uint>(value);
		}

		public static bool IsDefined<TEnum>(long value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, long>(value);
		}

		public static bool IsDefined<TEnum>(ulong value)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return Enumeration.isDefined<TEnum, ulong>(value);
		}

		public static bool IsDefined<TEnum>(string name, bool ignoreCase = false)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertEnum<TEnum>();
			if (ignoreCase)
			{
				return Enumeration.GetNames<TEnum>().Contains<string>(name, StringComparer.OrdinalIgnoreCase);
			}
			return Enum.IsDefined(typeof(TEnum), name);
		}

		public static bool IsEnum<TEnum>()
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			return typeof(TEnum).IsEnum;
		}

		public static bool IsFlags<TFlags>()
		where TFlags : struct, IComparable, IFormattable, IConvertible
		{
			if (!Enumeration.IsEnum<TFlags>())
			{
				return false;
			}
			return typeof(TFlags).IsDefined(typeof(FlagsAttribute), false);
		}

		public static TEnum Parse<TEnum>(string value, bool ignoreCase = false)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Nullable<TEnum> nullable;
			if (!Enumeration.TryParse<TEnum>(value, ignoreCase, out nullable))
			{
				Enumeration.throwNotDefined<TEnum, string>(value);
			}
			return nullable.Value;
		}

		public static TFlags SetFlag<TFlags>(this TFlags flags, TFlags flagToSet)
		where TFlags : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertFlags<TFlags>();
			return Enumeration.Flags<TFlags>.bitwiseOr(flags, flagToSet);
		}

		private static void throwNotDefined<TEnum, U>(U valueOrName)
		{
			throw new InvalidEnumArgumentException(string.Format(Exceptions.Enumeration_ValueNotDefinedTemplate, valueOrName, typeof(TEnum).Name));
		}

		public static TFlags ToggleFlag<TFlags>(this TFlags flags, TFlags flagToToggle)
		where TFlags : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertFlags<TFlags>();
			if (!Enumeration.asEnum<TFlags>(flags).HasFlag(Enumeration.asEnum<TFlags>(flagToToggle)))
			{
				return flags.SetFlag<TFlags>(flagToToggle);
			}
			return flags.UnsetFlag<TFlags>(flagToToggle);
		}

		public static bool TryCast<TEnum>(byte value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryCast<TEnum>(sbyte value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryCast<TEnum>(short value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryCast<TEnum>(ushort value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryCast<TEnum>(int value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryCast<TEnum>(uint value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryCast<TEnum>(long value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryCast<TEnum>(ulong value, out Nullable<TEnum> casted)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			casted = null;
			bool flag = Enumeration.IsDefined<TEnum>(value);
			if (flag)
			{
				casted = new Nullable<TEnum>((TEnum)Enum.ToObject(typeof(TEnum), value));
			}
			return flag;
		}

		public static bool TryGetName<TEnum>(TEnum value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, TEnum>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(byte value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, byte>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(sbyte value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, sbyte>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(short value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, short>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(ushort value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, ushort>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(int value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, int>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(uint value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, uint>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(long value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, long>(value);
			return name != null;
		}

		public static bool TryGetName<TEnum>(ulong value, out string name)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			name = Enumeration.getName<TEnum, ulong>(value);
			return name != null;
		}

		public static bool TryGetValue<TEnum, TNumeric>(TEnum value, out Nullable<TNumeric> numericValue)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		where TNumeric : struct, IComparable, IFormattable, IConvertible, IComparable<TNumeric>, IEquatable<TNumeric>
		{
			bool flag = false;
			numericValue = null;
			if (Enumeration.IsDefined<TEnum>(value))
			{
				try
				{
					numericValue = new Nullable<TNumeric>((TNumeric)Convert.ChangeType(value, typeof(TNumeric)));
					flag = true;
				}
				catch (InvalidCastException invalidCastException)
				{
				}
				catch (OverflowException overflowException)
				{
				}
			}
			return flag;
		}

		public static bool TryParse<TEnum>(string value, out Nullable<TEnum> parsed)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			TEnum tEnum;
			parsed = null;
			Type type = typeof(TEnum);
			Enumeration.AssertEnum<TEnum>();
			bool flag = false;
			if (Enum.TryParse<TEnum>(value, out tEnum))
			{
				flag = Enum.IsDefined(type, tEnum);
				parsed = new Nullable<TEnum>(tEnum);
			}
			return flag;
		}

		public static bool TryParse<TEnum>(string value, bool ignoreCase, out Nullable<TEnum> parsed)
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			TEnum tEnum;
			parsed = null;
			Type type = typeof(TEnum);
			Enumeration.AssertEnum<TEnum>();
			bool flag = false;
			if (Enum.TryParse<TEnum>(value, ignoreCase, out tEnum))
			{
				flag = Enum.IsDefined(type, tEnum);
				parsed = new Nullable<TEnum>(tEnum);
			}
			return flag;
		}

		public static TFlags UnsetFlag<TFlags>(this TFlags flags, TFlags flagToSet)
		where TFlags : struct, IComparable, IFormattable, IConvertible
		{
			Enumeration.AssertFlags<TFlags>();
			return Enumeration.Flags<TFlags>.bitwiseAnd(flags, Enumeration.Flags<TFlags>.not(flagToSet));
		}

		internal class FastEnumComparer<TEnum> : IEqualityComparer<TEnum>
		where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			public readonly static IEqualityComparer<TEnum> Instance;

			private readonly static Func<TEnum, TEnum, bool> equals;

			private readonly static Func<TEnum, int> getHashCode;

			static FastEnumComparer()
			{
				Enumeration.FastEnumComparer<TEnum>.getHashCode = Enumeration.FastEnumComparer<TEnum>.generateGetHashCode();
				Enumeration.FastEnumComparer<TEnum>.equals = Enumeration.FastEnumComparer<TEnum>.generateEquals();
				Enumeration.FastEnumComparer<TEnum>.Instance = new Enumeration.FastEnumComparer<TEnum>();
			}

			private FastEnumComparer()
			{
				Enumeration.FastEnumComparer<TEnum>.assertUnderlyingTypeIsSupported();
			}

			private static void assertUnderlyingTypeIsSupported()
			{
				Type underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
				Type[] typeArray = new Type[] { typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) };
				ICollection<Type> types = typeArray;
				if (!types.Contains(underlyingType))
				{
					string str = string.Join(", ", 
						from  t in types
						select t.Name);
					string enumerationNotSupportedUnderlyingTypeTemplate = Exceptions.Enumeration_NotSupportedUnderlyingTypeTemplate;
					string[] name = new string[] { typeof(TEnum).Name, underlyingType.Name, str };
					ExceptionHelper.Throw<NotSupportedException>(enumerationNotSupportedUnderlyingTypeTemplate, name);
				}
			}

			public bool Equals(TEnum x, TEnum y)
			{
				return Enumeration.FastEnumComparer<TEnum>.equals(x, y);
			}

			private static Func<TEnum, TEnum, bool> generateEquals()
			{
				return ((TEnum x, TEnum y) => x.Equals(y));
			}

			private static Func<TEnum, int> generateGetHashCode()
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(TEnum), "obj");
				Type underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
				UnaryExpression unaryExpression = Expression.Convert(parameterExpression, underlyingType);
				MethodCallExpression methodCallExpression = Expression.Call(unaryExpression, underlyingType.GetMethod("GetHashCode"));
				ParameterExpression[] parameterExpressionArray = new ParameterExpression[] { parameterExpression };
				return Expression.Lambda<Func<TEnum, int>>(methodCallExpression, parameterExpressionArray).Compile();
			}

			public int GetHashCode(TEnum obj)
			{
				return Enumeration.FastEnumComparer<TEnum>.getHashCode(obj);
			}
		}

		private static class Flags<TFlags>
		where TFlags : struct, IComparable, IFormattable, IConvertible
		{
			internal readonly static Func<TFlags, TFlags, TFlags> bitwiseOr;

			internal readonly static Func<TFlags, TFlags, TFlags> bitwiseAnd;

			internal readonly static Func<TFlags, TFlags> not;

			static Flags()
			{
				Type underlyingType = Enumeration.GetUnderlyingType<TFlags>();
				Type type = typeof(TFlags);
				ParameterExpression parameterExpression = Expression.Parameter(type, "x");
				ParameterExpression parameterExpression1 = Expression.Parameter(type, "y");
				Expression expression = Expression.Convert(parameterExpression, underlyingType);
				Expression expression1 = Expression.Convert(parameterExpression1, underlyingType);
				UnaryExpression unaryExpression = Expression.Convert(Expression.Or(expression, expression1), type);
				ParameterExpression[] parameterExpressionArray = new ParameterExpression[] { parameterExpression, parameterExpression1 };
				Enumeration.Flags<TFlags>.bitwiseOr = Expression.Lambda<Func<TFlags, TFlags, TFlags>>(unaryExpression, parameterExpressionArray).Compile();
				UnaryExpression unaryExpression1 = Expression.Convert(Expression.And(expression, expression1), type);
				ParameterExpression[] parameterExpressionArray1 = new ParameterExpression[] { parameterExpression, parameterExpression1 };
				Enumeration.Flags<TFlags>.bitwiseAnd = Expression.Lambda<Func<TFlags, TFlags, TFlags>>(unaryExpression1, parameterExpressionArray1).Compile();
				UnaryExpression unaryExpression2 = Expression.Convert(Expression.Not(expression), type);
				ParameterExpression[] parameterExpressionArray2 = new ParameterExpression[] { parameterExpression };
				Enumeration.Flags<TFlags>.not = Expression.Lambda<Func<TFlags, TFlags>>(unaryExpression2, parameterExpressionArray2).Compile();
			}
		}
	}
}