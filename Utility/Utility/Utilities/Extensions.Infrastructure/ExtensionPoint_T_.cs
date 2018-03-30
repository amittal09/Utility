using System;

namespace Vertica.Utilities_v4.Extensions.Infrastructure
{
	public class ExtensionPoint<T> : IExtensionPoint<T>, IExtensionPoint
	{
		private readonly Type _type;

		private readonly T _value;

		public Type ExtendedType
		{
			get
			{
				return this._type ?? typeof(T);
			}
		}

		public T ExtendedValue
		{
			get
			{
				return this._value;
			}
		}

		object Vertica.Utilities_v4.Extensions.Infrastructure.IExtensionPoint.ExtendedValue
		{
			get
			{
				return this._value;
			}
		}

		public ExtensionPoint(T value)
		{
			this._value = value;
		}

		public ExtensionPoint(Type type)
		{
			this._type = type;
		}
	}
}