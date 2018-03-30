using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4
{
	public static class ClassMapper
	{
		public static IEnumerable<TTo> MapIfNotNull<TFrom, TTo>(IEnumerable<TFrom> from, Func<TFrom, TTo> doMapping)
		where TFrom : class
		where TTo : class
		{
			if (from != null)
			{
				foreach (TFrom tFrom in from)
				{
					TTo tTo = ClassMapper.MapIfNotNull<TFrom, TTo>(tFrom, doMapping);
					if (tTo == null)
					{
						continue;
					}
					yield return tTo;
				}
			}
		}

		public static TTo MapIfNotNull<TFrom, TTo>(TFrom from, Func<TFrom, TTo> doMapping)
		where TFrom : class
		where TTo : class
		{
			return ClassMapper.MapIfNotNull<TFrom, TTo>(from, doMapping, default(TTo));
		}

		public static TTo MapIfNotNull<TFrom, TTo>(TFrom from, Func<TFrom, TTo> doMapping, TTo defaultTo)
		where TFrom : class
		where TTo : class
		{
			TTo tTo = defaultTo;
			if (from != null)
			{
				tTo = doMapping(from);
			}
			return tTo;
		}
	}
}