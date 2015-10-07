using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Philips.Logging
{
    public interface ILogger
    {
        LogLevel LogLevel { get; set; }
        
        void Info(string message, [CallerMemberName] string methodName = null,
          [CallerFilePath] string sourceFile = null,
          [CallerLineNumber] int lineNumber = 0);
 
        void Info( int drId, string patId, string studyId, string seriesId, TimeSpan ts, [CallerMemberName] string methodName = null);
       
        void Warning(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0);
        void Error(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0);
        void Debug(string message, [CallerMemberName] string methodName = null,
            [CallerFilePath] string sourceFile = null,
            [CallerLineNumber] int lineNumber = 0);


        void Add(ILogSink logSink);

    }
}
