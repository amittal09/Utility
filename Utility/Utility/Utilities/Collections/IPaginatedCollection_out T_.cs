using System;
using System.Collections;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Collections
{
	public interface IPaginatedCollection<out T> : IEnumerable<T>, IEnumerable
	{
		IEnumerable<T> Collection
		{
			get;
		}

		uint CurrentPage
		{
			get;
		}

		uint NumberOfPages
		{
			get;
		}

		uint Pagesize
		{
			get;
		}
	}
}