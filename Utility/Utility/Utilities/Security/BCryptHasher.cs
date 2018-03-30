using System;

namespace Vertica.Utilities_v4.Security
{
	public class BCryptHasher : IPasswordHasher
	{
		public BCryptHasher()
		{
		}

		public bool CheckPassword(string password, string hashed)
		{
			return BCrypt.CheckPassword(password, hashed);
		}

		public string HashPassword(string password)
		{
			return BCrypt.HashPassword(password, BCrypt.GenerateSalt());
		}
	}
}