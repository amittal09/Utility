using System;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.CompilerServices;
using Vertica.Utilities_v4.Extensions.EnumerableExt;

namespace Vertica.Utilities_v4.DirectoryServices
{
	public static class DirectorySearcherExtensions
	{
		private readonly static TimeSpan twoSeconds;

		static DirectorySearcherExtensions()
		{
			DirectorySearcherExtensions.twoSeconds = TimeSpan.FromSeconds(2);
		}

		public static DirectoryEntryCollection FindAllOf(this DirectorySearcher ds, AdObjectCategory category)
		{
			string str = "(objectCategory={0})";
			ds.SearchScope = SearchScope.Subtree;
			ds.PropertiesToLoad.Add(AdProperty.name.ToString());
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, category);
			SearchResultCollection searchResultCollections = ds.FindAll();
			DirectoryEntryCollection directoryEntryCollections = new DirectoryEntryCollection(searchResultCollections.Count);
			searchResultCollections.Cast<SearchResult>().ForEach<SearchResult>((SearchResult result) => directoryEntryCollections.Add(result.GetDirectoryEntry()));
			return directoryEntryCollections;
		}

		public static DirectoryEntry FindGroup(this DirectorySearcher ds, string groupName)
		{
			string str = "(&(objectCategory=group)(name={0}))";
			ds.SearchScope = SearchScope.Subtree;
			ds.PropertiesToLoad.Add(AdProperty.name.ToString());
			ds.PageSize = 1;
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, groupName);
			SearchResult searchResult = ds.FindOne();
			if (searchResult == null)
			{
				return null;
			}
			return searchResult.GetDirectoryEntry();
		}

		public static DirectoryEntry FindOu(this DirectorySearcher ds, string ouName)
		{
			string str = "(&(objectCategory=organizationalUnit)(name={0}))";
			ds.SearchScope = SearchScope.Subtree;
			StringCollection propertiesToLoad = ds.PropertiesToLoad;
			string[] strArrays = new string[] { AdProperty.ou.ToString(), AdProperty.name.ToString(), AdProperty.displayName.ToString() };
			propertiesToLoad.AddRange(strArrays);
			ds.PageSize = 1;
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, ouName);
			SearchResult searchResult = ds.FindOne();
			if (searchResult == null)
			{
				return null;
			}
			return searchResult.GetDirectoryEntry();
		}

		public static DirectoryEntry FindUser(this DirectorySearcher ds, string samAccountName)
		{
			string str = "(&(objectCategory=user)(samAccountName={0}))";
			ds.SearchScope = SearchScope.Subtree;
			ds.PropertiesToLoad.Add(AdProperty.samAccountName.ToString());
			ds.PageSize = 1;
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, samAccountName);
			SearchResult searchResult = ds.FindOne();
			if (searchResult == null)
			{
				return null;
			}
			return searchResult.GetDirectoryEntry();
		}

		public static DirectoryEntryCollection GetAllOf(this DirectorySearcher ds, AdObjectCategory category)
		{
			string str = "(objectCategory={0})";
			ds.SearchScope = SearchScope.OneLevel;
			StringCollection propertiesToLoad = ds.PropertiesToLoad;
			string[] strArrays = new string[] { AdProperty.name.ToString(), AdProperty.displayName.ToString() };
			propertiesToLoad.AddRange(strArrays);
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, category);
			SearchResultCollection searchResultCollections = ds.FindAll();
			DirectoryEntryCollection directoryEntryCollections = new DirectoryEntryCollection(searchResultCollections.Count);
			searchResultCollections.Cast<SearchResult>().ForEach<SearchResult>((SearchResult result) => directoryEntryCollections.Add(result.GetDirectoryEntry()));
			return directoryEntryCollections;
		}

		public static DirectoryEntry GetGroup(this DirectorySearcher ds, string groupName)
		{
			string str = "(&(objectCategory=group)(name={0}))";
			ds.SearchScope = SearchScope.OneLevel;
			ds.PropertiesToLoad.Add(AdProperty.name.ToString());
			ds.PageSize = 1;
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, groupName);
			SearchResult searchResult = ds.FindOne();
			if (searchResult == null)
			{
				return null;
			}
			return searchResult.GetDirectoryEntry();
		}

		public static DirectoryEntry GetOu(this DirectorySearcher ds, string ouName)
		{
			string str = "(&(objectCategory=organizationalUnit)(name={0}))";
			ds.SearchScope = SearchScope.OneLevel;
			StringCollection propertiesToLoad = ds.PropertiesToLoad;
			string[] strArrays = new string[] { AdProperty.ou.ToString(), AdProperty.name.ToString(), AdProperty.displayName.ToString() };
			propertiesToLoad.AddRange(strArrays);
			ds.PageSize = 1;
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, ouName);
			SearchResult searchResult = ds.FindOne();
			if (searchResult == null)
			{
				return null;
			}
			return searchResult.GetDirectoryEntry();
		}

		public static DirectoryEntry GetUser(this DirectorySearcher ds, string samAccountName)
		{
			string str = "(&(objectCategory=user)(samAccountName={0}))";
			ds.SearchScope = SearchScope.OneLevel;
			ds.PropertiesToLoad.Add(AdProperty.samAccountName.ToString());
			ds.PropertiesToLoad.Add(AdProperty.displayName.ToString());
			ds.PropertiesToLoad.Add(AdProperty.mail.ToString());
			ds.PageSize = 1;
			ds.ServerPageTimeLimit = DirectorySearcherExtensions.twoSeconds;
			ds.Filter = string.Format(str, samAccountName);
			SearchResult searchResult = ds.FindOne();
			if (searchResult == null)
			{
				return null;
			}
			return searchResult.GetDirectoryEntry();
		}
	}
}