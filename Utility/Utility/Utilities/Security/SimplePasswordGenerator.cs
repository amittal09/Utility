using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Vertica.Utilities_v4.Security
{
	public class SimplePasswordGenerator
	{
		private readonly static Random _random;

		private readonly static Lazy<IEnumerable<char>> _digits;

		private readonly static Lazy<IEnumerable<char>> _letters;

		private readonly static Lazy<IEnumerable<char>> _special;

		static SimplePasswordGenerator()
		{
			Guid guid = Guid.NewGuid();
			SimplePasswordGenerator._random = new Random(guid.GetHashCode());
			SimplePasswordGenerator._digits = new Lazy<IEnumerable<char>>(() => SimplePasswordGenerator.getRange('0', '9'));
			SimplePasswordGenerator._letters = new Lazy<IEnumerable<char>>(() => SimplePasswordGenerator.getRange('a', 'z'));
			SimplePasswordGenerator._special = new Lazy<IEnumerable<char>>(() => SimplePasswordGenerator.getRange(':', '@').Concat<char>(SimplePasswordGenerator.getRange('[', ']')).Concat<char>(SimplePasswordGenerator.getRange('[', ']')).Concat<char>(SimplePasswordGenerator.getRange('{', '}')).Concat<char>(new char[] { '\u005F' }));
		}

		public SimplePasswordGenerator()
		{
		}

		public string Generate(uint length, Charsets charsets)
		{
			List<char> chrs = new List<char>();
			if ((charsets & Charsets.Digits) == Charsets.Digits)
			{
				chrs.AddRange(SimplePasswordGenerator._digits.Value);
			}
			if ((charsets & Charsets.Letters) == Charsets.Letters)
			{
				chrs.AddRange(SimplePasswordGenerator._letters.Value);
			}
			if ((charsets & Charsets.SpecialCharacters) == Charsets.SpecialCharacters)
			{
				chrs.AddRange(SimplePasswordGenerator._special.Value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; (long)i <= (long)length; i++)
			{
				stringBuilder.Append(chrs[SimplePasswordGenerator._random.Next(0, chrs.Count)]);
			}
			return stringBuilder.ToString();
		}

		private static IEnumerable<char> getRange(char start, char end)
		{
			if (start > end)
			{
				throw new ArgumentException("start must be less than or equal to end.");
			}
			for (byte i = (byte)start; i <= (byte)end; i = (byte)(i + 1))
			{
				yield return Convert.ToChar(i);
			}
		}
	}
}