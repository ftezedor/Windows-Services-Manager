using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;

namespace Windows.Service.Worker
{    
    public static class ServicesControl
    {
        private static List<ServiceEntry> services = new List<ServiceEntry>();

        private class ServiceEntry
        {
            private Thread t;
            private ServiceWorker s;

            public ServiceEntry(ServiceWorker service)
            {
                this.s = service;
                this.t = new Thread(new ThreadStart(service.Run));
                this.t.Start();
            }

            public Thread Thread
            {
                get { return t; }
            }
            public ServiceWorker Service
            {
                get { return s; }
            }
        }

        public static void AddService(ServiceWorker service)
        {
            services.Add(new ServiceEntry(service));
        }

        public static void StopServices()
        {
            services.ForEach(entry => {
                entry.Service.Stop();
                entry.Thread.Join();
            });
        }
    }
}
