using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4
{
	[Serializable]
	public abstract class Enumerated<T> : IEnumerated
	where T : IEnumerated
	{
		private readonly static EnumeratedRepository<T> _repo;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
                _Name = value;
			}
		}

		private string _Name;

		
		public static IEnumerable<string> Names
		{
			get
			{
				return 
					from   e in _repo.FindAll()
					select e.Name;
			}
		}

		public static IEnumerable<T> Values
		{
			get
			{
				return Enumerated<T>._repo.FindAll();
			}
		}

		static Enumerated()
		{
			Enumerated<T>._repo = new EnumeratedRepository<T>();
		}

		protected Enumerated(string name)
		{
			this.Name = name;
			Enumerated<T>._repo.Add(this);
		}

		protected bool Equals(Enumerated<T> classEnumBase)
		{
			if (classEnumBase == null)
			{
				return false;
			}
			if (this.GetType() != classEnumBase.GetType())
			{
				return false;
			}
			return object.Equals(this.Name, classEnumBase.Name);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			return this.Equals(obj as Enumerated<T>);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public static bool operator ==(Enumerated<T> x, Enumerated<T> y)
		{
			return object.Equals(x, y);
		}

		public static bool operator !=(Enumerated<T> x, Enumerated<T> y)
		{
			return !object.Equals(x, y);
		}

		public static T Parse(string enumName)
		{
			T t;
			if (!Enumerated<T>.TryParse(enumName, out t))
			{
				ExceptionHelper.ThrowArgumentException("enumName", Exceptions.Enumerated_NotFoundTemplate, new string[] { enumName });
			}
			return t;
		}

		public static bool TryParse(string enumName, out T value)
		{
			return Enumerated<T>._repo.TryFind(enumName, out value);
		}
	}
}