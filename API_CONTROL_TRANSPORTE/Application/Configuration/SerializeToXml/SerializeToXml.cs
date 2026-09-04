using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configuration.SerializeToXml
{
    public class SerializeToXml : ISerializeToXml
    {

        public string SerializeToXmlContent<T>(T obj, string element)
        {
            System.Xml.Serialization.XmlSerializerNamespaces namespaces = new System.Xml.Serialization.XmlSerializerNamespaces();
            namespaces.Add("", "");

            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            using (System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(stringBuilder, new System.Xml.XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                xmlSerializer.Serialize(xmlWriter, obj, namespaces);
            }

            string xmlContent = stringBuilder.ToString();

            string resultXML = AddToSoapEnvelope(xmlContent, element);

            return resultXML;
        }

        public T DeserializeXml<T>(string xml)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            T result = default(T);
            using (System.IO.StringReader stringReader = new System.IO.StringReader(xml))
            {
                result = (T)xmlSerializer.Deserialize(stringReader);
            }

            return result;
        }

        public string AddToSoapEnvelope(string xmlContent, string element)
        {
            xmlContent = xmlContent.Replace("<" + element + ">", "<ser:" + element + ">");
            xmlContent = xmlContent.Replace("</" + element + ">", "</ser:" + element + ">");
            StringBuilder soapXml = new StringBuilder();
            soapXml.Append(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ser=""http://service.business.siraf.uif.gob.sv/"">");
            soapXml.Append("<soapenv:Header/>");
            soapXml.Append("<soapenv:Body>");
            soapXml.Append(xmlContent);
            soapXml.Append("</soapenv:Body>");
            soapXml.Append("</soapenv:Envelope>");

            return soapXml.ToString();
        }
    }
}
