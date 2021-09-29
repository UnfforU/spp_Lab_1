using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary.TracerResult
{
    public class TraceResult : ITraceResult
    {
        private Dictionary<int, ThreadTraceRes> traceResults = new Dictionary<int, ThreadTraceRes>();

        public List<ThreadTraceRes> AllTraceResult
        {
            get
            {
                List<ThreadTraceRes> ListOfThread = new List<ThreadTraceRes>();


                var allKeys = traceResults.Keys;
                foreach(int i in allKeys)
                {
                    var threadTraceResults = new ThreadTraceRes();
                    threadTraceResults.ThreadTraceStack = CreateStackForOneThread(traceResults[i].ThreadTraceStack);
                    threadTraceResults.ThreadId = i;
                    ListOfThread.Add(threadTraceResults);
                }

                return ListOfThread;

            }
        }

        private Stack<OneTraceRes> CreateStackForOneThread(Stack<OneTraceRes> first)
        {
            var result = new Stack<OneTraceRes>();

            var tempStack = new Stack<OneTraceRes>(first.Reverse());

            if(tempStack.Count <= 0)
            {
                return new Stack<OneTraceRes>();
            }

            result.Push(tempStack.Pop().GetValue());
            while(tempStack.Count > 0)
            {
                var tmpTraceRes = tempStack.Pop().GetValue();
                if (tmpTraceRes.CountId == 0) { result.Push(tmpTraceRes); } else { result.Peek().PushNewTraceRes(tmpTraceRes);  } 
            }
            return result;
        }

        internal void AddResult(string className, string methodName, int msTime, int countId, int ThreadId)
        {
            OneTraceRes oneTraceResult = new OneTraceRes(className, methodName, msTime, countId);
            if (!traceResults.ContainsKey(ThreadId))
            {
                var threadTraceResults = new ThreadTraceRes();
                threadTraceResults.ThreadTraceStack = new Stack<OneTraceRes>();
                traceResults.Add(ThreadId, threadTraceResults);
            }
            traceResults[ThreadId].ThreadTraceStack.Push(oneTraceResult);
        }

        public ITraceResult GetValue()
        {
            TraceResult result = new TraceResult();
            foreach(int i in traceResults.Keys)
            {
                result.traceResults.Add(i, traceResults[i].GetValue());
            }
            return result;
        }
    }
}
