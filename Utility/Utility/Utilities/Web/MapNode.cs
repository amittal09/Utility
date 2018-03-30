using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using Vertica.Utilities_v4.Extensions.ObjectExt;

namespace Vertica.Utilities_v4.Web
{
	public class MapNode
	{
		private readonly Uri _url;

		private readonly string _title;

		private readonly XmlNode _innerNode;

		internal XmlNode InnerNode
		{
			get
			{
				return this._innerNode;
			}
		}

		public string Title
		{
			get
			{
				return this._title;
			}
		}

		public Uri Url
		{
			get
			{
				return this._url;
			}
		}

		public MapNode(Uri url, string title)
		{
			this._url = url;
			this._title = title;
		}

		internal MapNode(Uri url, string title, XmlNode innerNode) : this(url, title)
		{
			this._innerNode = innerNode;
		}

		public void AppendAttribute(string name, string value)
		{
			if (this._innerNode == null)
			{
				return;
			}
			XmlAttribute xmlAttribute = this._innerNode.OwnerDocument.CreateAttribute(name);
			xmlAttribute.Value = value;
			this._innerNode.Attributes.Append(xmlAttribute);
		}

		public static XElement Build(Uri url, string title, params XElement[] nodes)
		{
			XName ns = SiteMapBuilder.Ns + SiteMapBuilder.SiteMapNode;
			object[] xAttribute = new object[] { new XAttribute(SiteMapBuilder.Url, url.SafeToString<Uri>(string.Empty)), null, null };
			xAttribute[1] = new XAttribute(SiteMapBuilder.Title, title ?? string.Empty);
			xAttribute[2] = nodes;
			return new XElement(ns, xAttribute);
		}

		public static XElement Build(Uri url, string title, object extraAttributes, params XElement[] nodes)
		{
			XName ns = SiteMapBuilder.Ns + SiteMapBuilder.SiteMapNode;
			object[] xAttribute = new object[] { new XAttribute(SiteMapBuilder.Url, url.SafeToString<Uri>(string.Empty)), null, null };
			xAttribute[1] = new XAttribute(SiteMapBuilder.Title, title ?? string.Empty);
			xAttribute[2] = nodes;
			XElement xElement = new XElement(ns, xAttribute);
			if (extraAttributes != null)
			{
				foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(extraAttributes))
				{
					xElement.SetAttributeValue(property.Name, property.GetValue(extraAttributes));
				}
			}
			return xElement;
		}
	}
}