using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Collections
{
	public static class Chain
	{
		public static IEnumerable<T> Empty<T>()
		{
			return Enumerable.Empty<T>();
		}

		public static IEnumerable<T> From<T>(params T[] items)
		{
			return items;
		}

		public static IEnumerable<T> Null<T>()
		{
			return null;
		}

		public static IEnumerable<T> Of<T>(T element)
		{
			while (true)
			{
				yield return element;
			}
		}
	}
}