using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Extensions.StringExt;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4.Security
{
	public class Credential
	{
		private readonly static char LogonNameSeparator;

		private readonly static char UpnSeparator;

		public string Domain
		{
			get;
			private set;
		}

		public string Password
		{
			get;
			private set;
		}

		public string UserName
		{
			get;
			private set;
		}

		static Credential()
		{
			Credential.LogonNameSeparator = '\\';
			Credential.UpnSeparator = '@';
		}

		public Credential(string userName, string password) : this(userName, password, string.Empty)
		{
		}

		public Credential(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.Password = password;
			this.Domain = domain;
		}

		private void assertHasUserAndDomain()
		{
			Guard.Against((this.UserName.IsEmpty() ? true : this.Domain.IsEmpty()), Exceptions.Credential_NoDomainOrUser, new string[0]);
		}

		public string LogonName()
		{
			this.assertHasUserAndDomain();
			return string.Concat(this.Domain, Credential.LogonNameSeparator, this.UserName);
		}

		public static string RemoveDomainIfPresent(string accountName)
		{
			StringExtensions.SubstrExtensionPoint substrExtensionPoint = accountName.Substr();
			char logonNameSeparator = Credential.LogonNameSeparator;
			string str = substrExtensionPoint.RightFromFirst(logonNameSeparator.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal);
			return str ?? accountName;
		}

		public static string ToLogonName(string userPrincipalName)
		{
			string str;
			string str1;
			if (!Credential.TryParseUserPrincipalName(userPrincipalName, out str, out str1))
			{
				throw new ArgumentException("userPrincipalName");
			}
			return (new Credential(str, null, str1)).LogonName();
		}

		public static string ToLogonName(string userName, string domainName)
		{
			return (new Credential(userName, null, domainName)).LogonName();
		}

		public override string ToString()
		{
			if (this.Domain.IsEmpty())
			{
				return this.UserName;
			}
			return string.Concat(this.Domain, Credential.LogonNameSeparator, this.UserName);
		}

		public static string ToUserPrincipalName(string logonName)
		{
			string str;
			string str1;
			if (!Credential.TryParseLogonName(logonName, out str, out str1))
			{
				throw new ArgumentException("logonName");
			}
			return (new Credential(str, null, str1)).UserPrincipalName();
		}

		public static string ToUserPrincipalName(string userName, string domainName)
		{
			return (new Credential(userName, null, domainName)).UserPrincipalName();
		}

		public static bool TryParseLogonName(string logonName, out string userName, out string domainName)
		{
			bool flag = false;
			object obj = null;
			string str = (string)obj;
			domainName = (string)obj;
			userName = str;
			if (logonName.IsNotEmpty())
			{
				int num = 0;
				int num1 = 1;
				char[] logonNameSeparator = new char[] { Credential.LogonNameSeparator };
				string[] strArrays = logonName.Split(logonNameSeparator);
				flag = (!((int)strArrays.Length).Equals(2) || !strArrays[num].IsNotEmpty() ? false : strArrays[num1].IsNotEmpty());
				if (flag)
				{
					domainName = strArrays[num];
					userName = strArrays[num1];
				}
			}
			return flag;
		}

		public static bool TryParseUserPrincipalName(string userPrincipalName, out string userName, out string domainName)
		{
			bool flag = false;
			object obj = null;
			string str = (string)obj;
			domainName = (string)obj;
			userName = str;
			if (userPrincipalName.IsNotEmpty())
			{
				int num = 1;
				int num1 = 0;
				char[] upnSeparator = new char[] { Credential.UpnSeparator };
				string[] strArrays = userPrincipalName.Split(upnSeparator);
				flag = (!((int)strArrays.Length).Equals(2) || !strArrays[num].IsNotEmpty() ? false : strArrays[num1].IsNotEmpty());
				if (flag)
				{
					domainName = strArrays[num];
					userName = strArrays[num1];
				}
			}
			return flag;
		}

		public string UserPrincipalName()
		{
			this.assertHasUserAndDomain();
			return string.Concat(this.UserName, Credential.UpnSeparator, this.Domain);
		}
	}
}