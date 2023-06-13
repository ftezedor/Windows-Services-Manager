using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using Windows.Service.Logging;
using System.ServiceProcess;

namespace Service.Manager
{
    public partial class DebugPanel : System.Windows.Forms.Form
    {
        public DebugPanel()
        {
            InitializeComponent();
        }

        private void Panel_Load(object sender, EventArgs e)
        {
            this.Text += " - " + ServiceManager.Name;

            btnClose.Enabled = true;
            btnClose.Text = "Close";

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
                this.label1.Text = "To run this application in debug mode, the service should be stopped. Close this window, stop the service, and launch this application again.";
                this.label1.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!WorkersOrchestrator.Running) 
                WorkersOrchestrator.start();

            btnClose.Text = "Stop";
            /*
            FileStream fs = new FileStream("ServicesManager.log", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.AutoFlush = true;
            Console.SetOut(sw);
            Console.SetError(sw);
            */
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnClose.Text = "stopping";
            btnClose.Enabled = false;
            if (WorkersOrchestrator.Running)
                WorkersOrchestrator.stop();
        }

    }
}
