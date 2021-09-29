using System;
using TracerLibrary.TracerResult;

namespace TracerLibrary.Tracer
{
    public interface ITracer
    {
        //Вызывается в начале замеряемого метода
        public void StartTrace();
        //Вызывается в конце замеряемого метода
        public void StopTrace();
        //Получить результаты измерений
        public TraceResult GetTraceResult();
    }
}
