using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4.Configuration
{
	public class CollectionCountValidator : ConfigurationValidatorBase
	{
		private readonly uint _minimumCount;

		public CollectionCountValidator(uint minCount)
		{
			this._minimumCount = minCount;
		}

		public override bool CanValidate(Type type)
		{
			return type.IsSubclassOf(typeof(ConfigurationElementCollection));
		}

		public override void Validate(object value)
		{
			ConfigurationElementCollection configurationElementCollections = (ConfigurationElementCollection)value;
			bool count = (long)configurationElementCollections.Count < (long)this._minimumCount;
			string collectionCountValidatorMessageTemplate = Exceptions.CollectionCountValidator_MessageTemplate;
			string[] name = new string[] { configurationElementCollections.GetType().Name, null, null };
			uint num = this._minimumCount;
			name[1] = num.ToString(CultureInfo.InvariantCulture);
			int count1 = configurationElementCollections.Count;
			name[2] = count1.ToString(CultureInfo.InvariantCulture);
			Guard.Against<ConfigurationErrorsException>(count, collectionCountValidatorMessageTemplate, name);
		}
	}
}