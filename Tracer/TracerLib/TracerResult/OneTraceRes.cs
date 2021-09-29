using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace TracerLibrary.TracerResult
{
    [XmlType("method")]
    public class OneTraceRes
    {
        private Stack<OneTraceRes> stack = new Stack<OneTraceRes>();
        private string _className;
        private string _methodName;
        private int _milliSeconds;

        [XmlIgnore]
        public int CountId;

        [XmlAttribute("class")]
        public string ClassName { get { return _className; } set { _className = value; } }
        [XmlAttribute("methodName")]
        public string MethodName { get { return _methodName; } set { _methodName = value; } }
        [XmlAttribute("time")]
        public int MilliSeconds { get { return _milliSeconds; } set { _milliSeconds = value; } } 
            
        
        public OneTraceRes()
        {


        }

        public OneTraceRes(string className, string methodName, int ms, int countId)
        {
            _className = className;
            _methodName = methodName;
            _milliSeconds = ms;
            CountId = countId;
        }
        
        [XmlElement("method")]
        public List<OneTraceRes> listOfMethods
        {
            get
            {
                return stack?.ToList();
            }
        }

        //Для вложенных функций(по индексам вызова)
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
