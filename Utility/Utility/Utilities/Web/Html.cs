using System;
using System.Runtime.CompilerServices;
using System.Web.UI;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Web
{
	public static class Html
	{
		public static string Write(this HtmlTextWriterTag tag)
		{
			Enumeration.AssertDefined<HtmlTextWriterTag>(tag);
			return tag.ToString().ToLowerInvariant();
		}

		public static string Write(this HtmlTextWriterAttribute attr)
		{
			Enumeration.AssertDefined<HtmlTextWriterAttribute>(attr);
			return attr.ToString().ToLowerInvariant();
		}
	}
}