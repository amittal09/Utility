using System;
using System.Collections;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Collections
{
	public interface IRandomizer : IEnumerable<int>, IEnumerable
	{
		int Next(int maxValue);
	}
}