using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4
{
	[Serializable]
	public class InvalidDomainException<T> : InvalidOperationException
	{
		public InvalidDomainException()
		{
		}

		public InvalidDomainException(string message) : base(message)
		{
		}

		public InvalidDomainException(string message, Exception inner) : base(message, inner)
		{
		}

		protected InvalidDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public InvalidDomainException(T actualValue, IEnumerable<T> expectedDomainValues) : this(InvalidDomainException<T>.buildMessage(actualValue, expectedDomainValues))
		{
		}

		private static string buildMessage(T actualValue, IEnumerable<T> expectedDomainValues)
		{
			string str = string.Join<T>(", ", expectedDomainValues ?? Enumerable.Empty<T>());
			return string.Format(Exceptions.InvalidDomainException_MessageTemplate, actualValue, str);
		}
	}
}