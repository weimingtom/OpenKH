namespace khkh_xldMii
{
    using System;
    using System.Xml;

    internal class UtXmlGettext
    {
        public static string Select(XmlElement eli, string xp, XmlNamespaceManager xns)
        {
            XmlNode node = eli.SelectSingleNode(xp, xns);
            if (node != null)
            {
                return node.Value;
            }
            return "";
        }
    }
}

