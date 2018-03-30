using System;
using System.Collections.Generic;

namespace Vertica.Utilities_v4.Patterns
{
	public class GenericVisitor<TBase>
	{
		private readonly Dictionary<RuntimeTypeHandle, object> _delegates;

		public GenericVisitor()
		{
		}

		public void AddDelegate<TSub>(GenericVisitor<TBase>.VisitDelegate<TSub> del)
		where TSub : TBase
		{
			this._delegates.Add(typeof(TSub).TypeHandle, del);
		}

		public void Visit<TSub>(TSub x)
		where TSub : TBase
		{
			RuntimeTypeHandle typeHandle = typeof(TSub).TypeHandle;
			if (this._delegates.ContainsKey(typeHandle))
			{
				((GenericVisitor<TBase>.VisitDelegate<TSub>)this._delegates[typeHandle])(x);
			}
		}

		public delegate void VisitDelegate<in TSub>(TSub u)
		where TSub : TBase;
	}
}