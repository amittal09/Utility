using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Collections
{
	[Serializable]
	public struct Pagination
	{
		public uint FirstRecord
		{
			get
			{
				if (this.PageNumber == 0)
				{
					return (uint)1;
				}
				return (this.PageNumber - 1) * this.PageSize + 1;
			}
		}

		public uint LastRecord
		{
			get
			{
				if (this.PageSize == 0)
				{
					return this.FirstRecord;
				}
				return this.FirstRecord + this.PageSize - 1;
			}
		}

		public uint PageNumber
		{
			get;
			private set;
		}

		public uint PageSize
		{
			get;
			private set;
		}

		public Pagination(uint pageSize, uint pageNumber)
		{
			this = new Pagination()
			{
				PageSize = pageSize,
				PageNumber = pageNumber
			};
		}

		public uint PageCount(uint totalCount)
		{
			return (totalCount % this.PageSize > 0 ? totalCount / this.PageSize + 1 : totalCount / this.PageSize);
		}
	}
}