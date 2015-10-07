using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public class DatabaseLogSinkFactroy : AbstractLogSinkFactory
    {
        public override ILogSink Create(string name, ILogger ilogger, string details)
        {
            return new SqlLogSink(name, ilogger) {SqlConnectionString = details};
        }
    }
}
