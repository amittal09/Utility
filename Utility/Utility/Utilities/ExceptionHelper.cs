using System;

namespace Vertica.Utilities_v4
{
	public static class ExceptionHelper
	{
		public static void Throw<T>(string templateMessage, params string[] arguments)
		where T : Exception
		{
			string str = string.Format(templateMessage, arguments);
			Type type = typeof(T);
			object[] objArray = new object[] { str };
			throw (Exception)Activator.CreateInstance(type, objArray);
		}

		public static void Throw<T>(Exception innerException, string templateMessage, params string[] arguments)
		where T : Exception
		{
			string str = string.Format(templateMessage, arguments);
			Type type = typeof(T);
			object[] objArray = new object[] { str, innerException };
			throw (Exception)Activator.CreateInstance(type, objArray);
		}

		public static void ThrowArgumentException(string paramName, string templateMessage, params string[] arguments)
		{
			throw new ArgumentException(string.Format(templateMessage, arguments), paramName);
		}

		public static void ThrowArgumentException(string paramName)
		{
			throw new ArgumentException(string.Empty, paramName);
		}

		public static void ThrowArgumentException<T>(string paramName)
		where T : ArgumentException
		{
			Type type = typeof(T);
			object[] objArray = new object[] { paramName };
			throw (ArgumentException)Activator.CreateInstance(type, objArray);
		}

		public static void ThrowArgumentException<T>(string paramName, string templateMessage, params string[] arguments)
		where T : ArgumentException
		{
			string str = string.Format(templateMessage, arguments);
			Type type = typeof(T);
			object[] objArray = new object[] { paramName, str };
			throw (ArgumentException)Activator.CreateInstance(type, objArray);
		}
	}
}