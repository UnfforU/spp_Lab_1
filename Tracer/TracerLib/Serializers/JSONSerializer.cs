using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLibrary.TracerResult;
using System.Text.Json;
using System.IO;

namespace TracerLibrary.Serializers
{
    public class JSONSerializer : ISerializer
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            AllowTrailingCommas = false,
            WriteIndented = true
        };

        public string Serialize(TraceResult unput)
        {
            return JsonSerializer.Serialize(unput, Options);
        }
    }
}
