using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.ServiceProcess;

namespace Service.Manager
{
	/// <summary>
	/// Summary description for Panel.
	/// </summary>
	public class InstallForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool svcInstalled = false;
		private System.Windows.Forms.Button btnInstall;
		private System.Windows.Forms.Button btnUnisntall;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbAccount1;
		private System.Windows.Forms.RadioButton rbAccount2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtAccount;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtPassword2;
		private ServiceControllerStatus svcStatus = ServiceControllerStatus.Stopped;

        private System.Timers.Timer svcWatcher;

		public InstallForm()
		{
			InitializeComponent();
            InitializeMyself();
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


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnUnisntall = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword2 = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.rbAccount2 = new System.Windows.Forms.RadioButton();
            this.rbAccount1 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service";
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(208, 144);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(96, 23);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "Install service";
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnUnisntall
            // 
            this.btnUnisntall.Location = new System.Drawing.Point(232, 32);
            this.btnUnisntall.Name = "btnUnisntall";
            this.btnUnisntall.Size = new System.Drawing.Size(96, 23);
            this.btnUnisntall.TabIndex = 4;
            this.btnUnisntall.Text = "Uninstall service";
            this.btnUnisntall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(8, 32);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(96, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start service";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(120, 32);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop service";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword2);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAccount);
            this.groupBox1.Controls.Add(this.rbAccount2);
            this.groupBox1.Controls.Add(this.rbAccount1);
            this.groupBox1.Controls.Add(this.btnInstall);
            this.groupBox1.Location = new System.Drawing.Point(8, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 192);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Log on as:";
            this.groupBox1.Visible = false;
            // 
            // txtPassword2
            // 
            this.txtPassword2.Enabled = false;
            this.txtPassword2.Location = new System.Drawing.Point(144, 106);
            this.txtPassword2.MaxLength = 30;
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.PasswordChar = '*';
            this.txtPassword2.Size = new System.Drawing.Size(160, 20);
            this.txtPassword2.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(144, 78);
            this.txtPassword.MaxLength = 30;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(160, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Confirm password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Password";
            // 
            // txtAccount
            // 
            this.txtAccount.Enabled = false;
            this.txtAccount.Location = new System.Drawing.Point(144, 50);
            this.txtAccount.MaxLength = 30;
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(160, 20);
            this.txtAccount.TabIndex = 2;
            // 
            // rbAccount2
            // 
            this.rbAccount2.Location = new System.Drawing.Point(24, 48);
            this.rbAccount2.Name = "rbAccount2";
            this.rbAccount2.Size = new System.Drawing.Size(96, 24);
            this.rbAccount2.TabIndex = 1;
            this.rbAccount2.Text = "&This Account";
            this.rbAccount2.CheckedChanged += new System.EventHandler(this.rbAccount2_CheckedChanged);
            // 
            // rbAccount1
            // 
            this.rbAccount1.Checked = true;
            this.rbAccount1.Location = new System.Drawing.Point(24, 24);
            this.rbAccount1.Name = "rbAccount1";
            this.rbAccount1.Size = new System.Drawing.Size(144, 24);
            this.rbAccount1.TabIndex = 0;
            this.rbAccount1.TabStop = true;
            this.rbAccount1.Text = "&Local System account";
            this.rbAccount1.CheckedChanged += new System.EventHandler(this.rbAccount1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnUnisntall);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 64);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            // 
            // Form
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(353, 321);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "Control Panel";
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
		}
		#endregion

        private void InitializeMyself()
        {
            //
            // Service Status Watcher
            //
            this.svcWatcher = new System.Timers.Timer(15000);
            this.svcWatcher.AutoReset = true;
            this.svcWatcher.Elapsed += (source, eventArgs) => CheckObjects();
            this.svcWatcher.Stop();
        }

		private void Form_Load(object sender, System.EventArgs e)
		{
			CheckObjects();
			this.Text += " - " + ServiceManager.Name;
		}

		private void CheckObjects()
		{
			CheckService();
			if ( svcInstalled )
			{
                if (!svcWatcher.Enabled)
                {
                    svcWatcher.Start();
                }
				//btnInstall.Enabled = false;
				btnStart.Enabled = (svcStatus==ServiceControllerStatus.Stopped);
				btnStop.Enabled = (svcStatus!=ServiceControllerStatus.Stopped);
				btnUnisntall.Enabled = true;
			}
			else
			{
                svcWatcher.Stop();

				//btnInstall.Enabled = true;
				btnStart.Enabled = false;;
				btnStop.Enabled = false;
				btnUnisntall.Enabled = false;;
			}
		}

		private void CheckService()
		{
			try
			{
				svcStatus = ServiceManager.Status;
				svcInstalled = true;
			}
			catch
			{
				svcInstalled = false;
				svcStatus = ServiceControllerStatus.Stopped;
			}

			//ServiceController svc = Service.GetWindowsService( Service.Name );
			//if ( svc == null )
			if ( ! svcInstalled )
			{
				//svcInstalled = false;
				//svcStatus = ServiceControllerStatus.Stopped;
				//label1.Text = "The service is not installed on this computer";
				panel1.Visible = false;
				this.Height = 232;
				this.Width = 359;
				groupBox1.Top = 8;
				groupBox1.Left = 8;
				groupBox1.Visible = true;
			}
			else
			{
				groupBox1.Visible = false;
				this.Height = 116;
				this.Width = 359;
				panel1.Top = 8;
				panel1.Left = 8;
				panel1.Visible = true;
				

				switch( svcStatus )
				{
					case ServiceControllerStatus.ContinuePending:
						label1.Text = "The service status is: Continue Pending";
						break;
					case ServiceControllerStatus.PausePending:
						label1.Text = "The service status is: Pause Pending";
						break;
					case ServiceControllerStatus.Running:
						label1.Text = "The service is running";
						break;
					case ServiceControllerStatus.StartPending:
						label1.Text = "The service status is: Start Pending";
						break;
					case ServiceControllerStatus.Stopped:
						label1.Text = "The service is stopped";
						break;
					case ServiceControllerStatus.StopPending:
						label1.Text = "The service status is: Stop Pending";
						break;
				}
			}
		}

		private void btnInstall_Click(object sender, System.EventArgs e)
		{
			try
			{
				ServiceManager.ClearEvents();
				ServiceManager.OnInstall += new ServiceManager.AfterInstall(Service_OnInstall);
				if ( rbAccount2.Checked )
				{
					if ( txtPassword.Text != txtPassword2.Text )
					{
						MessageBox.Show( "The password and its confirmation do not match","Password verification" );
						return;
					}
					ServiceManager.Install( txtAccount.Text, txtPassword.Text );
				}
				else
				{
					ServiceManager.Install();
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message,"Error" );
			}
		}

		private void btnUninstall_Click(object sender, System.EventArgs e)
		{
			try
			{
				ServiceManager.ClearEvents();
				ServiceManager.OnUninstall += new ServiceManager.AfterUninstall(Service_OnUninstall);
				ServiceManager.Uninstall();
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message,"Error" );
			}

		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{

			try
			{
				ServiceManager.Start();
				CheckObjects();
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message,"Error" );
			}
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			try
			{
				ServiceManager.Stop();
				CheckObjects();
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message,"Error" );
			}
		}

		private void Service_OnInstall()
		{
			CheckObjects();
		}

		private void Service_OnUninstall()
		{
			CheckObjects();
		}

		private void rbAccount2_CheckedChanged(object sender, System.EventArgs e)
		{
			txtAccount.Enabled = true;
			txtPassword.Enabled = true;
			txtPassword2.Enabled = true;
			txtAccount.Focus();
		}

		private void rbAccount1_CheckedChanged(object sender, System.EventArgs e)
		{
			txtAccount.Enabled = false;
			txtPassword.Enabled = false;
			txtPassword2.Enabled = false;
		}
	}
}
