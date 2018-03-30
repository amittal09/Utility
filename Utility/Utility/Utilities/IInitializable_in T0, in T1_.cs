using System;

namespace Vertica.Utilities_v4
{
	public interface IInitializable<in T0, in T1>
	{
		void Initialize(T0 t0, T1 t1);
	}
}