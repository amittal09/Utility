using System.Collections.Generic;

namespace Vertica.Utilities_v4.Patterns
{
	public interface IOperation<T>
	{
		IEnumerable<T> Execute(IEnumerable<T> input);
	}
}