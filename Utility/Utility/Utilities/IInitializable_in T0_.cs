using System;

namespace Vertica.Utilities_v4
{
	public interface IInitializable<in T0>
	{
		void Initialize(T0 t0);
	}
}