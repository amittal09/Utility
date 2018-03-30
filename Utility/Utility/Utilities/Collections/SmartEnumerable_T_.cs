using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Collections
{
	public class SmartEnumerable<T> : IEnumerable<SmartEntry<T>>, IEnumerable
	{
		private readonly IEnumerable<T> _wrapped;

		public SmartEnumerable(IEnumerable<T> enumerable)
		{
			Guard.AgainstNullArgument("enumerable", enumerable);
			this._wrapped = enumerable;
		}

		public IEnumerator<SmartEntry<T>> GetEnumerator()
		{
			using (IEnumerator<T> enumerator = this._wrapped.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					bool flag = true;
					bool flag1 = false;
					int num = 0;
					while (!flag1)
					{
						T t = enumerator.Current;
						flag1 = !enumerator.MoveNext();
						bool flag2 = flag;
						bool flag3 = flag1;
						T t1 = t;
						int num1 = num;
						int num2 = num1;
						num = num1 + 1;
						yield return new SmartEntry<T>(flag2, flag3, t1, num2);
						flag = false;
					}
				}
				else
				{
				}
			}
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}