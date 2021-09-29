using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary.TracerResult
{
    public class TraceResult : ITraceResult
    {
        private Dictionary<int, ThreadTraceRes> TraceResults = new Dictionary<int, ThreadTraceRes>();

        public List<ThreadTraceRes> Threads
        {
            get
            {
                List<ThreadTraceRes> ListOfThread = new List<ThreadTraceRes>();


                var allKeys = TraceResults.Keys;
                foreach(int i in allKeys)
                {
                    var threadTraceResults = new ThreadTraceRes();
                    
                    //threadTraceResults
                }

                return ListOfThread;

            }
        }

        internal void Push(string className, string methodName, int msTime, int countId, int ThreadId)
        {
            OneTraceRes oneTraceResult = new OneTraceRes(className, methodName, msTime, countId);
            if (!TraceResults.ContainsKey(ThreadId))
            {
                var threadTraceResults = new ThreadTraceRes();
            }
        }

        public ITraceResult GetValue()
        {
            TraceResult result = new TraceResult();
            foreach(int i in TraceResults.Keys)
            {
                result.TraceResults.Add(i, TraceResults[i].GetValue());
            }
            return result;
        }
    }
}
