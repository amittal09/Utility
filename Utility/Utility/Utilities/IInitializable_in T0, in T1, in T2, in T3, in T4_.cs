using System;

namespace Vertica.Utilities_v4
{
	public interface IInitializable<in T0, in T1, in T2, in T3, in T4>
	{
		void Initialize(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4);
	}
}