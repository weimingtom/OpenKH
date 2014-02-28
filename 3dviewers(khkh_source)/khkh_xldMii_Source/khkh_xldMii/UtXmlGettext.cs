using System;
using System.Xml;
namespace khkh_xldMii
{
	internal class UtXmlGettext
	{
		public static string Select(XmlElement eli, string xp, XmlNamespaceManager xns)
		{
			XmlNode xmlNode = eli.SelectSingleNode(xp, xns);
			if (xmlNode != null)
			{
				return xmlNode.Value;
			}
			return "";
		}
	}
}
