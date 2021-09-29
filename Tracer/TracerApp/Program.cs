using System;
using System.Threading;
using System.IO;
using TracerLibrary.Tracer;
using TracerLibrary.TracerResult;
using TracerLibrary.Serializers;

namespace TracerApp
{
    class Program
    {
        public static ITracer tracer = Tracer.getInstance();

        static void Main(string[] args)
        {

            Foo foo = new Foo(tracer);
            Bar bar = new Bar(tracer);

            Thread thread1 = new Thread(foo.MyMethod);
            thread1.Start();
            thread1.Join();

            foo.MyMethod();
            bar.InnerMethod();

            Thread thread2 = new Thread(ProgramMethod);
            thread2.Start();
            thread2.Join();

            TraceResult res = tracer.GetTraceResult();

            StrWriter writer = new StrWriter();
            
            ISerializer Serializer = new XMLSerializer();
            string output = Serializer.Serialize(res);
            writer.WriteStream(typeof(FileStream), "res.xml", output);
            writer.WriteStream(typeof(Console), output);
            //Console.WriteLine(output);

            Serializer = new JSONSerializer();
            output = Serializer.Serialize(res);
            writer.WriteStream(typeof(FileStream), "res.json", output);
            writer.WriteStream(typeof(Console), output);
            
        }

        static void ProgramMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(400);
            ProgramMethod2();
            tracer.StopTrace();
        }

        static void ProgramMethod2()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            tracer.StopTrace();
        }
    }
}
