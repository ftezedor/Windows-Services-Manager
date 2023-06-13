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
    class StagingWatcher : Windows.Service.Worker.ServiceWorker
    {
        private static StagingWatcher instance = null;

        private Boolean exit = false;

        private StagingWatcher()
        {
        }

        public override string Title
        {
            get { return "Staging Watcher"; }
        }

        /// <summary>
        /// Return the single instance of the StagingWatcher class
        /// </summary>
        public static StagingWatcher Instance
        {
            get
            {
                if (instance == null) 
                    instance = new StagingWatcher();
                return instance;
            }
        }

        public override void Run()
        {
            String[] files;

            while (!exit)
            {
                files = Directory.GetFiles(ManagerConfig.StagingFolder, "*.*");

                if (files.Length > 0)
                    HandleNewcomerFiles(files);

                /* rest for minute before checking staging folder again */
                try
                {
                    System.Threading.Thread.Sleep(60000);
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                    // no problem if thread gets taken out from its nap
                }
            }
        }

        /// <summary>
        /// move all files found in the staging folder to the services folder and load the dll ones
        /// </summary>
        /// <param name="files"></param>
        private void HandleNewcomerFiles(String[] files)
        {
            if (files == null || files.Length == 0)
                return;

            files = files.Where(file =>
            {
                Logger.LogInfo("found file " + file);

                String target = file.Replace(ManagerConfig.StagingFolder, ManagerConfig.WorkersFolder);

                if (File.Exists(target))
                {
                    Logger.LogWarning("File " + target + " already exists and can not be replaced by " + file + " at this moment");
                    Logger.LogWarning("Moving file " + file + " into the folder " + ManagerConfig.RejectsFolder);
                    target = file.Replace(ManagerConfig.StagingFolder, ManagerConfig.RejectsFolder);
                }
                else
                {
                    Logger.LogInfo("Moving " + file + " into the services folder");
                }

                if (File.Exists(target))
                    File.Delete(target);

                File.Move(file, target);

                return !target.Contains(ManagerConfig.RejectsFolder);
            }).Where(x => x.EndsWith(".dll"))
            .Select(x => x.Replace(ManagerConfig.StagingFolder, ManagerConfig.WorkersFolder))
            .ToArray<String>();

            WorkersOrchestrator.LoadDlls(files);
        }

        public static void PromoteStagingFiles()
        {
            if (instance != null)
                throw new InvalidOperationException("Illegal state: Staging Watcher is running");

            /* move all files found in the taging folder to the services folder */
            String[] files = Directory.GetFiles(ManagerConfig.StagingFolder, "*.*");

            foreach (String file in files)
            {
                Logger.LogInfo("Found file " + file);
                
                String target = file.Replace(ManagerConfig.StagingFolder, ManagerConfig.WorkersFolder);

                if (File.Exists(target))
                    Logger.LogInfo("Replacing " + target + " by " + file );
                else
                    Logger.LogInfo("Moving " + file + " into the services folder");

                if (File.Exists(target))
                    File.Delete(target);

                File.Move(file, target);
            }
        }

        public override void Stop()
        {
            exit = true;
        }
    }
}
