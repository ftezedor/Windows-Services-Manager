using System;
using System.IO;
using System.ServiceProcess;
using Service.Manager.Exceptions;

namespace Service.Manager
{
	public class ServiceManager : ServiceBase
	{
		public delegate void AfterInstall();
		public static event AfterInstall OnInstall;
		public delegate void AfterUninstall();
		public static event AfterUninstall OnUninstall;

		//private String appPath;

		public ServiceManager()
		{
			InitializeComponent();

			//appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			//appPath = appPath.Substring( 0, appPath.LastIndexOf("\\") );
		}


		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			//components = new System.ComponentModel.Container();
			this.ServiceName = ServiceManager.Name;
			this.CanStop = true;
			this.CanPauseAndContinue = false;
			this.AutoLog = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
            WorkersOrchestrator.start();
		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
            WorkersOrchestrator.stop();
		}

		public static string Name
		{
            get { return ManagerConfig.ServiceName == null ? "Windows Services Manager" : ManagerConfig.ServiceName; }
		}

		#region methods that expose static functionalities
		public static void Launch()
		{
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] { new ServiceManager() };
			ServiceBase.Run(ServicesToRun);
		}

		public static void ClearEvents()
		{
			if ( OnInstall != null ) OnInstall = null;
			if ( OnUninstall != null ) OnUninstall = null;
		}

		public static void Install( String name, String password )
		{
			System.Configuration.Install.TransactedInstaller ti = new System.Configuration.Install.TransactedInstaller();
			ti.AfterInstall += new System.Configuration.Install.InstallEventHandler(ti_AfterInstall);
			ServiceInstaller si = new ServiceInstaller();
			si.SetUser( name, password );
			ti.Installers.Add(si);
			String basePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			String path = String.Format("/assemblypath=\"{0}\"",basePath);
			System.Configuration.Install.InstallContext ctx = null;
			//ctx = new System.Configuration.Install.InstallContext( Path.ChangeExtension(basePath,".InstallLog"), new String[] {path} );
            ctx = new System.Configuration.Install.InstallContext(System.IO.Directory.GetCurrentDirectory() + "\\service.InstallLog", new String[] { path });
			ti.Context = ctx;
			ti.Install( new System.Collections.Hashtable() );
		}

		public static void Install()
		{
			System.Configuration.Install.TransactedInstaller ti = new System.Configuration.Install.TransactedInstaller();
			ti.AfterInstall += new System.Configuration.Install.InstallEventHandler(ti_AfterInstall);
			ServiceInstaller si = new ServiceInstaller();
			ti.Installers.Add(si);
			String basePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			String path = String.Format("/assemblypath=\"{0}\"",basePath);
			System.Configuration.Install.InstallContext ctx = null;
			//ctx = new System.Configuration.Install.InstallContext( Path.ChangeExtension(basePath,".InstallLog"), new String[] {path} );
            ctx = new System.Configuration.Install.InstallContext(System.IO.Directory.GetCurrentDirectory() + "\\service.InstallLog", new String[] { path });
			ti.Context = ctx;
			ti.Install( new System.Collections.Hashtable() );
		}

		private static void ti_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
		{
			if ( OnInstall != null ) OnInstall();
		}

		public static void Uninstall()
		{
			System.Configuration.Install.TransactedInstaller ti = new System.Configuration.Install.TransactedInstaller();
			ti.AfterUninstall += new System.Configuration.Install.InstallEventHandler(ti_AfterUninstall);
			ServiceInstaller pi = new ServiceInstaller();
			ti.Installers.Add(pi);
			String basePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
			String path = String.Format("/assemblypath=\"{0}\"",basePath);
			System.Configuration.Install.InstallContext ctx = null;
			//ctx = new System.Configuration.Install.InstallContext( Path.ChangeExtension(basePath,".UninstallLog"), new String[] {path,ServiceManager.Name} );
            ctx = new System.Configuration.Install.InstallContext(System.IO.Directory.GetCurrentDirectory() + "\\service.UninstallLog", new String[] {path,ServiceManager.Name});
			ti.Context = ctx;
			ti.Uninstall( null );
		}

		private static void ti_AfterUninstall(object sender, System.Configuration.Install.InstallEventArgs e)
		{
			if ( OnUninstall != null )
			{
				System.Threading.Thread.Sleep(2000);
				OnUninstall();
			}
		}

        public static void Start()
        {
            ServiceController svc = GetWindowsService(ServiceManager.Name);
            if (svc == null)
                throw new ServiceNotFoundException("The windows service \"" + ServiceManager.Name + "\" was not found.");
            if (svc.Status == ServiceControllerStatus.Stopped)
            {
                svc.Start();
                svc.WaitForStatus(ServiceControllerStatus.Running, new System.TimeSpan(0, 1, 0));
            }
        }

        public static new void Stop()
        {
            ServiceController svc = GetWindowsService(ServiceManager.Name);
            if (svc == null)
                throw new ServiceNotFoundException("The windows service \"" + ServiceManager.Name + "\" was not found.");

            if (svc.Status != ServiceControllerStatus.Stopped)
            {
                svc.Stop();
                svc.WaitForStatus(ServiceControllerStatus.Stopped, new System.TimeSpan(0, 1, 0));
            }
        }

        public static void Restart()
        {
            ServiceController svc = GetWindowsService(ServiceManager.Name);
            if (svc == null)
                throw new ServiceNotFoundException("The windows service \"" + ServiceManager.Name + "\" was not found.");

            if (svc.Status != ServiceControllerStatus.Stopped)
            {
                svc.Stop();
                svc.WaitForStatus(ServiceControllerStatus.Stopped, new System.TimeSpan(0, 1, 0));
            }
            if (svc.Status == ServiceControllerStatus.Stopped)
            {
                svc.Start();
                svc.WaitForStatus(ServiceControllerStatus.Running, new System.TimeSpan(0, 1, 0));
            }
        }

		public static ServiceController GetWindowsService( String name )
		{
			foreach( ServiceController service in ServiceController.GetServices() )
			{
				if ( service.ServiceName == name ) return service;
			}
			return null;
		}

		public static ServiceControllerStatus Status
		{
			get
			{
				ServiceController svc = GetWindowsService( Name );
                if (svc == null)
                    throw new ServiceNotFoundException("The windows service \"" + ServiceManager.Name + "\" was not found.");

				return svc.Status;
			}
		}
		#endregion
	}
}
