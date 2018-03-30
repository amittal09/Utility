using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Comparisons
{
	public static class Hasher
	{
		public static int Canonical(params object[] args)
		{
			int? nullable;
			int? nullable1;
			int? nullable2 = null;
			Func<object, int> func = (object o) => {
				if (o == null)
				{
					return 0;
				}
				return o.GetHashCode();
			};
			for (int i = 0; i < (int)args.Length; i++)
			{
				if (nullable2.HasValue)
				{
					int? nullable3 = nullable2;
					if (nullable3.HasValue)
					{
						nullable = new int?(nullable3.GetValueOrDefault() * 397);
					}
					else
					{
						nullable = null;
					}
					int? nullable4 = nullable;
					int num = func(args[i]);
					if (nullable4.HasValue)
					{
						nullable1 = new int?(nullable4.GetValueOrDefault() ^ num);
					}
					else
					{
						nullable1 = null;
					}
					nullable2 = nullable1;
				}
				else
				{
					nullable2 = new int?(func(args[i]));
				}
			}
			return nullable2.GetValueOrDefault();
		}

		public static int Default<T>(T t)
		{
			if (t == null)
			{
				return 0;
			}
			return t.GetHashCode();
		}

		public static int Zero<T>(T t)
		{
			return 0;
		}
	}
}