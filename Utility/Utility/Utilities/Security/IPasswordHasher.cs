using System;

namespace Vertica.Utilities_v4.Security
{
	public interface IPasswordHasher
	{
		bool CheckPassword(string password, string hashed);

		string HashPassword(string password);
	}
}