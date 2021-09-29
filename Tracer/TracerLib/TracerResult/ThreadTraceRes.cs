using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary.TracerResult
{
    public class ThreadTraceRes 
    {
        private Stack<OneTraceRes> threadTraceStack;
        private int time;
        private int threadId;

        public int Time { get { return time; } set { time = value; } }
        public int ThreadId { get { return threadId; } set { threadId = value; } }

        public List<OneTraceRes> listOfMethods
        {
            get
            {
                return threadTraceStack.ToList<OneTraceRes>();
            }
        }

        internal Stack<OneTraceRes> ThreadTraceStack
        {
            get
            {
                return threadTraceStack;
            }
            set
            {
                threadTraceStack = value;
                foreach(var oneTraceResult in threadTraceStack)
                {
                    if(oneTraceResult.CountId == 0) { 
                        time += oneTraceResult.MilliSeconds; 
                    }
                }
            }
        }

        public ThreadTraceRes GetValue()
        {
            var result = new ThreadTraceRes();
            result.threadId = threadId;
            result.time = time;
            result.threadTraceStack = new Stack<OneTraceRes>();

            foreach(OneTraceRes traceRes in this.threadTraceStack.Reverse())
            {
                result.threadTraceStack.Push(traceRes.GetValue());
            }
            return result;
        }
    }
}

