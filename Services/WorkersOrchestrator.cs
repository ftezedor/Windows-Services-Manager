using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Service.Worker;
using Windows.Service.Logging;

namespace Service.Manager
{
    public class WorkersOrchestrator
    {
        private static Boolean running = false;

        private WorkersOrchestrator()
        {
        }

        public static void start()
        {
            if (running)
                throw new InvalidOperationException("Orchestrator's state is running");

            running = true;

            /* fist, move any file from the staging folder to the the services (workers) folder */
            StagingWatcher.PromoteStagingFiles();

            /* second, load all dlls from services (workers) folder */
            LoadDlls(Directory.GetFiles(ManagerConfig.WorkersFolder, "*.dll"));

            //WorkersController.AddWorker(new StagingWatcher());
            /* finally, get Staging Watcher started */
            WorkersController.AddWorker(StagingWatcher.Instance);
        }

        public static void LoadDll(String dll)
        {
            LoadDlls(new String[] { dll });
        }

        public static void LoadDlls(String[] dlls)
        {
            if (dlls == null || dlls.Length <= 0) return;

            List<Assembly> assemblies = new List<Assembly>();

            foreach (String dll in dlls)
            {
                //AppDomain domain = AppDomain.CreateDomain("TestDomain");
                //Assembly assembly = domain.Load(dll);

                Logger.LogInfo("Loading dll \"" + dll + "\"");

                assemblies.Add(Assembly.LoadFrom(dll));

                foreach (AssemblyName asn in assemblies.Last().GetReferencedAssemblies())
                {
                    Logger.LogInfo(assemblies.Last().FullName + ": " + asn.FullName);
                }
            }

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> types = assembly.GetExportedTypes()
                    .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(ServiceWorker)));
                foreach (Type type in types)
                {
                    ServiceWorker service = (ServiceWorker)Activator.CreateInstance(type);
                    WorkersController.AddWorker(service);
                }
            }
        }

        public static void stop()
        {
            if (!running)
                throw new InvalidOperationException("Orchestrator state is not running");
            WorkersController.StopWorkers();
            running = false;
        }

        public static Boolean Running
        {
            get { return running; }
        }
    }
}
