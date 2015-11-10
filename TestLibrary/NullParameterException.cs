using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    class NullParameterException : ApplicationException
    {
        public NullParameterException() { }
        public NullParameterException(string message) : base(message) { }
        public NullParameterException(string message, Exception inner) : base(message, inner) { }
    }
}
