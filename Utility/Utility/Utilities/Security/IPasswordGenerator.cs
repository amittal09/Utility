using System;

namespace Vertica.Utilities_v4.Security
{
	public interface IPasswordGenerator
	{
		string Generate(int length, Charsets charsets);
	}
}