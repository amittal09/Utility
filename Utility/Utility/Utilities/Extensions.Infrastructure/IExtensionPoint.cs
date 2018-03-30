using System;

namespace Vertica.Utilities_v4.Extensions.Infrastructure
{
	public interface IExtensionPoint
	{
		Type ExtendedType
		{
			get;
		}

		object ExtendedValue
		{
			get;
		}
	}
}