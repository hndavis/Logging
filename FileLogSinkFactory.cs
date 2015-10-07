using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public class FileLogSinkFactory : AbstractLogSinkFactory
    {
        public  override ILogSink Create(string name, ILogger iLogger, string directoryForLogs)
        {
            var fls = new FileLogSink(name) {LogFileDirectory = directoryForLogs};
            return fls;
        }

       

    }
}
