using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.DirectoryServices
{
	public static class EnumExtensions
	{
		public static string ToAd(this AdObjectCategory category)
		{
			return category.ToString();
		}

		public static string ToAd(this AdEntryMethod method)
		{
			return method.ToString();
		}

		public static string ToAd(this AdProperty property)
		{
			return property.ToString();
		}
	}
}