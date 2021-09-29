using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TracerApp
{
    public class StrWriter
    {
        private Stream outputStream;

        public Stream OutputStream { get { return outputStream; } set { outputStream = value; } }
        
        public void WriteStream(Type console, string outputText)
        {
            if (console == typeof(Console))
            {
                outputStream = Console.OpenStandardOutput();
                Console.WriteLine(outputText);
                return;
            }
        }

        public void WriteStream(Type typeOfFileStream, string outputText, string path)
        {
            if(typeOfFileStream == typeof(FileStream))
            {
                outputStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                var bytes = Encoding.UTF8.GetBytes(outputText);
                outputStream.Write(bytes, 0, bytes.Length);

                outputStream.Close();
                return;
            }
        }
    }
}
