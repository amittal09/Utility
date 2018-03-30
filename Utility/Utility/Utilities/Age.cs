using System;
using System.Globalization;
using System.Text;
using Vertica.Utilities_v4.Extensions.StringExt;

namespace Vertica.Utilities_v4
{
	public struct Age : IFormattable, IEquatable<Age>, IComparable, IComparable<Age>, IComparable<TimeSpan>
	{
		public readonly static Age Empty;

		private readonly int _years;

		private readonly int _months;

		private readonly int _weeks;

		private readonly int _days;

		private readonly DateTime _advent;

		private readonly DateTime _terminus;

		public DateTime Advent
		{
			get
			{
				return this._advent;
			}
		}

		public int Days
		{
			get
			{
				return this._days;
			}
		}

		public TimeSpan Elapsed
		{
			get
			{
				return new TimeSpan(this._terminus.Ticks - this._advent.Ticks);
			}
		}

		public bool IsEmpty
		{
			get
			{
				if (this._advent != DateTime.MinValue)
				{
					return false;
				}
				return this._terminus == DateTime.MinValue;
			}
		}

		public int Months
		{
			get
			{
				return this._months;
			}
		}

		public DateTime Terminus
		{
			get
			{
				return this._terminus;
			}
		}

		public int Weeks
		{
			get
			{
				return this._weeks;
			}
		}

		public int Years
		{
			get
			{
				return this._years;
			}
		}

		static Age()
		{
			Age.Empty = new Age(DateTime.MinValue, DateTime.MinValue);
		}

		public Age(DateTime advent) : this(advent, Age.getNowFromKind(advent))
		{
		}

		public Age(DateTime advent, DateTime terminus)
		{
			this._advent = advent;
			this._terminus = terminus;
			TimeSpan timeOfDay = advent.TimeOfDay;
			if (timeOfDay > TimeSpan.Zero)
			{
				advent = advent.Subtract(timeOfDay);
				terminus = terminus.Subtract(timeOfDay);
			}
			this._years = terminus.Year - advent.Year;
			this._months = terminus.Month - advent.Month;
			this._days = terminus.Day - advent.Day;
			this._weeks = 0;
			if (this._days < 0)
			{
				this._months--;
			}
			while (this._months < 0)
			{
				this._months += 12;
				this._years--;
			}
			DateTime dateTime = advent.AddYears(this._years);
			TimeSpan timeSpan = terminus - dateTime.AddMonths(this._months);
			this._days = (int)Math.Floor(timeSpan.TotalDays);
			if (this._days > 0)
			{
				this._weeks = this._days / 7;
				this._days %= 7;
			}
		}

		public int CompareTo(object obj)
		{
			if (obj is Age)
			{
				return this.CompareTo((Age)obj);
			}
			if (!(obj is TimeSpan))
			{
				throw new ArgumentException(string.Format("The object '{0}' is of the wrong type for comparison.", obj.GetType()), "obj");
			}
			return this.CompareTo((TimeSpan)obj);
		}

		public int CompareTo(Age other)
		{
			return this.CompareTo(other.Elapsed);
		}

		public int CompareTo(TimeSpan other)
		{
			return this.Elapsed.CompareTo(other);
		}

		public bool Equals(Age other)
		{
			if (!this._advent.Equals(other._advent))
			{
				return false;
			}
			return this._terminus.Equals(other._terminus);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (!(obj is Age))
			{
				return false;
			}
			return this.Equals((Age)obj);
		}

		public override int GetHashCode()
		{
			DateTime dateTime = this._advent;
			DateTime dateTime1 = this._terminus;
			return dateTime.GetHashCode() * 397 ^ dateTime1.GetHashCode();
		}

		private static DateTime getNowFromKind(DateTime time)
		{
			if (time.Kind != DateTimeKind.Utc)
			{
				return Time.Now.DateTime;
			}
			return Time.UtcNow.DateTime;
		}

		public static bool operator ==(Age left, Age right)
		{
			return left.Equals(right);
		}

		public static bool operator >(Age x, Age y)
		{
			return x.Elapsed > y.Elapsed;
		}

		public static bool operator >=(Age x, Age y)
		{
			return x.Elapsed >= y.Elapsed;
		}

		public static bool operator !=(Age left, Age right)
		{
			return !left.Equals(right);
		}

		public static bool operator <(Age x, Age y)
		{
			return x.Elapsed < y.Elapsed;
		}

		public static bool operator <=(Age x, Age y)
		{
			return x.Elapsed <= y.Elapsed;
		}

		private static string plural(int number)
		{
			if (number != 1)
			{
				return "s";
			}
			return string.Empty;
		}

		public override string ToString()
		{
			return this.ToString(0, true);
		}

		public string ToString(int significantPlaces)
		{
			return this.ToString(significantPlaces, true);
		}

		public string ToString(int significantPlaces, bool includeTime)
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			int num = (significantPlaces < 1 ? 10 : significantPlaces);
			int num1 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			if (this._years > 0 && num1 < num)
			{
				stringBuilder.AppendFormat(" {0} year{1}", this._years, Age.plural(this._years));
				num1++;
			}
			if (this._months > 0 && num1 < num)
			{
				stringBuilder.AppendFormat(" {0} month{1}", this._months, Age.plural(this._months));
				num1++;
			}
			if (this._weeks > 0 && num1 < num)
			{
				stringBuilder.AppendFormat(" {0} week{1}", this._weeks, Age.plural(this._weeks));
				num1++;
			}
			if (this._days > 0 && num1 < num)
			{
				stringBuilder.AppendFormat(" {0} day{1}", this._days, Age.plural(this._days));
				num1++;
			}
			if (includeTime)
			{
				TimeSpan elapsed = this.Elapsed;
				if (elapsed.Hours != 0 && num1 < num)
				{
					stringBuilder.AppendFormat(" {0} hour{1}", elapsed.Hours, Age.plural(elapsed.Hours));
					num1++;
				}
				if (elapsed.Minutes != 0 && num1 < num)
				{
					stringBuilder.AppendFormat(" {0} minute{1}", elapsed.Minutes, Age.plural(elapsed.Minutes));
					num1++;
				}
				if (elapsed.Seconds != 0 && num1 < num)
				{
					stringBuilder.AppendFormat(" {0} second{1}", elapsed.Seconds, Age.plural(elapsed.Seconds));
					num1++;
				}
			}
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString().Trim();
			}
			if (!includeTime)
			{
				return "less than a day";
			}
			return "less than a second";
		}

		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.InvariantCulture);
		}

		public string ToString(string format, IFormatProvider provider)
		{
			if (format.IsEmpty())
			{
				format = "g";
			}
			char chr = format[0];
			if (char.ToLower(chr) != 'g')
			{
				if (!char.IsDigit(chr))
				{
					throw new FormatException(string.Concat("Could not parse the Age format: ", format));
				}
				int num = int.Parse(chr.ToString(provider));
				return this.ToString(num);
			}
			int num1 = 0;
			if (format.Length > 1 && char.IsDigit(format[1]))
			{
				char chr1 = format[1];
				num1 = int.Parse(chr1.ToString(provider));
			}
			return this.ToString(num1);
		}
	}
}