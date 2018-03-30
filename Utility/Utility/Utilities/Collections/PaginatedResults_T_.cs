using System;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Collections
{
	public class PaginatedResults<T> : IPaginatedResults
	{
		public uint CurrentPage
		{
			get
			{
				return JustDecompileGenerated_get_CurrentPage();
			}
			set
			{
				JustDecompileGenerated_set_CurrentPage(value);
			}
		}

		private uint JustDecompileGenerated_CurrentPage_k__BackingField;

		public uint JustDecompileGenerated_get_CurrentPage()
		{
			return this.JustDecompileGenerated_CurrentPage_k__BackingField;
		}

		private void JustDecompileGenerated_set_CurrentPage(uint value)
		{
			this.JustDecompileGenerated_CurrentPage_k__BackingField = value;
		}

		public string PageNumber
		{
			get
			{
				return "PageNumber";
			}
		}

		public T[] PageOfResults
		{
			get;
			private set;
		}

		public Vertica.Utilities_v4.Collections.Pagination Pagination
		{
			get
			{
				return JustDecompileGenerated_get_Pagination();
			}
			set
			{
				JustDecompileGenerated_set_Pagination(value);
			}
		}

		private Vertica.Utilities_v4.Collections.Pagination JustDecompileGenerated_Pagination_k__BackingField;

		public Vertica.Utilities_v4.Collections.Pagination JustDecompileGenerated_get_Pagination()
		{
			return this.JustDecompileGenerated_Pagination_k__BackingField;
		}

		private void JustDecompileGenerated_set_Pagination(Vertica.Utilities_v4.Collections.Pagination value)
		{
			this.JustDecompileGenerated_Pagination_k__BackingField = value;
		}

		public Range<uint> RecordNumbers
		{
			get
			{
				return JustDecompileGenerated_get_RecordNumbers();
			}
			set
			{
				JustDecompileGenerated_set_RecordNumbers(value);
			}
		}

		private Range<uint> JustDecompileGenerated_RecordNumbers_k__BackingField;

		public Range<uint> JustDecompileGenerated_get_RecordNumbers()
		{
			return this.JustDecompileGenerated_RecordNumbers_k__BackingField;
		}

		private void JustDecompileGenerated_set_RecordNumbers(Range<uint> value)
		{
			this.JustDecompileGenerated_RecordNumbers_k__BackingField = value;
		}

		public uint TotalPages
		{
			get
			{
				return JustDecompileGenerated_get_TotalPages();
			}
			set
			{
				JustDecompileGenerated_set_TotalPages(value);
			}
		}

		private uint JustDecompileGenerated_TotalPages_k__BackingField;

		public uint JustDecompileGenerated_get_TotalPages()
		{
			return this.JustDecompileGenerated_TotalPages_k__BackingField;
		}

		private void JustDecompileGenerated_set_TotalPages(uint value)
		{
			this.JustDecompileGenerated_TotalPages_k__BackingField = value;
		}

		public uint TotalResults
		{
			get
			{
				return JustDecompileGenerated_get_TotalResults();
			}
			set
			{
				JustDecompileGenerated_set_TotalResults(value);
			}
		}

		private uint JustDecompileGenerated_TotalResults_k__BackingField;

		public uint JustDecompileGenerated_get_TotalResults()
		{
			return this.JustDecompileGenerated_TotalResults_k__BackingField;
		}

		private void JustDecompileGenerated_set_TotalResults(uint value)
		{
			this.JustDecompileGenerated_TotalResults_k__BackingField = value;
		}

		public PaginatedResults()
		{
			this.PageOfResults = new T[0];
			this.RecordNumbers = Range.Empty<uint>();
		}

		public PaginatedResults(T[] pageOfResults, int totalResults, Vertica.Utilities_v4.Collections.Pagination pagination)
		{
			uint pageNumber;
			this.Pagination = pagination;
			uint num = Convert.ToUInt32(totalResults);
			if (pagination.PageNumber == 0)
			{
				pageNumber = 1;
			}
			else
			{
				pageNumber = pagination.PageNumber;
			}
			this.CurrentPage = pageNumber;
			this.PageOfResults = pageOfResults;
			this.TotalResults = num;
			this.TotalPages = pagination.PageCount(num);
			uint num1 = 0;
			if (this.CurrentPage <= this.TotalPages)
			{
                num1 = (PaginatedResults<T>.PageNotComplete(pageOfResults, pagination) ? Convert.ToUInt32((long)pagination.FirstRecord + (long)((int)pageOfResults.Length) - 1L) : pagination.LastRecord);
			}
			this.RecordNumbers = (num1 > 0 ? new Range<uint>(pagination.FirstRecord, num1) : new Range<uint>(0, 0));
		}

		private static bool PageNotComplete(T[] pageOfResults, Vertica.Utilities_v4.Collections.Pagination pagination)
		{
			return (long)pageOfResults.Length < (long)pagination.PageSize;
		}
	}
}