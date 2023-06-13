using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace Service.Manager
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class ServiceInstaller : System.Configuration.Install.Installer
	{
		private System.ServiceProcess.ServiceProcessInstaller spiGeneral;
		private System.ServiceProcess.ServiceInstaller siGeneral;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ServiceInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
            this.siGeneral.ServiceName = ManagerConfig.ServiceName;
            this.siGeneral.DisplayName = ManagerConfig.ServiceName;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.spiGeneral = new System.ServiceProcess.ServiceProcessInstaller();
            this.siGeneral = new System.ServiceProcess.ServiceInstaller();
            // 
            // spiGeneral
            // 
            this.spiGeneral.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.spiGeneral.Password = null;
            this.spiGeneral.Username = null;
            this.spiGeneral.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.spiNetscreen_AfterInstall);
            // 
            // siGeneral
            // 
            this.siGeneral.DisplayName = "Windows Services Manager";
            this.siGeneral.ServiceName = "Windows Services Manager";
            this.siGeneral.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.spiGeneral,
            this.siGeneral});

		}
		#endregion

		public void SetUser( String name, String password )
		{
			if ( name == null || name.Length <= 0 ) 
                throw new ArgumentException("The user name must be informed" );
			if ( password == null || password.Length <= 0 ) 
                throw new ArgumentException("The user password must be informed" );
			this.spiGeneral.Account = System.ServiceProcess.ServiceAccount.User;
			this.spiGeneral.Password = password;
			this.spiGeneral.Username = name;
		}

        // does some tweaks after installation
		private void serviceInstaller1_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
		{
			Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM");
			rk = rk.OpenSubKey("CurrentControlSet");
			rk = rk.OpenSubKey("Services");
			rk = rk.OpenSubKey( this.siGeneral.ServiceName,true );
			if ( rk != null )
			{
				rk.SetValue("Description","Manages child windows services");
				//rk.SetValue("ImagePath", rk.GetValue("ImagePath") + " /service" );
                rk.SetValue("ImagePath", rk.GetValue("ImagePath") + " /config \"" + ManagerConfig.ConfigurationFile + "\"");
			}
		}

		private void spiNetscreen_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
		{
		
		}
	}
}
