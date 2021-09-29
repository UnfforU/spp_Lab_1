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
                    if(oneTraceResult.CountId == 0) { time += oneTraceResult.MilliSeconds; }
                }
            }
        }

        public ThreadTraceRes GetValue()
        {

            //Написать код, как возвращать копию
            return new ThreadTraceRes();
        }
    }
}

