using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Extensions.DomainExt
{
	public static class DomainOfValuesExtensions
	{
		public static void AssertAgainst<TDomain>(this TDomain actualValue, params TDomain[] expectedDomainValues)
		{
			DomainOf.Values<TDomain>(expectedDomainValues).AssertContains(actualValue);
		}

		public static void AssertAgainst<TDomain>(this TDomain actualValue, IEnumerable<TDomain> expectedDomainValues)
		{
			DomainOf.Values<TDomain>(expectedDomainValues).AssertContains(actualValue);
		}

		public static bool CheckAgainst<TDomain>(this TDomain actualValue, params TDomain[] expectedDomainValues)
		{
			return DomainOf.Values<TDomain>(expectedDomainValues).CheckContains(actualValue);
		}

		public static bool CheckAgainst<TDomain>(this TDomain actualValue, IEnumerable<TDomain> expectedDomainValues)
		{
			return DomainOf.Values<TDomain>(expectedDomainValues).CheckContains(actualValue);
		}
	}
}