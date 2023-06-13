using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Service.Worker;
using Windows.Service.Logging;

namespace Service.Worker1
{
    public class Worker1 : ServiceWorker
    {
        private Boolean exit = false;

        public override string Title
        {
            get { return "Worker Tester 1"; }
        }

        public override void Run()
        {
            Logger.LogInfo("starting working");
            //LogIt("starting working");
            while (!exit)
            {
                Logger.LogInfo("performing some work...");
                try
                {
                    System.Threading.Thread.Sleep(14000);
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                    // no problem if thread gets interrupted while sleeping
                }
            }
            //LogIt("work is done");
            Logger.LogInfo("work is done");
        }

        public override void Stop()
        {
            exit = true;
        }
    }
}
