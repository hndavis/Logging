using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public abstract class LogSinkBase : ILogSink
    {
       
        public abstract void Open();

        public abstract void Flush();

        public virtual void BufferSize(int i)
        {
            
        }

        public abstract bool IsOpen { get;  }


        public abstract void Write( object[] l);

        public abstract void Dispose();
        


    }
}
