using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using TracerLibrary.TracerResult;

namespace TracerLibrary.Tracer
{
    public class Tracer : ITracer
    {
        private static Tracer instance;

        private static object locker = new object();
        private TraceResult traceResult;

        private Stack<Stopwatch> stopwatches = new Stack<Stopwatch>();
        private Dictionary<int, int> countCallThreads = new Dictionary<int, int>();

        public static Tracer getInstance()
        {
            if(instance == null)
                instance = new Tracer();
            return instance;
        }

        public void StartTrace()
        {
            lock (locker)
            {
                stopwatches.Push(new Stopwatch());
                var ThreadId = Thread.CurrentThread.ManagedThreadId;

                if (!countCallThreads.ContainsKey(ThreadId)) { countCallThreads.Add(ThreadId, 0); 
                }
                countCallThreads[ThreadId]++;

                stopwatches.Peek().Start();
            }
        }

        public void StopTrace()
        {
            lock (locker)
            {
                TimeSpan ts = stopwatches.Pop().Elapsed;
                var ThreadId = Thread.CurrentThread.ManagedThreadId;

                var countCall = --countCallThreads[ThreadId];

                var stackTrace = new StackTrace(true);
                var sf = stackTrace.GetFrame(1);
                string method = sf.GetMethod().Name,
                ClassName = sf.GetMethod().DeclaringType.Name;

                int ms = (int)ts.TotalMilliseconds;

                if(traceResult == null)
                {
                    traceResult = new TraceResult();
                }

                traceResult.Push(ClassName, method, ms, countCall, ThreadId);
            }

        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}
