using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public abstract class AbstractLogSinkFactory : ILogSinkFactory
    {
      //public  abstract ILogSinkFactory CreateLogSinkFactory();

        public abstract ILogSink Create(string p, ILogger iLogger, string details);

    }
}
