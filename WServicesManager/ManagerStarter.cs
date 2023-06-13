using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Manager;

namespace Service.Manager
{
    class ManagerStarter
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                CommandLineArgumentsHelper clargs = new CommandLineArgumentsHelper(args);

                clargs.Validate();

                // load config file and get config manager ready
                if (!String.IsNullOrWhiteSpace(clargs.ConfigurationFile))
                {
                    ManagerConfig.Initialize(clargs.ConfigurationFile);
                    /* set the workng directory to the same where the config file is */
                    string wdir = System.IO.Directory.GetParent(ManagerConfig.ConfigurationFile).FullName;
                    System.IO.Directory.SetCurrentDirectory(wdir);
                }

                switch (clargs.Action)
                {
                    case "install":
                        System.Windows.Forms.Application.Run(new InstallForm());
                        break;
                    // option for Windows Process Launcher
                    case "service":
                        ServiceManager.Launch();
                        break;
                    case "configure":
                        System.Windows.Forms.Application.Run(new ConfigManager());
                        break;
                    case "start":
                        ServiceManager.Start();
                        break;
                    case "stop":
                        ServiceManager.Stop();
                        break;
                    case "restart":
                        ServiceManager.Restart();
                        break;
                    case "status":
                        DisplayMessage("Service is " + ServiceManager.Status.ToString());
                        break;
                    case "debug":
                        #if DEBUG
                        System.Windows.Forms.Application.Run(new DebugPanel());
                        break;
                        #else
                        throw new ArgumentException("'debug' option is not permitted at this time");
                        #endif
                    case "?":
                    case "h":
                    case "help":
                        DisplayMessage(HelpMessage);
                        break;
                    default:
                        throw new ArgumentException("invalid argument: " + clargs.Action);
                }
            }
            catch (Service.Manager.Exceptions.ServiceNotFoundException)
            {
                if (!Environment.UserInteractive) throw;
                DisplayMessage("Service is not installed"); ;
            }
             catch (Exception ex)
            {
                if (!Environment.UserInteractive) throw;
                DisplayMessage(ex.Message);
            }
        }

        private static void DisplayMessage(String msg)
        {
            if (Environment.UserInteractive)
                MessageBox.Show(msg, ManagerConfig.ServiceName == null ? "Windows Service Manager" : ManagerConfig.ServiceName);
        }

        private static String HelpMessage
        {
            get
            {
                return "Usage: WServicesManager.exe <action> /config <file>\r\n\r\n" +
                       "       actions:\r\n" +
                       "           /start      - start the service\r\n" +
                       "           /stop       - stop the service\r\n" +
                       "           /restart    - restart the service\r\n" +
                       "           /status     - get the status of the service\r\n" +
                       "           /install    - install this service (default)\r\n" +
                       "           /configure  - allow the creation of the configuration file\r\n" +
                       "           /debug      - run the service as a regular program\r\n" +
                       "           /help       - print this message";
            }
        }
    }
}
