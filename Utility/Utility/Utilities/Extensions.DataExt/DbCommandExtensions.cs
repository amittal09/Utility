using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;

namespace Vertica.Utilities_v4.Extensions.DataExt
{
	public static class DbCommandExtensions
	{
		private readonly static IDictionary<Type, DbType> _map;

		static DbCommandExtensions()
		{
			Dictionary<Type, DbType> types = new Dictionary<Type, DbType>()
			{
				{ typeof(long), DbType.Int64 },
				{ typeof(byte[]), DbType.Binary },
				{ typeof(bool), DbType.Boolean },
				{ typeof(string), DbType.String },
				{ typeof(char), DbType.StringFixedLength },
				{ typeof(char[]), DbType.StringFixedLength },
				{ typeof(DateTime), DbType.DateTime },
				{ typeof(DateTimeOffset), DbType.DateTimeOffset },
				{ typeof(decimal), DbType.Decimal },
				{ typeof(double), DbType.Double },
				{ typeof(int), DbType.Int32 },
				{ typeof(float), DbType.Single },
				{ typeof(short), DbType.Int16 },
				{ typeof(object), DbType.Object },
				{ typeof(TimeSpan), DbType.Time },
				{ typeof(byte), DbType.Byte },
				{ typeof(Guid), DbType.Guid }
			};
			DbCommandExtensions._map = types;
		}

		public static int AddInputParameter<T>(this IDbCommand cmd, string name, T value)
		where T : class
		{
			IDbDataParameter dbDataParameter = cmd.CreateParameter();
			dbDataParameter.ParameterName = name;
			IDbDataParameter dbDataParameter1 = dbDataParameter;
			object obj = value;
			if (obj == null)
			{
				obj = DBNull.Value;
			}
			dbDataParameter1.Value = obj;
			return cmd.Parameters.Add(dbDataParameter);
		}

		public static int AddInputParameter<T>(this IDbCommand cmd, string name, Nullable<T> value)
		where T : struct
		{
			object obj;
			IDbDataParameter dbDataParameter = cmd.CreateParameter();
			dbDataParameter.ParameterName = name;
			IDbDataParameter dbDataParameter1 = dbDataParameter;
			if (value.HasValue)
			{
				obj = value;
			}
			else
			{
				obj = DBNull.Value;
			}
			dbDataParameter1.Value = obj;
			return cmd.Parameters.Add(dbDataParameter);
		}

		public static void AddInputParameters<T>(this IDbCommand cmd, T parameters)
		where T : class
		{
			PropertyInfo[] properties = parameters.GetType().GetProperties();
			for (int i = 0; i < (int)properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				object value = propertyInfo.GetValue(parameters, null);
				IDbDataParameter name = cmd.CreateParameter();
				name.ParameterName = propertyInfo.Name;
				IDbDataParameter dbDataParameter = name;
				object obj = value;
				if (obj == null)
				{
					obj = DBNull.Value;
				}
				dbDataParameter.Value = obj;
				cmd.Parameters.Add(name);
			}
		}

		public static IDbDataParameter AddOutputParameter(this IDbCommand cmd, string name, DbType dbType)
		{
			IDbDataParameter dbDataParameter = cmd.CreateParameter();
			dbDataParameter.ParameterName = name;
			dbDataParameter.DbType = dbType;
			dbDataParameter.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(dbDataParameter);
			return dbDataParameter;
		}

		public static void AddOutputParameters<T>(this IDbCommand cmd, T parameters)
		where T : class
		{
			PropertyInfo[] properties = parameters.GetType().GetProperties();
			for (int i = 0; i < (int)properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				Type propertyType = propertyInfo.PropertyType;
				string name = propertyInfo.Name;
				try
				{
					DbType dbType = propertyType.Translate();
					cmd.AddOutputParameter(name, dbType);
				}
				catch (ArgumentException argumentException)
				{
					throw new ArgumentException(string.Format("Cannot convert type for property '{0}'.", name), name, argumentException);
				}
			}
		}

		public static DbType Translate(this Type netType)
		{
			DbType? nullable = netType.TryTranslate();
			if (!nullable.HasValue)
			{
				string[] name = new string[] { netType.Name };
				ExceptionHelper.ThrowArgumentException("netType", "Cannot map type '{0}' to any DbType.", name);
			}
			return nullable.Value;
		}

		public static DbType? TryTranslate(this Type netType)
		{
			DbType dbType;
			if (DbCommandExtensions._map.TryGetValue(netType, out dbType))
			{
				return new DbType?(dbType);
			}
			return null;
		}
	}
}