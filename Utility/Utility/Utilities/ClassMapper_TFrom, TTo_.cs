using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4
{
	public abstract class ClassMapper<TFrom, TTo> : IMapper<TFrom, TTo>
	where TFrom : class
	where TTo : class
	{
		protected ClassMapper()
		{
		}

		public TTo Map(TFrom from)
		{
			ClassMapper<TFrom, TTo> classMapper = this;
			return ClassMapper.MapIfNotNull<TFrom, TTo>(from, new Func<TFrom, TTo>(classMapper.MapOne));
		}

		public TTo Map(TFrom from, TTo defaultTo)
		{
			ClassMapper<TFrom, TTo> classMapper = this;
			return ClassMapper.MapIfNotNull<TFrom, TTo>(from, new Func<TFrom, TTo>(classMapper.MapOne), defaultTo);
		}

		public IEnumerable<TTo> Map(IEnumerable<TFrom> from)
		{
			return ClassMapper.MapIfNotNull<TFrom, TTo>(from, (TFrom partialFrom) => ClassMapper.MapIfNotNull<TFrom, TTo>(partialFrom, new Func<TFrom, TTo>(this.Map)));
		}

		public abstract TTo MapOne(TFrom from);
	}
}