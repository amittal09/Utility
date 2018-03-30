using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Vertica.Utilities_v4.Collections
{
	public class FrameworkRandomizer : IRandomizer, IEnumerable<int>, IEnumerable
	{
		private int? _maxValue;

		private static int _seed;

		[ThreadStatic]
		private static Random _random;

		private static Random random
		{
			get
			{
				Random random = FrameworkRandomizer._random;
				if (random == null)
				{
					random = new Random(Interlocked.Increment(ref FrameworkRandomizer._seed));
					FrameworkRandomizer._random = random;
				}
				return random;
			}
		}

		static FrameworkRandomizer()
		{
			FrameworkRandomizer._seed = Environment.TickCount;
		}

		public FrameworkRandomizer()
		{
		}

		public FrameworkRandomizer(int maxValue)
		{
			this._maxValue = new int?(maxValue);
		}

		private IEnumerable<int> buildEnumerable()
		{
			int num;
			while (true)
			{
				num = (this._maxValue.HasValue ? FrameworkRandomizer.random.Next(this._maxValue.Value) : FrameworkRandomizer.random.Next());
				yield return num;
			}
		}

		public IEnumerator<int> GetEnumerator()
		{
			return this.buildEnumerable().GetEnumerator();
		}

		public int Next(int maxValue)
		{
			return FrameworkRandomizer.random.Next(maxValue);
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}