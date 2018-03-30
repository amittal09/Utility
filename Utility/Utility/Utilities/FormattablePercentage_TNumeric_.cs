using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4
{
	internal struct FormattablePercentage<TNumeric>
	where TNumeric : IFormattable, IComparable, IConvertible, IComparable<TNumeric>, IEquatable<TNumeric>
	{
		internal readonly static string DivideByZeroMessage;

		public TNumeric Value
		{
			get;
			private set;
		}

		static FormattablePercentage()
		{
			FormattablePercentage<TNumeric>.DivideByZeroMessage = (new DivideByZeroException()).Message;
		}

		public FormattablePercentage(TNumeric value)
		{
			this = new FormattablePercentage<TNumeric>()
			{
				Value = value
			};
		}

		private string doFormat(TNumeric percentage, string numberFormat, IFormatProvider provider)
		{
			object[] objArray = new object[] { percentage };
			return string.Format(provider, numberFormat, objArray);
		}

		public override string ToString()
		{
			return this.ToString("{0}", CultureInfo.InvariantCulture);
		}

		public string ToString(string numberFormat)
		{
			return this.doFormat(this.Value, string.Concat(numberFormat, " %"), CultureInfo.InvariantCulture);
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return this.doFormat(this.Value, string.Concat(format, " %"), formatProvider);
		}
	}
}