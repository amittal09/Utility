using System;
using System.DirectoryServices;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Vertica.Utilities_v4.DirectoryServices
{
	public static class DirectoryEntryExtensions
	{
		public static DirectoryEntry AddGroup(this DirectoryEntry container, string groupName)
		{
			DirectoryEntry directoryEntry = container.Children.Add(string.Concat("CN=", groupName), AdObjectCategory.@group.ToString());
			directoryEntry.SetProperty<string>(AdProperty.samAccountName, groupName);
			directoryEntry.CommitChanges();
			return directoryEntry;
		}

		public static DirectoryEntry AddMember(this DirectoryEntry group, DirectoryEntry member)
		{
			string str = AdEntryMethod.Add.ToString();
			object[] path = new object[] { member.Path };
			group.Invoke(str, path);
			group.CommitChanges();
			return group;
		}

		public static DirectoryEntry AddOu(this DirectoryEntry container, string ouName)
		{
			DirectoryEntry directoryEntry = container.Children.Add(string.Concat("OU=", ouName), AdObjectCategory.organizationalUnit.ToString());
			directoryEntry.CommitChanges();
			return directoryEntry;
		}

		public static DirectoryEntry AddUser(this DirectoryEntry container, string userName, string password, string displayName)
		{
			DirectoryEntry directoryEntry = container.Children.Add(string.Concat("CN=", userName), AdObjectCategory.user.ToString());
			directoryEntry.CommitChanges();
			directoryEntry.SetProperty<string>(AdProperty.samAccountName, userName);
			directoryEntry.SetProperty<string>(AdProperty.displayName, displayName);
			directoryEntry.CommitChanges();
			directoryEntry.SetPassword(password);
			return directoryEntry;
		}

		internal static void DelegateOuToGroup(this DirectoryEntry ou, string domainName, string groupName)
		{
			IdentityReference nTAccount = new NTAccount(domainName, groupName);
			AccessControlType accessControlType = AccessControlType.Allow;
			Guid guid = new Guid("{BF967A9C-0DE6-11D0-A285-00AA003049E2}");
			Guid guid1 = new Guid("{BF967AA5-0DE6-11D0-A285-00AA003049E2}");
			Guid guid2 = new Guid("{BF967ABA-0DE6-11D0-A285-00AA003049E2}");
			ActiveDirectoryRights activeDirectoryRight = ActiveDirectoryRights.CreateChild | ActiveDirectoryRights.DeleteChild;
			ActiveDirectorySecurityInheritance activeDirectorySecurityInheritance = ActiveDirectorySecurityInheritance.All;
			ou.ObjectSecurity.AddAccessRule(new ActiveDirectoryAccessRule(nTAccount, activeDirectoryRight, accessControlType, guid, activeDirectorySecurityInheritance));
			ou.ObjectSecurity.AddAccessRule(new ActiveDirectoryAccessRule(nTAccount, activeDirectoryRight, accessControlType, guid1, activeDirectorySecurityInheritance));
			ou.ObjectSecurity.AddAccessRule(new ActiveDirectoryAccessRule(nTAccount, activeDirectoryRight, accessControlType, guid2, activeDirectorySecurityInheritance));
			ActiveDirectoryRights activeDirectoryRight1 = ActiveDirectoryRights.GenericAll;
			ActiveDirectorySecurityInheritance activeDirectorySecurityInheritance1 = ActiveDirectorySecurityInheritance.Descendents;
			ou.ObjectSecurity.AddAccessRule(new ActiveDirectoryAccessRule(nTAccount, activeDirectoryRight1, accessControlType, activeDirectorySecurityInheritance1, guid));
			ou.ObjectSecurity.AddAccessRule(new ActiveDirectoryAccessRule(nTAccount, activeDirectoryRight1, accessControlType, activeDirectorySecurityInheritance1, guid1));
			ou.ObjectSecurity.AddAccessRule(new ActiveDirectoryAccessRule(nTAccount, activeDirectoryRight1, accessControlType, activeDirectorySecurityInheritance1, guid2));
			ou.CommitChanges();
		}

		public static DirectoryEntry EnableAccount(this DirectoryEntry user)
		{
			int value = (int)user.Properties[AdProperty.userAccountControl.ToString()].Value;
			user.Properties[AdProperty.userAccountControl.ToString()].Value = value | 1;
			user.CommitChanges();
			int num = (int)user.Properties[AdProperty.userAccountControl.ToString()].Value;
			user.Properties[AdProperty.userAccountControl.ToString()].Value = num & -3;
			user.CommitChanges();
			return user;
		}

		public static T GetProperty<T>(this DirectoryEntry entry, AdProperty property)
		{
			return (T)entry.Properties[property.ToString()][0];
		}

		public static void SetPassword(this DirectoryEntry user, string password)
		{
			string str = AdEntryMethod.SetPassword.ToString();
			object[] objArray = new object[] { password };
			user.Invoke(str, objArray);
			user.CommitChanges();
		}

		public static T SetProperty<T>(this DirectoryEntry entry, AdProperty property, T value)
		{
			PropertyValueCollection item = entry.Properties[property.ToString()];
			object obj = value;
			object obj1 = obj;
			item.Value = obj;
			return (T)obj1;
		}
	}
}