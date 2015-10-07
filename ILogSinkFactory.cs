using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
   public interface ILogSinkFactory
   {
       ILogSink Create(string p, ILogger iLogger, string details);
   }
}
