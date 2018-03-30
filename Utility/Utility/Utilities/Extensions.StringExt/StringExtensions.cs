using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Extensions.Infrastructure;
using Vertica.Utilities_v4.Extensions.ObjectExt;

namespace Vertica.Utilities_v4.Extensions.StringExt
{
	public static class StringExtensions
	{
		private readonly static string _strippingPattern;

		private readonly static Regex _strippingRegEx;

		static StringExtensions()
		{
			StringExtensions._strippingPattern = "<(.|\\n)*?>";
			StringExtensions._strippingRegEx = new Regex(StringExtensions._strippingPattern, RegexOptions.Compiled);
		}

		public static string Append(this StringExtensions.IfNotThereExtensionPoint sp, string appendix)
		{
			string extendedValue = sp.ExtendedValue;
			if (extendedValue == null && appendix == null)
			{
				return null;
			}
			return StringExtensions.appendIfNotThere(extendedValue.EmptyIfNull(), appendix.EmptyIfNull());
		}

		private static string appendIfNotThere(string str, string appendix)
		{
			if (str.EndsWith(appendix))
			{
				return str;
			}
			return string.Concat(str, appendix);
		}

		public static Nullable<T> AsNullable<T>(this ExtensionPoint<string> s, Func<string, T> parser)
		where T : struct
		{
			Nullable<T> nullable = null;
			if (s.ExtendedValue.IsNotEmpty())
			{
				nullable = new Nullable<T>(parser(s.ExtendedValue));
			}
			return nullable;
		}

		public static string AttributeEncode(this StringExtensions.HttpExtensionPoint str)
		{
			return HttpUtility.HtmlAttributeEncode(str.ExtendedValue);
		}

		public static IEnumerable<string> Chunkify(this string text, uint chunkSize)
		{
			int num = 0;
			for (int i = 0; i < text.Length; i += num)
			{
				num = Math.Min((int)chunkSize, text.Length - i);
				yield return text.Substring(i, num);
			}
		}

		public static string CombineWith(this StringExtensions.VirtualPathExtensionPoint basePath, string relativePath)
		{
			return VirtualPathUtility.Combine(basePath.ExtendedValue, relativePath);
		}

		public static string Compress(this string text)
		{
			string base64String;
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gZipStream.Write(bytes, 0, (int)bytes.Length);
				}
				memoryStream.Position = (long)0;
				byte[] numArray = new byte[checked((int)memoryStream.Length)];
				memoryStream.Read(numArray, 0, (int)numArray.Length);
				byte[] numArray1 = new byte[(int)numArray.Length + 4];
				Buffer.BlockCopy(numArray, 0, numArray1, 4, (int)numArray.Length);
				Buffer.BlockCopy(BitConverter.GetBytes((int)bytes.Length), 0, numArray1, 0, 4);
				base64String = Convert.ToBase64String(numArray1);
			}
			return base64String;
		}

		public static string Decompress(this string compressedText)
		{
			string str;
			byte[] numArray = Convert.FromBase64String(compressedText);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				int num = BitConverter.ToInt32(numArray, 0);
				memoryStream.Write(numArray, 4, (int)numArray.Length - 4);
				byte[] numArray1 = new byte[num];
				memoryStream.Position = (long)0;
				using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					gZipStream.Read(numArray1, 0, (int)numArray1.Length);
				}
				str = Encoding.UTF8.GetString(numArray1);
			}
			return str;
		}

		public static string EmptyIfNull(this string s)
		{
			return s ?? string.Empty;
		}

		public static string FormatWith(this string s, params object[] additionalArgs)
		{
			return s.Safe<string, string>((string input) => (additionalArgs == null || (int)additionalArgs.Length == 0 ? input : string.Format(input, additionalArgs)), null);
		}

		public static string GetDirectory(this StringExtensions.VirtualPathExtensionPoint virtualPath)
		{
			return VirtualPathUtility.GetDirectory(virtualPath.ExtendedValue);
		}

		public static string GetExtension(this StringExtensions.VirtualPathExtensionPoint virtualPath)
		{
			return VirtualPathUtility.GetExtension(virtualPath.ExtendedValue);
		}

		public static string GetFileName(this StringExtensions.VirtualPathExtensionPoint virtualPath)
		{
			return VirtualPathUtility.GetFileName(virtualPath.ExtendedValue);
		}

		public static MemoryStream GetMemoryStream(this StringExtensions.IOExtensionPoint<string> s)
		{
			MemoryStream memoryStream = null;
			if (s.ExtendedValue.IsNotEmpty())
			{
				memoryStream = new MemoryStream();
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				streamWriter.Write(s.ExtendedValue);
				streamWriter.Flush();
			}
			return memoryStream;
		}

		public static string GetString(this StringExtensions.IOExtensionPoint<MemoryStream> m)
		{
			string end = null;
			if (m != null && m.ExtendedValue != null && m.ExtendedValue.Length > (long)0)
			{
				m.ExtendedValue.Flush();
				m.ExtendedValue.Position = (long)0;
				end = (new StreamReader(m.ExtendedValue)).ReadToEnd();
			}
			return end;
		}

		public static string HtmlDecode(this StringExtensions.HttpExtensionPoint str)
		{
			return HttpUtility.HtmlDecode(str.ExtendedValue);
		}

		public static string HtmlEncode(this StringExtensions.HttpExtensionPoint str)
		{
			return HttpUtility.HtmlEncode(str.ExtendedValue);
		}

		public static StringExtensions.HttpExtensionPoint Http(this string subject)
		{
			return new StringExtensions.HttpExtensionPoint(subject);
		}

		public static StringExtensions.IfNotThereExtensionPoint IfNotThere(this string subject)
		{
			return new StringExtensions.IfNotThereExtensionPoint(subject);
		}

		public static StringExtensions.IOExtensionPoint<MemoryStream> IO(this MemoryStream subject)
		{
			return new StringExtensions.IOExtensionPoint<MemoryStream>(subject);
		}

		public static StringExtensions.IOExtensionPoint<string> IO(this string subject)
		{
			return new StringExtensions.IOExtensionPoint<string>(subject);
		}

		public static bool IsAbsolute(this StringExtensions.VirtualPathExtensionPoint virtualPath)
		{
			return VirtualPathUtility.IsAbsolute(virtualPath.ExtendedValue);
		}

		public static bool IsAppRelative(this StringExtensions.VirtualPathExtensionPoint virtualPath)
		{
			return VirtualPathUtility.IsAppRelative(virtualPath.ExtendedValue);
		}

		public static bool IsEmpty(this string str)
		{
			return string.IsNullOrWhiteSpace(str);
		}

		public static bool IsNotEmpty(this string str)
		{
			return !str.IsEmpty();
		}

		public static string Left(this StringExtensions.SubstrExtensionPoint sp, int length)
		{
			int num = length;
			string extendedValue = sp.ExtendedValue;
			num = Math.Max(num, 0);
			return extendedValue.Safe<string, string>((string s) => {
				if (s.Length <= num)
				{
					return s;
				}
				return s.Substring(0, num);
			}, null);
		}

		public static string LeftFromFirst(this StringExtensions.SubstrExtensionPoint sp, string substring, StringComparison comparison = StringComparison.Ordinal)
		{
			string extendedValue = sp.ExtendedValue;
			return extendedValue.Safe<string, string>((string s) => {
				substring = substring.EmptyIfNull();
				int num = (s.IndexOf(substring, comparison) >= 0 ? s.IndexOf(substring, comparison) : -1);
				if (num < 0)
				{
					return null;
				}
				return sp.Left(num);
			}, null);
		}

		public static string LeftFromLast(this StringExtensions.SubstrExtensionPoint sp, string substring, StringComparison comparison = StringComparison.Ordinal)
		{
			string extendedValue = sp.ExtendedValue;
			return extendedValue.Safe<string, string>((string s) => {
				substring = substring.EmptyIfNull();
				int num = -1;
				if (substring.IsEmpty())
				{
					num = 0;
				}
				else if (s.LastIndexOf(substring, comparison) >= 0)
				{
					num = s.LastIndexOf(substring, comparison);
				}
				if (num < 0)
				{
					return null;
				}
				return sp.Left(num);
			}, null);
		}

		public static string MakeRelativeTo(this StringExtensions.VirtualPathExtensionPoint fromPath, string toPath)
		{
			return VirtualPathUtility.MakeRelative(fromPath.ExtendedValue, toPath);
		}

		public static string NullIfEmpty(this string s)
		{
			if (!s.IsEmpty())
			{
				return s;
			}
			return null;
		}

		public static T Parse<T>(this string s)
		{
			T t = default(T);
			if (s.IsNotEmpty())
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
				t = (T)converter.ConvertFrom(s);
			}
			return t;
		}

		public static ExtensionPoint<string> Parse(this string subject)
		{
			return new ExtensionPoint<string>(subject);
		}

		public static string Prepend(this StringExtensions.IfNotThereExtensionPoint sp, string prefix)
		{
			string extendedValue = sp.ExtendedValue;
			if (extendedValue == null && prefix == null)
			{
				return null;
			}
			return StringExtensions.prependIfNotThere(extendedValue.EmptyIfNull(), prefix.EmptyIfNull());
		}

		private static string prependIfNotThere(string str, string prefix)
		{
			if (str.StartsWith(prefix))
			{
				return str;
			}
			return string.Concat(prefix, str);
		}

		public static string Right(this StringExtensions.SubstrExtensionPoint ep, int length)
		{
			int num = length;
			string extendedValue = ep.ExtendedValue;
			num = Math.Max(num, 0);
			return extendedValue.Safe<string, string>((string s) => {
				if (s.Length <= num)
				{
					return s;
				}
				return s.Substring(s.Length - num, num);
			}, null);
		}

		public static string RightFromFirst(this StringExtensions.SubstrExtensionPoint sp, string substring, StringComparison comparison = StringComparison.Ordinal)
		{
			string extendedValue = sp.ExtendedValue;
			return extendedValue.Safe<string, string>((string s) => {
				substring = substring.EmptyIfNull();
				int num = (s.IndexOf(substring, comparison) >= 0 ? s.IndexOf(substring, comparison) + substring.Length : -1);
				if (num < 0)
				{
					return null;
				}
				return sp.Right(s.Length - num);
			}, null);
		}

		public static string RightFromLast(this StringExtensions.SubstrExtensionPoint sp, string substring, StringComparison comparison = StringComparison.Ordinal)
		{
			string extendedValue = sp.ExtendedValue;
			return extendedValue.Safe<string, string>((string s) => {
				substring = substring.EmptyIfNull();
				int length = -1;
				if (substring.IsEmpty())
				{
					length = s.Length;
				}
				else if (s.LastIndexOf(substring, comparison) >= 0)
				{
					length = s.LastIndexOf(substring, comparison) + substring.Length;
				}
				if (length < 0)
				{
					return null;
				}
				return sp.Right(s.Length - length);
			}, null);
		}

		public static string Separate(this string ss, uint size, string separator)
		{
			Guard.AgainstArgument<ArgumentOutOfRangeException>("size", size == 0);
			return ss.Safe<string, string>((string input) => Regex.Replace(input, "(.{{1,{0}}})".FormatWith(new object[] { size }), (System.Text.RegularExpressions.Match m) => string.Concat(m.Value, (m.NextMatch().Success ? separator : string.Empty))), null);
		}

		public static string Strip(this string s, params char[] chars)
		{
			return s.Safe<string, string>((string s1) => {
				string str = s1;
				if (chars != null)
				{
					char[] chrArray = chars;
					for (int i = 0; i < (int)chrArray.Length; i++)
					{
						str = str.Replace(chrArray[i].ToString(CultureInfo.CurrentCulture), string.Empty);
					}
				}
				return str;
			}, null);
		}

		public static string Strip(this string s, string subString)
		{
			return s.Safe<string, string>((string s1) => s1.Replace(subString, string.Empty), null);
		}

		public static string StripHtmlTags(this string input)
		{
			return input.Safe<string, string>((string s1) => StringExtensions._strippingRegEx.Replace(s1, string.Empty), null);
		}

		public static StringExtensions.SubstrExtensionPoint Substr(this string subject)
		{
			return new StringExtensions.SubstrExtensionPoint(subject);
		}

		public static string ToAbsolute(this StringExtensions.VirtualPathExtensionPoint virtualPath)
		{
			return VirtualPathUtility.ToAbsolute(virtualPath.ExtendedValue);
		}

		public static string ToAbsolute(this StringExtensions.VirtualPathExtensionPoint virtualPath, string applicationPath)
		{
			return VirtualPathUtility.ToAbsolute(virtualPath.ExtendedValue, applicationPath);
		}

		public static string ToAppRelative(this StringExtensions.VirtualPathExtensionPoint virtualPath, string applicationPath)
		{
			return VirtualPathUtility.ToAppRelative(virtualPath.ExtendedValue, applicationPath);
		}

		public static bool TryAsNullable<T>(this ExtensionPoint<string> s, out Nullable<T> result, TryParseDelegate<T> parser)
		where T : struct
		{
			bool flag;
			T t = default(T);
			if (!s.ExtendedValue.IsEmpty())
			{
				flag = parser(s.ExtendedValue, out t);
				if (!flag)
				{
					result = null;
				}
				else
				{
					result = new Nullable<T>(t);
				}
			}
			else
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		public static string UrlDecode(this StringExtensions.HttpExtensionPoint str)
		{
			return HttpUtility.UrlDecode(str.ExtendedValue);
		}

		public static string UrlDecode(this StringExtensions.HttpExtensionPoint str, Encoding e)
		{
			return HttpUtility.UrlDecode(str.ExtendedValue, e);
		}

		public static string UrlEncode(this StringExtensions.HttpExtensionPoint str)
		{
			return HttpUtility.UrlEncode(str.ExtendedValue);
		}

		public static string UrlEncode(this StringExtensions.HttpExtensionPoint str, Encoding e)
		{
			return HttpUtility.UrlEncode(str.ExtendedValue, e);
		}

		public static StringExtensions.VirtualPathExtensionPoint VirtualPath(this string subject)
		{
			return new StringExtensions.VirtualPathExtensionPoint(subject);
		}

		public class HttpExtensionPoint : ExtensionPoint<string>
		{
			public HttpExtensionPoint(string value) : base(value)
			{
			}
		}

		public class IfNotThereExtensionPoint : ExtensionPoint<string>
		{
			public IfNotThereExtensionPoint(string value) : base(value)
			{
			}
		}

		public class IOExtensionPoint<T> : ExtensionPoint<T>
		{
			public IOExtensionPoint(T value) : base(value)
			{
			}
		}

		public class SubstrExtensionPoint : ExtensionPoint<string>
		{
			public SubstrExtensionPoint(string value) : base(value)
			{
			}
		}

		public class VirtualPathExtensionPoint : ExtensionPoint<string>
		{
			public VirtualPathExtensionPoint(string value) : base(value)
			{
			}
		}
	}
}