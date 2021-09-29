using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLibrary.Tracer;
using TracerLibrary.TracerResult;
using TracerApp;
using System.Threading;

namespace TracerTests
{
    [TestClass]
    public class TracerTests
    {
        private ITracer tracer;
        private Foo foo;
        private Bar bar;

        [TestInitialize]
        public void Initialize()
        {
            tracer = new Tracer();
            foo = new Foo(tracer);
            bar = new Bar(tracer);
        }


        [TestMethod]
        public void TestOneThreadResult()
        {
            foo.MyMethod();
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(traceResult.threads.Count, 1);
        }

        [TestMethod]
        public void TestMoreThreadResult()
        {
            Thread thread1 = new Thread(foo.MyMethod);
            Thread thread2 = new Thread(foo.MyMethod);
            Thread thread3 = new Thread(foo.MyMethod);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread1.Join();
            thread2.Join();
            thread3.Join();

            foo.MyMethod();


            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(traceResult.threads.Count, 4);
        }

        [TestMethod]
        public void TestThreadId()
        {
            tracer.StartTrace();
            tracer.StopTrace();
            var result = tracer.GetTraceResult();
            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, result.threads[0].ThreadId);
        }

        [TestMethod]
        public void TestClassName()
        {
            bar.InnerMethod();
            var result = tracer.GetTraceResult();

            Assert.AreEqual(nameof(Bar), result.threads[0].ListOfMethods[0].ClassName);
        }
    }


}
