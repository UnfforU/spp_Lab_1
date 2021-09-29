using System;
using TracerLibrary.Tracer;
using TracerLibrary.TracerResult;

namespace TracerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = Tracer.getInstance();

            Foo foo = new Foo(tracer);
            Bar bar = new Bar(tracer);

            foo.MyMethod();
            //bar.InnerMethod();


            TraceResult res = tracer.GetTraceResult();


            Console.WriteLine("Hello World!");
        }
    
    
    
    }
}
