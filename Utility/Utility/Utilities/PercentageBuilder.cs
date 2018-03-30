using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4
{
	public static class PercentageBuilder
	{
		public static Percentage AsPercentOf(this double given, double total)
		{
			return Percentage.FromAmounts(given, total);
		}

		public static DecimalPercentage AsPercentOf(this decimal given, decimal total)
		{
			return DecimalPercentage.FromAmounts(given, total);
		}

		public static Percentage Percent(this double value)
		{
			return new Percentage(value);
		}

		public static DecimalPercentage Percent(this decimal value)
		{
			return new DecimalPercentage(value);
		}
	}
}