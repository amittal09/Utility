using System;
using System.Runtime.CompilerServices;

namespace Vertica.Utilities_v4.Web
{
	public struct CompactGuid : IEquatable<CompactGuid>
	{
		public System.Guid Guid
		{
			get;
			private set;
		}

		public string Value
		{
			get;
			private set;
		}

		public CompactGuid(string value)
		{
			this = new CompactGuid()
			{
				Value = value,
				Guid = CompactGuid.Decode(value)
			};
		}

		public CompactGuid(System.Guid guid)
		{
			this = new CompactGuid()
			{
				Value = CompactGuid.Encode(guid),
				Guid = guid
			};
		}

		public static System.Guid Decode(string value)
		{
			value = value.Replace("_", "/").Replace("-", "+");
			byte[] numArray = Convert.FromBase64String(string.Concat(value, "=="));
			return new System.Guid(numArray);
		}

		public static string Encode(string value)
		{
			return CompactGuid.Encode(new System.Guid(value));
		}

		public static string Encode(System.Guid guid)
		{
			string base64String = Convert.ToBase64String(guid.ToByteArray());
			base64String = base64String.Replace("/", "_").Replace("+", "-");
			return base64String.Substring(0, 22);
		}

		public bool Equals(CompactGuid other)
		{
			return this.Guid.Equals(other.Guid);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (!(obj is CompactGuid))
			{
				return false;
			}
			return this.Equals((CompactGuid)obj);
		}

		public override int GetHashCode()
		{
			return this.Guid.GetHashCode();
		}

		public static CompactGuid NewGuid()
		{
			return new CompactGuid(System.Guid.NewGuid());
		}

		public static bool operator ==(CompactGuid left, CompactGuid right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(CompactGuid left, CompactGuid right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}