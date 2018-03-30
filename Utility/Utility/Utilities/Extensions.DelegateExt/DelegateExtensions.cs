using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Extensions.DelegateExt
{
	public static class DelegateExtensions
	{
		public static T Cast<T>(this Delegate value)
		where T : class
		{
			if (value == null)
			{
				return default(T);
			}
			Delegate[] invocationList = value.GetInvocationList();
			if ((int)invocationList.Length == 1)
			{
				return (T)(Delegate.CreateDelegate(typeof(T), invocationList[0].Target, invocationList[0].Method) as T);
			}
			for (int i = 0; i < (int)invocationList.Length; i++)
			{
				invocationList[i] = Delegate.CreateDelegate(typeof(T), invocationList[i].Target, invocationList[0].Method);
			}
			return (T)(Delegate.Combine(invocationList) as T);
		}

		public static Action<TBase> Cast<TBase, TDerived>(this Action<TDerived> source)
		where TDerived : TBase
		{
			return (TBase baseValue) => source((TDerived)(object)baseValue);
		}
	}
}