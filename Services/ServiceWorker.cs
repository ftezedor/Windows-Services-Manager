using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windows.Service.Worker
{
    public abstract class ServiceWorker
    {
        //private Log log;

        public ServiceWorker()
        {
            Initialize();
        }

        ~ServiceWorker()
        {
            Terminate();
        }

        private void Initialize()
        {
            /*
            if (log == null)
            {
                log = new Log(Assembly.GetEntryAssembly().Location + ".log");
                log.Append("Initializing log file");
            }
            */
        }

        private void Terminate()
        {
            /*
            if (log != null)
            {
                log.Append("Closing the log file");
                log.Dispose();
                log = null;
            }
            */
        }

        public abstract String Title
        {
            get;
        }

        public abstract void Run();

        public abstract void Stop();

        protected void LogIt(string msg)
        {
            //log.Append(msg);
            Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + msg);
        }
    }
}
