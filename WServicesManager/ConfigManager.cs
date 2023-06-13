using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Service.Manager
{
    public partial class ConfigManager : Form
    {
        private Boolean edited = false;
        private static FolderBrowserDialog fbd = new FolderBrowserDialog();

        public ConfigManager()
        {
            InitializeComponent();
        }

        private void btnWorkersPath_Click(object sender, EventArgs e)
        {
            txtWorkersPath.Text = SelectFolder(txtWorkersPath.Text);
        }

        private void btnStagingWorkers_Click(object sender, EventArgs e)
        {
            txtStagingPath.Text = SelectFolder(txtStagingPath.Text);
        }

        private void btnRejectedWorkers_Click(object sender, EventArgs e)
        {
            txtRejectsPath.Text = SelectFolder(txtRejectsPath.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (edited)
            {
                DialogResult result = MessageBox.Show(" Are you sure you want leave without saving your data?", "Question", MessageBoxButtons.YesNo);
                if (result.Equals(DialogResult.No)) return;
            }
            this.Close();
        }

        private static String SelectFolder(String path)
        {

            if (path != null && path != "" && Directory.Exists(path))
                fbd.SelectedPath = path;
            else
            {
                if (String.IsNullOrWhiteSpace(Environment.CurrentDirectory))
                    Environment.CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                fbd.SelectedPath = Environment.CurrentDirectory;
            }

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    Environment.CurrentDirectory = fbd.SelectedPath;
                    return fbd.SelectedPath;
                }

                return path == null ? String.Empty : path;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text) ||
                string.IsNullOrWhiteSpace(txtWorkersPath.Text) ||
                string.IsNullOrWhiteSpace(txtStagingPath.Text) ||
                string.IsNullOrWhiteSpace(txtRejectsPath.Text))
            {
                MessageBox.Show("All fields must be filled", "Warning");
                return;
            }

            String[] dirs = new String[] {txtWorkersPath.Text, txtStagingPath.Text, txtRejectsPath.Text};

            foreach (String dir in dirs)
            {
                if (!Directory.Exists(dir))
                {
                    MessageBox.Show(dir + " does not exist", "Error");
                    return;
                }
            }

            using (FileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "cfg";
                fd.CheckPathExists = true;
                fd.CheckFileExists = false;
                fd.AddExtension = true;
                //fd.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (String.IsNullOrWhiteSpace(Environment.CurrentDirectory))
                    Environment.CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                fd.InitialDirectory = Environment.CurrentDirectory;

                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    try
                    {
                        Service.Manager.Config config = new Service.Manager.Config();
                        config.ServiceName = txtServiceName.Text;
                        config.WorkersFolder = txtWorkersPath.Text;
                        config.StagingFolder = txtStagingPath.Text;
                        config.RejectsFolder = txtRejectsPath.Text;
                        config.Save(fd.FileName);
                        Environment.CurrentDirectory = Path.GetDirectoryName(fd.FileName);
                        MessageBox.Show("Configuration saved into the file '" + fd.FileName + "'", "Information");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while saving configuration to file '" + fd.FileName + "'\r\n\r\n" + ex.Message, "Error");
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (FileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "cfg";
                fd.CheckPathExists = true;
                fd.CheckFileExists = true;
                fd.AddExtension = true;
                //fd.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (String.IsNullOrWhiteSpace(Environment.CurrentDirectory))
                    Environment.CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                fd.InitialDirectory = Environment.CurrentDirectory;

                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fd.FileName))
                {
                    try
                    {
                        Service.Manager.Config config = Service.Manager.Config.load(fd.FileName);
                        txtServiceName.Text = config.ServiceName;
                        txtWorkersPath.Text = config.WorkersFolder;
                        txtStagingPath.Text = config.StagingFolder;
                        txtRejectsPath.Text = config.RejectsFolder;
                        Environment.CurrentDirectory = Path.GetDirectoryName(fd.FileName);
                    }
                    catch (InvalidOperationException ex)
                    {
                        MessageBox.Show("Could not load configuration from file '" + fd.FileName + "'\r\n\r\n" + ex.Message, "Error");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while loading configuration from file '" + fd.FileName + "'\r\n\r\n" + ex.Message, "Error");
                    }
                }
            }
        }

        private void ConfigManager_Load(object sender, EventArgs e)
        {
            try
            {
                txtServiceName.Text = Service.Manager.ManagerConfig.ServiceName;
                txtWorkersPath.Text = Service.Manager.ManagerConfig.WorkersFolder;
                txtStagingPath.Text = Service.Manager.ManagerConfig.StagingFolder;
                txtRejectsPath.Text = Service.Manager.ManagerConfig.RejectsFolder;
            }
            catch
            {
                // ManagerConfig would throw an exception if not initialized
            }
        }

//        private void txtServiceName_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            edited = true;
//        }

        private void txtWorkersPath_TextChanged(object sender, EventArgs e)
        {
            edited = true;
        }

        private void txtStagingPath_TextChanged(object sender, EventArgs e)
        {
            edited = true;
        }

        private void txtRejectsPath_TextChanged(object sender, EventArgs e)
        {
            edited = true;
        }

        private void txtServiceName_TextChanged(object sender, EventArgs e)
        {
            edited = true;
        }
    }
}
