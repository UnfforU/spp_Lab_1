using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private static object locker = new object();
        private TraceResult traceResult;

        private Dictionary<int, Stack<Stopwatch>> Threads= new Dictionary<int, Stack<Stopwatch>>();



        public TraceResult GetTraceResult()
        {
            throw new NotImplementedException();
        }

        public void StartTrace()
        {
            lock (locker)
            {
                var sw = new Stopwatch();

                var id = Thread.CurrentThread.ManagedThreadId;

                if (!Threads.ContainsKey(id))
                {
                    Threads.Add(id, new Stack<Stopwatch>());
                }

                Threads[id].Push(sw);
                Threads[id].Peek().Start();
            }
        }

        public void StopTrace()
        {
            lock (locker)
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                TimeSpan ts = Threads.GetValueOrDefault(id).Pop().Elapsed;

                var stackTrace = new StackTrace(true);
                var sf = stackTrace.GetFrame(1);
                string method = sf.GetMethod().Name,
                ClassName = sf.GetMethod().DeclaringType.Name;

                long tick = ts.Ticks;
                int ms = (int)ts.TotalMilliseconds;

                if(traceResult == null)
                {
                    traceResult = new TraceResult();
                }



            }
        }
    }
}
