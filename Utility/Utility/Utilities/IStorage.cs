using System;
using System.Reflection;

namespace Vertica.Utilities_v4
{
	public interface IStorage
	{
		int Count
		{
			get;
		}

		object this[object key]
		{
			get;
			set;
		}

		void Clear();
	}
}