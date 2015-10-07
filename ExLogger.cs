using System;
using System.Data.SqlClient;
using System.IO;

using System.Text;
using System.Runtime.CompilerServices;

namespace ClinicalStudy.Utils
{


    //todo make this a singleton
    //todo choose of logging desitinations
    //todo hold dr info.
    //todo buffering..?
    //



   

   
   



  

    


    public class ExLogger
    {
        private string LongName = "c:\\temp\\FirstLog.log";
        private FileStream fs = null;
        private Int32 currentPosition = 0;

        public ExLogger()
        {
            if (!File.Exists(LongName))
            {
                File.Create(LongName);
            }
            fs = File.OpenWrite(LongName);
            fs.Position = 0;

        }

        public void Log(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            Console.WriteLine("[{2} -- {0}] - {1} ({3}:{4})",
                methodName, message, DateTime.Now,
                sourceFile, lineNumber);
            string s = string.Format("[{2} -- {0}] - {1} ({3}:{4})",
                methodName, message, DateTime.Now,
                sourceFile, lineNumber);
            Byte[] info = new UTF8Encoding(true).GetBytes(s);
            fs.Write(info, currentPosition, info.Length);

        }
    }

}
