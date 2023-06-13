using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using Windows.Service.Logging;

namespace Service.Manager
{
    class DebugConsole
    {
        public static void run()
        {
            ServiceControllerStatus svs = ServiceControllerStatus.Stopped;
            try
            {
                svs = ServiceManager.Status;
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
            }

            if (svs != ServiceControllerStatus.Stopped)
            {
                Console.WriteLine("To run this application in debug mode, the service should be stopped. Close this window, stop the service, and launch this application again.");
                return;
            }

            if (!WorkersOrchestrator.Running)
                WorkersOrchestrator.start();

            Console.WriteLine("\n" + ServiceManager.Name + 
                "\nCopyright (c) 2023, Fabio Tezedor" + 
                "\n\nThis program performs in application mode the same routines that are perfomed as service\n\nPress Esc or Ctrl+C to exit");

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape || (cki.Key == ConsoleKey.C && cki.Modifiers == ConsoleModifiers.Control))
                {
                    break;
                }
            }

            if (WorkersOrchestrator.Running)
                WorkersOrchestrator.stop();
        }
    }
}
