using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4
{
	public struct DecimalPercentage
	{
		private FormattablePercentage<decimal>? _formattable;

		private FormattablePercentage<decimal> Formattable
		{
			get
			{
				FormattablePercentage<decimal>? nullable = this._formattable;
				this._formattable = new FormattablePercentage<decimal>?((nullable.HasValue ? nullable.GetValueOrDefault() : new FormattablePercentage<decimal>(this.Value)));
				return this._formattable.Value;
			}
		}

		public decimal Fraction
		{
			get;
			private set;
		}

		public decimal Value
		{
			get;
			private set;
		}

		public DecimalPercentage(decimal value)
		{
			this = new DecimalPercentage()
			{
				Value = value,
				Fraction = value / new decimal(100)
			};
		}

		public decimal Apply(long given)
		{
			return this.Fraction * given;
		}

		public decimal Apply(decimal given)
		{
			return this.Fraction * given;
		}

		public decimal DeductFrom(decimal amountIncludingPercentage)
		{
			return amountIncludingPercentage / this.Fraction++;
		}

		public static DecimalPercentage FromAmounts(long given, long total)
		{
			Guard.AgainstArgument("total", total == (long)0, FormattablePercentage<decimal>.DivideByZeroMessage, new string[0]);
			return new DecimalPercentage((given / total) * new decimal(100));
		}

		public static DecimalPercentage FromAmounts(decimal given, decimal total)
		{
			Guard.AgainstArgument("total", total == new decimal(0), FormattablePercentage<decimal>.DivideByZeroMessage, new string[0]);
			return new DecimalPercentage((given / total) * new decimal(100));
		}

		public static DecimalPercentage FromDifference(long total, long given)
		{
			Guard.AgainstArgument("total", total == (long)0, FormattablePercentage<decimal>.DivideByZeroMessage, new string[0]);
			return new DecimalPercentage(((total - given) / total) * new decimal(100));
		}

		public static DecimalPercentage FromDifference(decimal total, decimal given)
		{
			Guard.AgainstArgument("total", total == new decimal(0), FormattablePercentage<decimal>.DivideByZeroMessage, new string[0]);
			return new DecimalPercentage(((total - given) / total) * new decimal(100));
		}

		public static DecimalPercentage FromFraction(decimal fraction)
		{
			return new DecimalPercentage(fraction * new decimal(100));
		}

		public override string ToString()
		{
			return this.Formattable.ToString();
		}

		public string ToString(string numberFormat)
		{
			return this.Formattable.ToString(numberFormat);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this.Formattable.ToString(format, formatProvider);
		}
	}
}