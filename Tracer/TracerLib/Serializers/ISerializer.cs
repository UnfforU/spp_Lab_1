using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLibrary.TracerResult;

namespace TracerLibrary.Serializers
{
    public interface ISerializer
    {
        public string Serialize(TraceResult unput);
    }
}
