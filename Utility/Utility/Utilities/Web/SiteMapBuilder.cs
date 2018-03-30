using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Extensions.ObjectExt;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4.Web
{
	public class SiteMapBuilder
	{
		internal readonly static string SiteMapSchema;

		internal readonly static string SiteMap;

		internal readonly static string SiteMapNode;

		internal readonly static string Url;

		internal readonly static string Title;

		private XmlDocument _dom;

		private XmlNamespaceManager _nsManager;

		public readonly static string DefaultBackupExtension;

		internal readonly static XNamespace Ns;

		public XPathNavigator Navigator
		{
			get
			{
				return this._dom.CreateNavigator();
			}
		}

		public string RawXml
		{
			get
			{
				return this._dom.OuterXml;
			}
		}

		static SiteMapBuilder()
		{
			SiteMapBuilder.SiteMapSchema = "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0";
			SiteMapBuilder.SiteMap = "siteMap";
			SiteMapBuilder.SiteMapNode = "siteMapNode";
			SiteMapBuilder.Url = "url";
			SiteMapBuilder.Title = "title";
			SiteMapBuilder.DefaultBackupExtension = ".old";
			SiteMapBuilder.Ns = SiteMapBuilder.SiteMapSchema;
		}

		public SiteMapBuilder()
		{
		}

		private void appendAttribute(XmlNode element, string name, string value)
		{
			XmlAttribute xmlAttribute = this._dom.CreateAttribute(name);
			xmlAttribute.Value = value;
			element.Attributes.Append(xmlAttribute);
		}

		public MapNode AppendNode(MapNode parent, Uri url, string title)
		{
			if (parent.InnerNode == null)
			{
				string siteMapBuilderNoContextParentTemplate = Exceptions.SiteMapBuilder_NoContextParentTemplate;
				string[] name = new string[] { typeof(SiteMapBuilder).Name };
				ExceptionHelper.ThrowArgumentException("parent", siteMapBuilderNoContextParentTemplate, name);
			}
			XmlNode xmlNodes = this.createNode(url, title);
			parent.InnerNode.AppendChild(xmlNodes);
			return new MapNode(url, title, xmlNodes);
		}

		private void backupExistingFile(string fileName, string backupExtension)
		{
			if (File.Exists(fileName))
			{
				string str = string.Concat(fileName, this.ensureSingledot(backupExtension));
				File.Copy(fileName, str, true);
			}
		}

		public static XDocument Build(params XElement[] nodes)
		{
			XDeclaration xDeclaration = new XDeclaration("1.0", "utf-8", null);
			object[] xElement = new object[] { new XElement(SiteMapBuilder.Ns + "siteMap", nodes) };
			return new XDocument(xDeclaration, xElement);
		}

		private void checkBeforeSave()
		{
			if (this._dom == null)
			{
				throw new NotSupportedException(Exceptions.SiteMapBuilder_SaveBeforeCreate);
			}
		}

		public MapNode Create(Uri rootUrl, string rootTitle)
		{
			this.setRoot();
			return new MapNode(rootUrl, rootTitle, this.setRootNode(rootUrl, rootTitle));
		}

		private XmlElement createNode(Uri url, string title)
		{
			XmlElement xmlElement = this._dom.CreateElement(SiteMapBuilder.SiteMapNode, SiteMapBuilder.SiteMapSchema);
			this.appendAttribute(xmlElement, SiteMapBuilder.Url, url.SafeToString<Uri>(null));
			this.appendAttribute(xmlElement, SiteMapBuilder.Title, title);
			return xmlElement;
		}

		private string ensureSingledot(string extension)
		{
			if (extension.StartsWith("."))
			{
				return extension;
			}
			return string.Concat(".", extension);
		}

		public void Save(string fileName, bool performBackup, string backupExtension)
		{
			this.checkBeforeSave();
			if (performBackup)
			{
				this.backupExistingFile(fileName, backupExtension);
			}
			this._dom.Save(fileName);
		}

		public void Save(string fileName, bool performBackup)
		{
			this.Save(fileName, performBackup, SiteMapBuilder.DefaultBackupExtension);
		}

		public void Save(string fileName)
		{
			this.Save(fileName, false, SiteMapBuilder.DefaultBackupExtension);
		}

		private void setRoot()
		{
			this._dom = new XmlDocument();
			this._dom.AppendChild(this._dom.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, null));
			this._dom.AppendChild(this._dom.CreateElement(SiteMapBuilder.SiteMap, SiteMapBuilder.SiteMapSchema));
			this._nsManager = new XmlNamespaceManager(this._dom.NameTable);
			this._nsManager.AddNamespace("s", SiteMapBuilder.SiteMapSchema);
		}

		private XmlNode setRootNode(Uri url, string title)
		{
			XmlNode xmlNodes = this.createNode(url, title);
			this._dom.DocumentElement.AppendChild(xmlNodes);
			return xmlNodes;
		}
	}
}