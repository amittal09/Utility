using System;
using System.Security.Cryptography;
using System.Text;

namespace Vertica.Utilities_v4.Security
{
	public class SimplePasswordHasher : IPasswordHasher
	{
		private readonly string _userName;

		public SimplePasswordHasher(string userName)
		{
			this._userName = userName;
		}

		public bool CheckPassword(string password, string hashed)
		{
			return StringComparer.Ordinal.Equals(hashed, this.HashPassword(password));
		}

		private string generateSalt()
		{
			Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(this._userName, Encoding.UTF8.GetBytes("thisisasalt"), 10000);
			return Convert.ToBase64String(rfc2898DeriveByte.GetBytes(25));
		}

		public string HashPassword(string password)
		{
			string str = this.generateSalt();
			Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(str), 10000);
			return Convert.ToBase64String(rfc2898DeriveByte.GetBytes(25));
		}
	}
}