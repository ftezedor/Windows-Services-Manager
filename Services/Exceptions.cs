using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Manager.Exceptions
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException()
        {
        }

        public ServiceNotFoundException(string message)
            : base(message)
        {
        }

        public ServiceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
