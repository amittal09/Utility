using System;
using System.Collections.Generic;
using System.Text;

namespace Vertica.Utilities_v4.Security
{
	internal class Base64
	{
		private readonly static int[] _indexes;

		private readonly static char[] _codes;

		static Base64()
		{
			Base64._indexes = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, -1, -1, -1, -1, -1, -1, -1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, -1, -1, -1, -1, -1, -1, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, -1, -1, -1, -1, -1 };
			Base64._codes = new char[] { '.', '/', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
		}

		public Base64()
		{
		}

		public static int Char(char c)
		{
			int num = c;
			if (num < 0 || num > (int)Base64._indexes.Length)
			{
				return -1;
			}
			return Base64._indexes[num];
		}

		public static char Code(int index)
		{
			return Base64._codes[index];
		}

		public static byte[] Decode(string s, int maximumLength)
		{
			List<byte> nums = new List<byte>(Math.Min(maximumLength, s.Length));
			if (maximumLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maximumLength", (object)maximumLength, null);
			}
			int num = 0;
			int length = s.Length;
			for (int i = 0; num < length - 1 && i < maximumLength; i++)
			{
				int num1 = num;
				num = num1 + 1;
				int num2 = Base64.Char(s[num1]);
				int num3 = num;
				num = num3 + 1;
				int num4 = Base64.Char(s[num3]);
				if (num2 == -1 || num4 == -1)
				{
					break;
				}
				nums.Add((byte)(num2 << 2 | (num4 & 48) >> 4));
				int num5 = i + 1;
				i = num5;
				if (num5 >= maximumLength || num >= s.Length)
				{
					break;
				}
				int num6 = num;
				num = num6 + 1;
				int num7 = Base64.Char(s[num6]);
				if (num7 == -1)
				{
					break;
				}
				nums.Add((byte)((num4 & 15) << 4 | (num7 & 60) >> 2));
				int num8 = i + 1;
				i = num8;
				if (num8 >= maximumLength || num >= s.Length)
				{
					break;
				}
				int num9 = num;
				num = num9 + 1;
				int num10 = Base64.Char(s[num9]);
				nums.Add((byte)((num7 & 3) << 6 | num10));
			}
			return nums.ToArray();
		}

		public static string Encode(byte[] d, int length)
		{
			if (length <= 0 || length > (int)d.Length)
			{
				throw new ArgumentOutOfRangeException("length", (object)length, null);
			}
			StringBuilder stringBuilder = new StringBuilder(length * 2);
			int num = 0;
			while (num < length)
			{
				int num1 = num;
				num = num1 + 1;
				int num2 = d[num1] & 255;
				stringBuilder.Append(Base64.Code(num2 >> 2 & 63));
				num2 = (num2 & 3) << 4;
				if (num < length)
				{
					int num3 = num;
					num = num3 + 1;
					int num4 = d[num3] & 255;
					num2 = num2 | num4 >> 4 & 15;
					stringBuilder.Append(Base64.Code(num2 & 63));
					num2 = (num4 & 15) << 2;
					if (num < length)
					{
						int num5 = num;
						num = num5 + 1;
						num4 = d[num5] & 255;
						num2 = num2 | num4 >> 6 & 3;
						stringBuilder.Append(Base64.Code(num2 & 63));
						stringBuilder.Append(Base64.Code(num4 & 63));
					}
					else
					{
						stringBuilder.Append(Base64.Code(num2 & 63));
						break;
					}
				}
				else
				{
					stringBuilder.Append(Base64.Code(num2 & 63));
					break;
				}
			}
			return stringBuilder.ToString();
		}
	}
}