using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging.Timing
{
    public class swatch : IDisposable
    {
        private DateTime _start;   // not exact but ok for now
        private readonly ILogger _logger;
        private LogLevel _logLevel = LogLevel.Debug;
        private string _Name;

        public  swatch(ILogger logger )
        {
            _start = DateTime.Now;
            _logger = logger;

        }
        public swatch(ILogger logger, string name)
        {
            _start = DateTime.Now;
            _logger = logger;
            _Name = name;

        }

        public swatch(ILogger logger, string name, LogLevel logLevel)
        {
            _start = DateTime.Now;
            _logger = logger;
            _logLevel = logLevel;
            _Name = name;

        }




        public void Dispose()
        {

            if (_logger != null)
            {
             
                switch (_logLevel)
                {
                    case LogLevel.Debug:
                        _logger.Info(string.Format("{0}:Time = {1:F5}", _Name, (DateTime.Now - _start).TotalMilliseconds));
                        break;
                    case LogLevel.Info:
                        _logger.Info(string.Format("{0}:Time = {1:F}", _Name, (DateTime.Now - _start).TotalMilliseconds));
                        break;

                }
            }

        }
    }
}
