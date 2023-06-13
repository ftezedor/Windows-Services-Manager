using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Service.Logging;
using Windows.Service.Worker;

namespace Service.Manager
{
    class WorkersController
    {
        private static List<WorkerEntry> workers = new List<WorkerEntry>();

        private class WorkerEntry
        {
            private AppDomain d;
            private Thread t;
            private ServiceWorker w;

            public WorkerEntry(AppDomain domain, ServiceWorker worker) : this(worker)
            {
                this.d = domain;
            }

            public WorkerEntry(ServiceWorker worker)
            {
                if (AppDomain.CurrentDomain == null)
                    this.d = AppDomain.CurrentDomain;
                this.w = worker;
                this.t = new Thread(new ThreadStart(worker.Run));
                this.t.Start();
            }

            public Thread Thread
            {
                get { return t; }
            }

            public ServiceWorker Worker
            {
                get { return w; }
            }
        }

        public static void AddWorker(AppDomain domain, ServiceWorker service)
        {
            Logger.LogInfo("Starting service '" + service.Title + "'");
            workers.Add(new WorkerEntry(domain, service));
        }

        public static void AddWorker(ServiceWorker service)
        {
            Logger.LogInfo("Starting service '" + service.Title + "'");
            workers.Add(new WorkerEntry(service));
        }

        public static void StopWorkers()
        {
            // ask'em all to stop
            workers.ForEach(entry =>
            {
                    Logger.LogInfo("Asking service '" + entry.Worker.Title + "' to stop");
                    entry.Worker.Stop();
            });

            // wait/monitor 'em all til they're done
            workers.ForEach(entry =>
            {
                if (entry.Thread.IsAlive)
                {
                    Logger.LogInfo("Waiting service '" + entry.Worker.Title + "' to stop");
                    if (entry.Thread.ThreadState == ThreadState.WaitSleepJoin)
                        entry.Thread.Interrupt();
                    entry.Thread.Join();
                    Logger.LogInfo("Service '" + entry.Worker.Title + "' stopped");
                }
            });

            workers.Clear();
        }

    }
}
