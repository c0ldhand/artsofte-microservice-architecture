using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.TraceLogic.Interfaces
{
    public interface ITraceWriter
    {
        string Name { get; }

        string GetValue();
    }
}
