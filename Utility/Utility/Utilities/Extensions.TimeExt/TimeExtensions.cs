using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Extensions.ComparableExt;

namespace Vertica.Utilities_v4.Extensions.TimeExt
{
	public static class TimeExtensions
	{
		private readonly static DayOfWeek MinDay;

		private readonly static DayOfWeek MaxDay;

		private readonly static string[] NAMES;

		private readonly static Func<int, string, string> _tense;

		static TimeExtensions()
		{
			TimeExtensions.MinDay = DayOfWeek.Sunday;
			TimeExtensions.MaxDay = DayOfWeek.Saturday;
			string[] strArrays = new string[] { "day", "hour", "minute", "second" };
			TimeExtensions.NAMES = strArrays;
			TimeExtensions._tense = (int q, string n) => {
				if (q == 1)
				{
					return string.Concat("1 ", n);
				}
				return string.Format("{0} {1}s", q, n);
			};
		}

		public static DateTimeOffset Ago(this TimeSpan timeSpan)
		{
			return Time.Now - timeSpan;
		}

		public static string AsLapseDescription(this TimeSpan ts)
		{
			return string.Concat(ts.Describe(), " ago");
		}

		public static DateTimeOffset AsOffset(this DateTime dt, TimeSpan offset)
		{
			return new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, offset);
		}

		public static DateTimeOffset AsUtcOffset(this DateTime dt)
		{
			return dt.AsOffset(TimeSpan.Zero);
		}

		public static DateTimeOffset BeginningOf(this DateTimeOffset dt, Period period)
		{
			DateTimeOffset dateTimeOffset;
			switch (period)
			{
				case Period.Week:
				{
					dateTimeOffset = dt.BeginningOfWeek();
					break;
				}
				case Period.Month:
				{
					dateTimeOffset = dt.BeginningOfMonth();
					break;
				}
				case Period.Year:
				{
					dateTimeOffset = dt.BeginningOfYear();
					break;
				}
				default:
				{
					throw new ArgumentOutOfRangeException("period");
				}
			}
			return dateTimeOffset;
		}

		public static DateTimeOffset BeginningOfDay(this DateTimeOffset dt)
		{
			return dt.SetTime(Time.MidNight);
		}

		public static DateTimeOffset BeginningOfMonth(this DateTimeOffset dt)
		{
			return new DateTimeOffset(dt.Year, dt.Month, 1, 0, 0, 0, dt.Offset);
		}

		public static DateTimeOffset BeginningOfWeek(this DateTimeOffset dt)
		{
			return (dt - TimeExtensions.days(dt.DayOfWeek.DaysSince(Time.FirstDayOfWeek()))).BeginningOfDay();
		}

		public static DateTimeOffset BeginningOfYear(this DateTimeOffset dt)
		{
			return new DateTimeOffset(dt.Year, 1, 1, 0, 0, 0, dt.Offset);
		}

		public static void CheckRange(this DayOfWeek dow)
		{
			if (!dow.InRange())
			{
				string[] str = new string[] { dow.ToString(), TimeExtensions.MinDay.ToString(), TimeExtensions.MaxDay.ToString() };
				ExceptionHelper.ThrowArgumentException<ArgumentOutOfRangeException>("dow", "{0} has to be between {1} and {2}.", str);
			}
		}

		private static TimeSpan days(int days)
		{
			return TimeSpan.FromDays((double)days);
		}

		private static int daysBetween(int first, int second)
		{
			if (first > second)
			{
				return first - second;
			}
			return (int)TimeExtensions.MaxDay - (second - first) + (int)DayOfWeek.Monday;
		}

		public static int DaysSince(this DayOfWeek dt, DayOfWeek prevDoW)
		{
			dt.CheckRange();
			prevDoW.CheckRange();
			return TimeExtensions.daysBetween((int)dt, (int)prevDoW);
		}

		public static int DaysTill(this DayOfWeek dt, DayOfWeek nextDoW)
		{
			dt.CheckRange();
			nextDoW.CheckRange();
			return TimeExtensions.daysBetween((int)nextDoW, (int)dt);
		}

		public static string Describe(this TimeSpan ts)
		{
			int[] days = new int[] { ts.Days, ts.Hours, ts.Minutes, ts.Seconds };
			int[] numArray = days;
			double[] totalDays = new double[] { ts.TotalDays, ts.TotalHours, ts.TotalMinutes, ts.TotalSeconds };
			double[] numArray1 = totalDays;
			var variable = ((IEnumerable<int>)numArray).Select((int value, int index) => new { @value = value, index = index }).FirstOrDefault((x) => x.@value != 0);
			if (variable == null)
			{
				return "now";
			}
			int num = variable.index;
			string str = (num >= 3 ? string.Empty : "about ");
			int num1 = (int)Math.Round(numArray1[num]);
			return string.Concat(str, TimeExtensions._tense(num1, TimeExtensions.NAMES[num]));
		}

		public static TimeSpan Difference(this DateTimeOffset dt1, DateTimeOffset dt2)
		{
			return dt1.Subtract(dt2).Duration();
		}

		public static TimeExtensions.DifferFromBuilder DiffersFrom(this DateTimeOffset dt1, DateTimeOffset dt2)
		{
			return new TimeExtensions.DifferFromBuilder(dt1, dt2);
		}

		public static TimeSpan Elapsed(this DateTimeOffset time)
		{
			return Time.Now.Subtract(time).Duration();
		}

		public static DateTimeOffset EndOf(this DateTimeOffset dt, Period period)
		{
			DateTimeOffset dateTimeOffset;
			switch (period)
			{
				case Period.Week:
				{
					dateTimeOffset = dt.EndOfWeek();
					break;
				}
				case Period.Month:
				{
					dateTimeOffset = dt.EndOfMonth();
					break;
				}
				case Period.Year:
				{
					dateTimeOffset = dt.EndOfYear();
					break;
				}
				default:
				{
					throw new ArgumentOutOfRangeException("period");
				}
			}
			return dateTimeOffset;
		}

		private static DateTimeOffset endOfDay(this DateTimeOffset dt)
		{
			return new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, dt.Offset) + Time.EndOfDay;
		}

		public static DateTimeOffset EndOfMonth(this DateTimeOffset dt)
		{
			return (new DateTimeOffset(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), 0, 0, 0, dt.Offset)).endOfDay();
		}

		public static DateTimeOffset EndOfWeek(this DateTimeOffset dt)
		{
			return (dt + TimeExtensions.days(dt.DayOfWeek.DaysTill(Time.LastDayOfWeek()))).endOfDay();
		}

		public static DateTimeOffset EndOfYear(this DateTimeOffset dt)
		{
			return (new DateTimeOffset(dt.Year, 12, 31, 0, 0, 0, dt.Offset)).endOfDay();
		}

		public static DateTimeOffset FromNow(this TimeSpan timeSpan)
		{
			return Time.Now + timeSpan;
		}

		public static bool InRange(this DayOfWeek doW)
		{
			if (doW < TimeExtensions.MinDay)
			{
				return false;
			}
			return doW <= TimeExtensions.MaxDay;
		}

		public static bool IsLeapYear(this int year)
		{
			return year.IsLeapYear(CultureInfo.CurrentCulture);
		}

		public static bool IsLeapYear(this int year, CultureInfo culture)
		{
			return culture.Calendar.IsLeapYear(year);
		}

		public static DateTimeOffset Next(this DateTimeOffset dt, DayOfWeek nextDoW)
		{
			return dt + TimeExtensions.days(dt.DayOfWeek.DaysTill(nextDoW));
		}

		public static DateTimeOffset Previous(this DateTimeOffset dt, DayOfWeek nextDoW)
		{
			return dt - TimeExtensions.days(dt.DayOfWeek.DaysSince(nextDoW));
		}

		public static DateTimeOffset SetTime(this DateTimeOffset dt, int hour, int minute, int second, int miliSecond)
		{
			return new DateTimeOffset(dt.Year, dt.Month, dt.Day, hour, minute, second, miliSecond, dt.Offset);
		}

		public static DateTimeOffset SetTime(this DateTimeOffset dt, int hour, int minute, int second)
		{
			return new DateTimeOffset(dt.Year, dt.Month, dt.Day, hour, minute, second, dt.Offset);
		}

		public static DateTimeOffset SetTime(this DateTimeOffset dt, TimeSpan span)
		{
			return dt.SetTime(span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}

		public static DateTimeOffset Tomorrow(this DateTimeOffset dt)
		{
			return dt + Time.OneDay;
		}

		public static double ToUnixTime(this DateTimeOffset dt)
		{
			return Time.ToUnixTime(dt);
		}

		public static DateTimeOffset UtcAgo(this TimeSpan timeSpan)
		{
			return Time.UtcNow - timeSpan;
		}

		public static TimeSpan UtcElapsed(this DateTimeOffset time)
		{
			return Time.UtcNow.Subtract(time).Duration();
		}

		public static DateTimeOffset UtcFromNow(this TimeSpan timeSpan)
		{
			return Time.UtcNow + timeSpan;
		}

		public static int Week(this DateTimeOffset dt)
		{
			return dt.Week(CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule);
		}

		public static int Week(this DateTimeOffset dt, CalendarWeekRule rule)
		{
			return dt.Week(rule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
		}

		public static int Week(this DateTimeOffset dt, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt.DateTime, rule, firstDayOfWeek);
		}

		public static DateTimeOffset Yesterday(this DateTimeOffset dt)
		{
			return dt - Time.OneDay;
		}

		public class DifferFromBuilder
		{
			private readonly DateTimeOffset _dt1;

			private readonly DateTimeOffset _dt2;

			internal DifferFromBuilder(DateTimeOffset dt1, DateTimeOffset dt2)
			{
				this._dt1 = dt1;
				this._dt2 = dt2;
			}

			public bool In(TimeSpan ts)
			{
				return this._dt1.Difference(this._dt2).IsEqualTo<TimeSpan>(ts);
			}

			public bool InAtLeast(TimeSpan ts)
			{
				return this._dt1.Difference(this._dt2).IsAtLeast<TimeSpan>(ts);
			}

			public bool InAtMost(TimeSpan ts)
			{
				return this._dt1.Difference(this._dt2).IsAtMost<TimeSpan>(ts);
			}

			public bool InLessThan(TimeSpan ts)
			{
				return this._dt1.Difference(this._dt2).IsLessThan<TimeSpan>(ts);
			}

			public bool InMoreThan(TimeSpan ts)
			{
				return this._dt1.Difference(this._dt2).IsMoreThan<TimeSpan>(ts);
			}

			public bool InNothing()
			{
				return this._dt1.Difference(this._dt2).IsEqualTo<TimeSpan>(TimeSpan.Zero);
			}

			public bool InSomething()
			{
				return this._dt1.Difference(this._dt2).IsDifferentFrom<TimeSpan>(TimeSpan.Zero);
			}
		}
	}
}