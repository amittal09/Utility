using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Vertica.Utilities_v4
{
	public static class UniqueIdentifier
	{
		private const int RPC_S_OK = 0;

		private readonly static DateTimeOffset _baseDate;

		private readonly static int[] _sqlOrderMap;

		static UniqueIdentifier()
		{
			UniqueIdentifier._baseDate = new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero);
			UniqueIdentifier._sqlOrderMap = new int[] { 3, 2, 1, 0, 5, 4, 7, 6, 9, 8, 15, 14, 13, 12, 11, 10 };
		}

		public static Guid Comb()
		{
			byte[] byteArray = Guid.NewGuid().ToByteArray();
			DateTimeOffset utcNow = Time.UtcNow;
			TimeSpan timeSpan = new TimeSpan(utcNow.Ticks - UniqueIdentifier._baseDate.Ticks);
			TimeSpan timeOfDay = utcNow.TimeOfDay;
			byte[] bytes = BitConverter.GetBytes(timeSpan.Days);
			byte[] numArray = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
			Array.Reverse(bytes);
			Array.Reverse(numArray);
			Array.Copy(bytes, (int)bytes.Length - 2, byteArray, (int)byteArray.Length - 6, 2);
			Array.Copy(numArray, (int)numArray.Length - 4, byteArray, (int)byteArray.Length - 4, 4);
			return new Guid(byteArray);
		}

		private static Guid generateSequentialGuid()
		{
			Guid guid;
			int num = UniqueIdentifier.UuidCreateSequential(out guid);
			if (num != 0)
			{
				throw new Win32Exception(num, string.Format("UuidCreateSequential() call failed: {0}", num));
			}
			return guid;
		}

		public static Guid Sequential()
		{
			return UniqueIdentifier.setSortOrder(UniqueIdentifier.generateSequentialGuid());
		}

		private static Guid setSortOrder(Guid guid)
		{
			byte[] byteArray = guid.ToByteArray();
			byte[] numArray = new byte[16];
			byteArray.CopyTo(numArray, 0);
			for (int i = 0; i < 10; i++)
			{
				byteArray[i] = numArray[UniqueIdentifier._sqlOrderMap[i]];
			}
			guid = new Guid(byteArray);
			return guid;
		}

		[DllImport("rpcrt4.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern int UuidCreateSequential(out Guid guid);
	}
}