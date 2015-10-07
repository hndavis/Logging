using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philips.Logging
{
    public enum LogSinkType { TableName, FileName }

    public interface ILogSink : IDisposable
    {
        void Open();
        void Flush();
        void BufferSize(int i);
        void Write(params object[] l );
        Boolean IsOpen { get; }
       

    }

}
