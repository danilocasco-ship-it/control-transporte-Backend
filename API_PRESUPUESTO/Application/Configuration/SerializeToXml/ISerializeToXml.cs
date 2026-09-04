using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configuration.SerializeToXml
{
    public interface ISerializeToXml
    {
        string SerializeToXmlContent<T>(T obj, string element);

        T DeserializeXml<T>(string xml);

    }
}
