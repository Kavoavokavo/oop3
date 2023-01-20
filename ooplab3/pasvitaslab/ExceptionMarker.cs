using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasvitaslab
{
    using System;
    class ExceptionMarker : Exception
    {
        public ExceptionMarker(string message) : base(message) { }
    }
}