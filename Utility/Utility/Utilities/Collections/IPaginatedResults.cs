using System;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Collections
{
	public interface IPaginatedResults
	{
		uint CurrentPage
		{
			get;
		}

		string PageNumber
		{
			get;
		}

		Vertica.Utilities_v4.Collections.Pagination Pagination
		{
			get;
		}

		Range<uint> RecordNumbers
		{
			get;
		}

		uint TotalPages
		{
			get;
		}

		uint TotalResults
		{
			get;
		}
	}
}