using System;
using System.Collections.Generic;
using System.Linq;
using Vertica.Utilities_v4.Extensions.StringExt;

namespace Vertica.Utilities_v4
{
	public static class Option
	{
		public static Option<T> Maybe<T>(T value)
		where T : class
		{
			if (value == null)
			{
				return Option<T>.None;
			}
			return Option.Some<T>(value);
		}

		public static Option<T> Maybe<T>(T value, Func<T> defaultValue)
		where T : class
		{
			if (value != null)
			{
				return Option.Some<T>(value);
			}
			return Option.None<T>(defaultValue());
		}

		public static Option<T> Maybe<T>(Nullable<T> value)
		where T : struct
		{
			if (!value.HasValue)
			{
				return Option.None<T>(value.GetValueOrDefault());
			}
			return Option.Some<T>(value.Value);
		}

		public static Option<string> Maybe(string value)
		{
			if (!value.IsEmpty())
			{
				return Option.Some<string>(value);
			}
			return Option.None<string>(string.Empty);
		}

		public static Option<IEnumerable<T>> Maybe<T>(IEnumerable<T> value)
		{
			if (value != null && value.Any<T>())
			{
				return Option.Some<IEnumerable<T>>(value);
			}
			return Option.None<IEnumerable<T>>(Enumerable.Empty<T>());
		}

		public static Option<IEnumerable<T>> Maybe<T>(T[] value)
		{
			if (value == null || !value.Any<T>())
			{
				return Option.None<IEnumerable<T>>(Enumerable.Empty<T>());
			}
			return Option.Some<IEnumerable<T>>(value.AsEnumerable<T>());
		}

		public static Option<IEnumerable<T>> Maybe<T>(IList<T> value)
		{
			if (value == null || !value.Any<T>())
			{
				return Option.None<IEnumerable<T>>(Enumerable.Empty<T>());
			}
			return Option.Some<IEnumerable<T>>(value.AsEnumerable<T>());
		}

		public static Option<IEnumerable<T>> Maybe<T>(ICollection<T> value)
		{
			if (value == null || !value.Any<T>())
			{
				return Option.None<IEnumerable<T>>(Enumerable.Empty<T>());
			}
			return Option.Some<IEnumerable<T>>(value.AsEnumerable<T>());
		}

		public static Option<T> None<T>(T defaultValue)
		{
			return Option<T>.NoneWithDefault(defaultValue);
		}

		public static Option<T> Some<T>(T value)
		{
			return Option<T>.Some(value);
		}
	}
}