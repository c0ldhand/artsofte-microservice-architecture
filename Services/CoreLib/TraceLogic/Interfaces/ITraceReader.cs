using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.TraceLogic.Interfaces
{
    public interface ITraceReader
    {
        string Name { get; }

        void WriteValue(string value);
    }
}
