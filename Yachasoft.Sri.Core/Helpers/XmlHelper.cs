using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Yachasoft.Sri.Core.Helpers
{
    public static class XmlHelper
    {
        public static XmlDocument ToXmlDocument<T>(this T source, Type[] extraTypes = null)
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new XmlSerializer(typeof(T), extraTypes).Serialize((Stream)memoryStream, (object)source);
                memoryStream.Position = 0L;
                XmlReaderSettings settings = new XmlReaderSettings()
                {
                    IgnoreWhitespace = true
                };
                using (XmlReader reader = XmlReader.Create((Stream)memoryStream, settings))
                    xmlDocument.Load(reader);
            }
            return xmlDocument;
        }

        public static XmlDocument ToXmlDocument(this Stream stream)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);
            return xmlDocument;
        }

        public static XmlDocument ToXmlDocument(this string xmlString)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            return xmlDocument;
        }

        public static T XmlDeserialize<T>(this XmlDocument objectData, Type[] extraTypes = null)
        {
            object obj = (object)null;
            if (objectData.ChildNodes.Count > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(objectData.InnerXml.ToString())))
                {
                    obj = new XmlSerializer(typeof(T), extraTypes).Deserialize((Stream)memoryStream);
                    memoryStream.Close();
                }
            }
            return (T)obj;
        }
    }
}
