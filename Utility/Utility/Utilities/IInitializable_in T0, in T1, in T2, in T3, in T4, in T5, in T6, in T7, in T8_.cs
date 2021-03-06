using System;

namespace Vertica.Utilities_v4
{
	public interface IInitializable<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>
	{
		void Initialize(T0 t0, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8);
	}
}