using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Reflection;

namespace Vertica.Utilities_v4.Eventing
{
	public static class EventHelper
	{
		public static void Notify<T, TValue>(this T instance, PropertyChangedEventHandler handler, Expression<Func<T, TValue>> selector, TValue oldValue, TValue newValue)
		where T : INotifyPropertyChanged
		{
			if (handler == null)
			{
				return;
			}
			handler(instance, new PropertyValueChangedEventArgs<TValue>(Name.Of<T, TValue>(selector), oldValue, newValue));
		}

		public static bool Notify<T, TValue>(this T instance, PropertyChangingEventHandler handler, Expression<Func<T, TValue>> selector, TValue oldValue, TValue newValue)
		where T : INotifyPropertyChanging
		{
			if (handler == null)
			{
				return false;
			}
			PropertyValueChangingEventArgs<TValue> propertyValueChangingEventArg = new PropertyValueChangingEventArgs<TValue>(Name.Of<T, TValue>(selector), oldValue, newValue);
			handler(instance, propertyValueChangingEventArg);
			return propertyValueChangingEventArg.IsCancelled;
		}

		public static IDisposable Observed(this INotifyPropertyChanged notify, PropertyChangedEventHandler handler)
		{
			notify.PropertyChanged += handler;
			return new DisposableAction(() => notify.PropertyChanged -= handler);
		}

		public static IDisposable Observing(this INotifyPropertyChanging notify, PropertyChangingEventHandler handler)
		{
			notify.PropertyChanging += handler;
			return new DisposableAction(() => notify.PropertyChanging -= handler);
		}

		public static void Raise(this EventHandler handler, object sender, EventArgs e)
		{
			EventHandler eventHandler = handler;
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		public static void Raise<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
		where TEventArgs : EventArgs
		{
			EventHandler<TEventArgs> eventHandler = handler;
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		public static void Raise<TEventArgs, TSender>(this EventHandler<TEventArgs> handler, TSender sender, TEventArgs e)
		where TEventArgs : EventArgs
		{
			EventHandler<TEventArgs> eventHandler = handler;
			if (eventHandler != null)
			{
				eventHandler(sender, e);
			}
		}

		public static void Raise(this PropertyChangedEventHandler handler, object sender, string propertyName)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = handler;
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(sender, new PropertyChangedEventArgs(propertyName));
			}
		}

		public static void Raise(this PropertyChangingEventHandler handler, object sender, string propertyName)
		{
			PropertyChangingEventHandler propertyChangingEventHandler = handler;
			if (propertyChangingEventHandler != null)
			{
				propertyChangingEventHandler(sender, new PropertyChangingEventArgs(propertyName));
			}
		}

		public static bool Raise<T>(this ChainedEventHandler<T> delegates, object sender, T arg)
		where T : ChainedEventArgs
		{
			bool handled = false;
			if (delegates != null)
			{
				Delegate[] invocationList = delegates.GetInvocationList();
				for (int i = 0; i < (int)invocationList.Length && !handled; i++)
				{
					((ChainedEventHandler<T>)invocationList[i])(sender, arg);
					handled = arg.Handled;
				}
			}
			return handled;
		}

		public static bool RaiseUntil<T, K>(this ChainedEventHandler<T, K> delegates, object sender, T args, Predicate<K> predicate)
		where T : IMutableValueEventArgs<K>
		{
			bool flag = false;
			if (delegates != null)
			{
				Delegate[] invocationList = delegates.GetInvocationList();
				for (int i = 0; i < (int)invocationList.Length && !flag; i++)
				{
					K k = ((ChainedEventHandler<T, K>)invocationList[i])(sender, args);
					flag = predicate(k);
				}
			}
			return flag;
		}
	}
}