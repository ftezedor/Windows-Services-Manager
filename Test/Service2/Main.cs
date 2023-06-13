using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Service.Worker;
using Windows.Service.Logging;

namespace Service.Worker2
{
    public class Worker2 : ServiceWorker
    {
        private Boolean exit = false;

        public override string Title
        {
            get { return "Worker Tester 2"; }
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
                    // no problem if thread gets awaken
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
