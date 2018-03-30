using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Web
{
	public static class QueryStringHelper
	{
		private const string EQ = "=";

		private const string AMP = "&";

		private const string Q = "?";

		private static string buildString(NameValueCollection collection, Func<string, string> valueEncoding, Action<StringBuilder> prependAction)
		{
			Guard.AgainstNullArgument("collection", collection);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string key in collection.Keys)
			{
				string item = collection[key];
				if (key == null)
				{
					continue;
				}
				stringBuilder.Append(valueEncoding(key));
				stringBuilder.Append("=");
				stringBuilder.Append(valueEncoding(item));
				stringBuilder.Append("&");
			}
			if (stringBuilder.Length > 0)
			{
				prependAction(stringBuilder);
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		public static NameValueCollection QueryString(this Uri uri)
		{
			return HttpUtility.ParseQueryString(HttpUtility.UrlDecode(uri.Query));
		}

		public static string ToDecodedQuery(this NameValueCollection collection)
		{
			return QueryStringHelper.buildString(collection, QueryStringHelper.ValueEncoding.None, QueryStringHelper.Prepend.Nothing);
		}

		public static string ToDecodedQueryString(this NameValueCollection collection)
		{
			return QueryStringHelper.buildString(collection, QueryStringHelper.ValueEncoding.None, QueryStringHelper.Prepend.Q);
		}

		public static string ToQuery(this NameValueCollection collection)
		{
			return QueryStringHelper.buildString(collection, QueryStringHelper.ValueEncoding.Url, QueryStringHelper.Prepend.Nothing);
		}

		public static string ToQueryString(this NameValueCollection collection)
		{
			return QueryStringHelper.buildString(collection, QueryStringHelper.ValueEncoding.Url, QueryStringHelper.Prepend.Q);
		}

		private static class Prepend
		{
			internal readonly static Action<StringBuilder> Nothing;

			internal readonly static Action<StringBuilder> Q;

			static Prepend()
			{
				QueryStringHelper.Prepend.Nothing = (StringBuilder _) => {
				};
				QueryStringHelper.Prepend.Q = (StringBuilder sb) => sb.Insert(0, "?");
			}
		}

		private static class ValueEncoding
		{
			internal readonly static Func<string, string> None;

			internal readonly static Func<string, string> Url;

			static ValueEncoding()
			{
				QueryStringHelper.ValueEncoding.None = (string _) => _;
				QueryStringHelper.ValueEncoding.Url = (string str) => HttpUtility.UrlEncode(str);
			}
		}
	}
}