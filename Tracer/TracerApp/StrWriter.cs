using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TracerApp
{
    public class FileWriter
    {
        private Stream outputStream;

        public Stream OutputStream { get { return outputStream; } set { outputStream = value; } }

        public void WriteStream(Type typeOfStream, string path, string outputText)
        {
            if(typeOfStream == typeof(Console))
            {
                outputStream = Console.OpenStandardOutput();
                Console.WriteLine(outputText);
                return;
            } 

            if(typeOfStream == typeof(FileStream))
            {
                outputStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                var bytes = Encoding.UTF8.GetBytes(outputText);
                outputStream.Write(bytes, 0, bytes.Length);

                outputStream?.Close();
                return;
            }



        }
    }
}
