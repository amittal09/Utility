using System;

namespace Vertica.Utilities_v4.Eventing
{
	public static class Args
	{
		public static ValueCancelEventArgs<T> Cancel<T>(T value)
		{
			return new ValueCancelEventArgs<T>(value);
		}

		public static ValueIndexCancelEventArgs<T> Cancel<T>(int index, T value)
		{
			return new ValueIndexCancelEventArgs<T>(index, value);
		}

		public static PropertyValueChangedEventArgs<T> Changed<T>(string propertyName, T oldValue, T newValue)
		{
			return new PropertyValueChangedEventArgs<T>(propertyName, oldValue, newValue);
		}

		public static ValueIndexChangedEventArgs<T> Changed<T>(int index, T oldValue, T newValue)
		{
			return new ValueIndexChangedEventArgs<T>(index, oldValue, newValue);
		}

		public static PropertyValueChangingEventArgs<T> Changing<T>(string propertyName, T oldValue, T newValue)
		{
			return new PropertyValueChangingEventArgs<T>(propertyName, oldValue, newValue);
		}

		public static ValueIndexChangingEventArgs<T> Changing<T>(int index, T oldValue, T newValue)
		{
			return new ValueIndexChangingEventArgs<T>(index, newValue, oldValue);
		}

		public static ValueIndexEventArgs<T> Index<T>(int index, T value)
		{
			return new ValueIndexEventArgs<T>(index, value);
		}

		public static MutableValueEventArgs<T> Mutable<T>(T value)
		{
			return new MutableValueEventArgs<T>()
			{
				Value = value
			};
		}

		public static MultiEventArgs<T, U> Value<T, U>(T value1, U value2)
		{
			return new MultiEventArgs<T, U>(value1, value2);
		}

		public static ValueEventArgs<T> Value<T>(T value)
		{
			return new ValueEventArgs<T>(value);
		}
	}
}