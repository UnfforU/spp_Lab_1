using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary.TracerResult
{
    public class OneTraceRes
    {
        private Stack<OneTraceRes> stack = new Stack<OneTraceRes>();
        private string _className;
        private string _methodName;
        private int _milliSeconds;

        public int CountId;

        public string ClassName => _className;
        public string MethodName => _methodName;
        public int MilliSeconds => _milliSeconds;

        public OneTraceRes(string className, string methodName, int ms, int countId)
        {
            _className = className;
            _methodName = methodName;
            _milliSeconds = ms;
            CountId = countId;
        }

        public void PushNewTraceRes(OneTraceRes res)
        {
            if (CountId == res.CountId - 1) { stack.Push(res); } else { stack.Peek().PushNewTraceRes(res); }
        }
      
        public OneTraceRes GetValue()
        {
            return new OneTraceRes(_className, _methodName, _milliSeconds, CountId);
        }

    }
}
