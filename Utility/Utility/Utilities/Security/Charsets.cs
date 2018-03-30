using System;

namespace Vertica.Utilities_v4.Security
{
	[Flags]
	public enum Charsets
	{
		Digits = 1,
		Letters = 2,
		AlphaNumeric = 3,
		SpecialCharacters = 4,
		All = 7
	}
}