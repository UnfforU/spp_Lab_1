using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLibrary.Tracer;
using System.Threading;

namespace TracerApp
{
    public class Bar
    {
        private ITracer _tracer;

        public Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(2000);
            _tracer.StopTrace();
        }
    }
}
