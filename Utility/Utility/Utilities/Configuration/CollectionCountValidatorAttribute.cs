using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Configuration
{
	[AttributeUsage(AttributeTargets.Property)]
	public class CollectionCountValidatorAttribute : ConfigurationValidatorAttribute
	{
		public uint MinCount
		{
			get;
			set;
		}

		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new CollectionCountValidator(this.MinCount);
			}
		}

		public CollectionCountValidatorAttribute()
		{
		}
	}
}