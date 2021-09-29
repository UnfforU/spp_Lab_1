using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLibrary.TracerResult;
using System.Xml.Serialization;
using System.IO;

namespace TracerLibrary.Serializers
{
    public class XMLSerializer : ISerializer
    {

        public string Serialize(TraceResult unput)
        {
            var Serializer = new XmlSerializer(typeof(TraceResult));

            StringWriter result = new StringWriter();
            Serializer.Serialize(result, unput);
            return result.ToString();
        }
    }
}
