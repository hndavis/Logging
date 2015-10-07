using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public class LogSinkFactory
    {
        ILogSink BuildLogSink(LogSinkType sinkType, string name)
        {

            switch (sinkType)
            {
                case LogSinkType.FileName:
                    return new FileLogSink(name);
                   

                case LogSinkType.TableName:
                    return new FileLogSink(name);  //todo 
                   
            }

            return new FileLogSink(name);  //todo 
        }
    }

}
