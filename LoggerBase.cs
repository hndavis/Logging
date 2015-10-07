using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public enum LogLevel { Error = 10, Warning = 20, Info = 30, Debug = 40 };
    public  class Logger: ILogger , IDisposable
    {
        private bool isDisposed = false;
       readonly  List<ILogSink> _sinks = new List<ILogSink>();
        
        public LogLevel LogLevel { get; set; }

        private String FormattedOutput(string message, string methodName = null,
            string sourceFile = null,
             int lineNumber = 0, [CallerMemberName] string logMethodName = null)
        {
            return string.Format("[{2}:{5} - {0}] - {1} ({3}:{4})",
                        methodName, message, DateTime.Now,
                        sourceFile, lineNumber, logMethodName);
        }
        public Logger(ILogSink logSink)
        {
            _sinks.Add(logSink);
           
        }
        public Logger(List<ILogSink> logSinks)
        {
            _sinks.AddRange(logSinks);

        }
        public Logger()
        {
          

        }

        public void Add(ILogSink logSink)
        {
            _sinks.Add(logSink);

        }

        public  void Warning(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel >= LogLevel.Warning)
            {
                foreach (var logSink in _sinks)
                {
                    logSink.Write(FormattedOutput(message, methodName, sourceFile, lineNumber));
                        
                }
              
            }
        }





        public void Error(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel >= LogLevel.Error)
            {
                foreach (var logSink in _sinks)
                {
                    logSink.Write(FormattedOutput(message, methodName, sourceFile, lineNumber));

                }

            }
        }

        public void Info(int drId, string patId, string studyId, string seriesId, TimeSpan ts, [CallerMemberName] string methodName = null)
        {
           
            if (LogLevel >= LogLevel.Info)
            {
                foreach (var logSink in _sinks)
                {
                     logSink.Write(drId,  patId,  studyId, seriesId, ts, methodName);
                }
            }


        }
        public void Info([CallerMemberName] string methodName = null, params Object[] p)
        {
            if (LogLevel >= LogLevel.Info)
            {
                foreach (var logSink in _sinks)
                {
                    logSink.Write(methodName , p);

                }

            } 
        }

        public void Info(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel >= LogLevel.Info)
            {
                foreach (var logSink in _sinks)
                {
                    logSink.Write(FormattedOutput(message, methodName, sourceFile, 
                        lineNumber));

                }


            }
        }


        public void Debug(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (LogLevel <= LogLevel.Debug)
            {
                foreach (var logSink in _sinks)
                {
                    logSink.Write(FormattedOutput(message, methodName, sourceFile, lineNumber));

                }

            }
        }

        public void Dispose()
        {
           Dispose(true);
        }

        public void Dispose( bool isDisposing)
        {
            if (!isDisposed)
            {
                foreach (var logSink in _sinks)
                {
                    logSink.Dispose();
                }
            }
            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }

        }

        ~Logger()
        {
            Dispose(false);
        }


    }
}
