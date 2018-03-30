using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4
{
	public struct Percentage : IFormattable
	{
		private FormattablePercentage<double>? _formattable;

		private FormattablePercentage<double> Formattable
		{
			get
			{
				FormattablePercentage<double>? nullable = this._formattable;
				this._formattable = new FormattablePercentage<double>?((nullable.HasValue ? nullable.GetValueOrDefault() : new FormattablePercentage<double>(this.Value)));
				return this._formattable.Value;
			}
		}

		public double Fraction
		{
			get;
			private set;
		}

		public double Value
		{
			get;
			private set;
		}

		public Percentage(double value)
		{
			this = new Percentage()
			{
				Value = value,
				Fraction = value / 100,
				_formattable = new FormattablePercentage<double>?(new FormattablePercentage<double>(value))
			};
		}

		public double Apply(long given)
		{
			return this.Fraction * (double)given;
		}

		public double Apply(double given)
		{
			return this.Fraction * given;
		}

		public double DeductFrom(double amountIncludingPercentage)
		{
			return amountIncludingPercentage / (1 + this.Fraction);
		}

		public static Percentage FromAmounts(long given, long total)
		{
			Guard.AgainstArgument("total", total == (long)0, FormattablePercentage<double>.DivideByZeroMessage, new string[0]);
			return new Percentage((double)given / (double)total * 100);
		}

		public static Percentage FromAmounts(double given, double total)
		{
			Guard.AgainstArgument("total", total == 0, FormattablePercentage<double>.DivideByZeroMessage, new string[0]);
			return new Percentage(given / total * 100);
		}

		public static Percentage FromDifference(long total, long given)
		{
			Guard.AgainstArgument("total", total == (long)0, FormattablePercentage<double>.DivideByZeroMessage, new string[0]);
			return new Percentage((double)(total - given) / (double)total * 100);
		}

		public static Percentage FromDifference(double total, double given)
		{
			Guard.AgainstArgument("total", total == 0, FormattablePercentage<double>.DivideByZeroMessage, new string[0]);
			return new Percentage((total - given) / total * 100);
		}

		public static Percentage FromFraction(double fraction)
		{
			return new Percentage(fraction * 100);
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