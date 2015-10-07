using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public class FileLogSink : LogSinkBase, IDisposable
    {
        private FileStream fs = null;
        private Int32 currentPosition = 0;
        private string _logName;
        private string _name;
        private bool disposed = false;
        
        public string LogFileDirectory { get; set; }

        public FileLogSink(string name)
        {
            _name = name;
        }

        public override bool IsOpen
        {
            get
            {
                if (fs == null)
                    return false;

                if ( fs == Stream.Null )
                    return false;

                return fs.CanWrite;
            }
        }


        public override void Open()
        {
            if ( string.IsNullOrEmpty(LogFileDirectory))
                throw new Exception("Log File Directory not set.");
           // string logDir = ConfigurationManager.AppSettings.Get(Constants.LOG_FILE_DIR);
            _logName = string.Format("{0}\\{1}{2}{3}{4}_{5}_{6}_{7}.log", LogFileDirectory, _name, DateTime.Now.Year,
                DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"),
                DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"),
                DateTime.Now.Second.ToString("D2"));

            try
            {
                fs = !File.Exists(_logName) ? File.Create(_logName) : File.OpenWrite(_logName);
                fs.Position = 0;
               
            }
            catch (Exception exFile)
            { 
                // for debug
                var m = exFile.Message;
                throw;
            }
        }


    

        public override void Write(params object[] l)

        {
            if (l.Length == 1)
            {
                string s = (string) l[0];
                if (!IsOpen)
                    Open();
                Byte[] info = new UTF8Encoding(true).GetBytes(s);
                fs.Write(info, currentPosition, info.Length);
                string cr = Environment.NewLine;
                Byte[] info2 = new UTF8Encoding(true).GetBytes(cr);
                fs.Write(info2, currentPosition, info2.Length);
            }
            else if (l.Length == 5)
            {
                var logTime = string.Format("{0}{1}{2}_{3}_{4}_{5}.log", DateTime.Now.Year,
                DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"),
                DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"),
                DateTime.Now.Second.ToString("D2"));

                string s = string.Format("{0} D={1} PID={2} Ind={3} Event={4} tsoff={5} ",

                    logTime,  (int) l[0], (string) l[1],
                        (string) l[2], (string) l[4], (TimeSpan) l[3]);
                if (!IsOpen)
                    Open();
                Byte[] info = new UTF8Encoding(true).GetBytes(s);
                fs.Write(info, currentPosition, info.Length);
                string cr = Environment.NewLine;
                Byte[] info2 = new UTF8Encoding(true).GetBytes(cr);
                fs.Write(info2, currentPosition, info2.Length);

            }
            else if (l.Length == 7)
            {
                var logTime = string.Format("{0}{1}{2}_{3}_{4}_{5}.log", DateTime.Now.Year,
                DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"),
                DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"),
                DateTime.Now.Second.ToString("D2"));


                string s = string.Format("{0} D={1} PID={2} Ind={3} Event={4} tsoff={5} BtnName={6}-{7} ",
                                logTime, (int)l[0], (string)l[1],
                                (string)l[2], (string)l[4], (TimeSpan)l[3], l[5], l[6]);
                if (!IsOpen)
                    Open();
                Byte[] info = new UTF8Encoding(true).GetBytes(s);
                fs.Write(info, currentPosition, info.Length);
                string cr = Environment.NewLine;
                Byte[] info2 = new UTF8Encoding(true).GetBytes(cr);
                fs.Write(info2, currentPosition, info2.Length);

            }


        }

        public void Write(int drId, string patId, string index, TimeSpan ts, string methodName)
        {
             var logTime = string.Format("{0}{1}{2} {3}:{4}:{5}", DateTime.Now.Year,
                DateTime.Now.Month.ToString("D2"), DateTime.Now.Day.ToString("D2"),
                DateTime.Now.Hour.ToString("D2"), DateTime.Now.Minute.ToString("D2"),
                DateTime.Now.Second.ToString("D2"));

            string s = string.Format("{0} D={1} PID={2} Ind={3} Event={4} tsoff={5} ",
                logTime, drId, patId, index, methodName, ts);
            if (!IsOpen)
                Open();
            Byte[] info = new UTF8Encoding(true).GetBytes(s);
            fs.Write(info, currentPosition, info.Length);
            string cr = Environment.NewLine;
            Byte[] info2 = new UTF8Encoding(true).GetBytes(cr);
            fs.Write(info2, currentPosition, info2.Length);

        }

        public override void Flush()

        {
            try
            {
                 if ( fs.CanWrite)
                    fs.Flush();

            }
            catch (System.ObjectDisposedException e)
            {
                if (e.Message != "System.ObjectDisposedException")
                throw;
            }
           
            
        }



        public  override void Dispose()
        {
            Close();
        }

        public void Close()
        {
            Dispose(true);
           
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ( disposing)
            {
                if (!disposed)
                {
                    Flush();
                    if (fs != null)
                    {
                        fs.Close();
                        fs = null;
                    }
                    disposed = true;
         
                }
            }


        }

        ~FileLogSink()
        {
            Dispose(false);
        }
    }
}
