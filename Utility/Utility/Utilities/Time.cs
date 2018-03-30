using System;
using System.Diagnostics;
using System.Globalization;
using Vertica.Utilities_v4.Extensions.TimeExt;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4
{
	public static class Time
	{
		private static DateTimeOffset? _now;

		private static DateTimeOffset? _utcNow;

		public static TimeSpan Noon;

		public static TimeSpan MidNight;

		public static TimeSpan EndOfDay;

		public static TimeSpan BeginningOfDay;

		public static TimeSpan OneDay;

		public static TimeSpan OneHour;

		public static TimeSpan OneMinute;

		public static TimeSpan OneSecond;

		public static TimeSpan OneWeek;

		public static DateTimeOffset Now
		{
			get
			{
				DateTimeOffset? nullable = Time._now;
				if (!nullable.HasValue)
				{
					return DateTimeOffset.Now;
				}
				return nullable.GetValueOrDefault();
			}
		}

		public static TimeSpan Offset
		{
			get
			{
				return Time.Now.Offset;
			}
		}

		public static DateTimeOffset Today
		{
			get
			{
				return Time.Now.SetTime(Time.MidNight);
			}
		}

		public static DateTimeOffset Tomorrow
		{
			get
			{
				return Time.Today.Tomorrow();
			}
		}

		public static DateTimeOffset UnixEpoch
		{
			get
			{
				return new DateTimeOffset(new DateTime(1970, 1, 1), TimeSpan.Zero);
			}
		}

		public static double UnixTimestamp
		{
			get
			{
				return Time.ToUnixTime(Time.Now);
			}
		}

		public static DateTimeOffset UtcNow
		{
			get
			{
				DateTimeOffset? nullable = Time._utcNow;
				if (nullable.HasValue)
				{
					return nullable.GetValueOrDefault();
				}
				return Time.Now.ToUniversalTime();
			}
		}

		public static DateTimeOffset Yesterday
		{
			get
			{
				return Time.Today.Yesterday();
			}
		}

		static Time()
		{
			Time.Noon = new TimeSpan(0, 12, 0, 0, 0);
			Time.MidNight = TimeSpan.Zero;
			Time.EndOfDay = new TimeSpan(0, 23, 59, 59, 999);
			Time.BeginningOfDay = Time.MidNight;
			Time.OneDay = new TimeSpan(1, 0, 0, 0);
			Time.OneHour = new TimeSpan(0, 1, 0, 0);
			Time.OneMinute = new TimeSpan(0, 0, 1, 0);
			Time.OneSecond = new TimeSpan(0, 0, 0, 1);
			Time.OneWeek = new TimeSpan(7, 0, 0, 0);
		}

		public static DayOfWeek FirstDayOfWeek()
		{
			return CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
		}

		public static DateTimeOffset FromUnixTime(double timeStamp)
		{
			return Time.UnixEpoch.AddSeconds(timeStamp);
		}

		public static DayOfWeek LastDayOfWeek()
		{
			return Time.LastDayOfWeek(Time.FirstDayOfWeek());
		}

		public static DayOfWeek LastDayOfWeek(DayOfWeek firstDoW)
		{
            return (DayOfWeek)((int)(firstDoW + 6) % 7);
		}

		public static TimeSpan Measure(Action action)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Time.Measure(stopwatch, action);
			return stopwatch.Elapsed;
		}

		public static TimeSpan Measure(Action action, long numberOfIterations)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Time.Measure(stopwatch, action, numberOfIterations);
			return stopwatch.Elapsed;
		}

		public static void Measure(Stopwatch watch, Action action)
		{
			Time.Measure(watch, action, (long)1);
		}

		public static void Measure(Stopwatch watch, Action action, long numberOfIterations)
		{
			watch.Reset();
			watch.Start();
			for (int i = 0; (long)i < numberOfIterations; i++)
			{
				action();
			}
			watch.Stop();
		}

		public static void ResetNow()
		{
			Time._now = null;
		}

		public static void ResetUtcNow()
		{
			Time._utcNow = null;
		}

		public static void SetNow(DateTimeOffset now)
		{
			Time._now = new DateTimeOffset?(now);
		}

		public static void SetUtcNow(DateTimeOffset now)
		{
			TimeSpan offset = now.Offset;
			string timeMustBeUtcTemplate = Exceptions.Time_MustBeUtcTemplate;
			string[] str = new string[] { now.Offset.ToString() };
			Guard.Against<InvalidTimeZoneException>(!offset.Equals(TimeSpan.Zero), timeMustBeUtcTemplate, str);
			if (now.Offset.Equals(TimeSpan.Zero))
			{
				Time._utcNow = new DateTimeOffset?(now);
			}
		}

		public static double ToUnixTime(DateTimeOffset date)
		{
			return Math.Floor((date - Time.UnixEpoch).TotalSeconds);
		}
	}
}