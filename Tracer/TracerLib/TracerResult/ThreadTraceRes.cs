using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace TracerLibrary.TracerResult
{
    [XmlType("thread")]
    public class ThreadTraceRes 
    {
        private Stack<OneTraceRes> threadTraceStack = null;
        private int time;
        private int threadId;

        [XmlAttribute("time")]
        public int Time { get { return time; } set { time = value; } }

        [XmlAttribute("ThreadId")]
        public int ThreadId { get { return threadId; } set { threadId = value; } }

        [XmlElement("method")]
        public List<OneTraceRes> ListOfMethods
        {
            get
            {
                return ThreadTraceStack.ToList<OneTraceRes>();
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

            foreach(OneTraceRes traceRes in threadTraceStack.Reverse())
            {
                result.threadTraceStack.Push(traceRes.GetValue());
            }
            return result;
        }
    }
}

