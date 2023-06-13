namespace Service.Manager
{
    partial class ConfigManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigManager));
            this.label1 = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWorkersPath = new System.Windows.Forms.TextBox();
            this.btnWorkersPath = new System.Windows.Forms.Button();
            this.btnStagingWorkers = new System.Windows.Forms.Button();
            this.txtStagingPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRejectedWorkers = new System.Windows.Forms.Button();
            this.txtRejectsPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Windows service name";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(16, 30);
            this.txtServiceName.MaxLength = 60;
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(473, 20);
            this.txtServiceName.TabIndex = 1;
            this.txtServiceName.TextChanged += new System.EventHandler(this.txtServiceName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path for service workers";
            // 
            // txtWorkersPath
            // 
            this.txtWorkersPath.Location = new System.Drawing.Point(16, 73);
            this.txtWorkersPath.MaxLength = 300;
            this.txtWorkersPath.Name = "txtWorkersPath";
            this.txtWorkersPath.Size = new System.Drawing.Size(440, 20);
            this.txtWorkersPath.TabIndex = 3;
            this.txtWorkersPath.TextChanged += new System.EventHandler(this.txtWorkersPath_TextChanged);
            // 
            // btnWorkersPath
            // 
            this.btnWorkersPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnWorkersPath.Location = new System.Drawing.Point(462, 73);
            this.btnWorkersPath.Name = "btnWorkersPath";
            this.btnWorkersPath.Size = new System.Drawing.Size(27, 20);
            this.btnWorkersPath.TabIndex = 4;
            this.btnWorkersPath.Text = "...";
            this.toolTip1.SetToolTip(this.btnWorkersPath, "select folder");
            this.btnWorkersPath.UseVisualStyleBackColor = true;
            this.btnWorkersPath.Click += new System.EventHandler(this.btnWorkersPath_Click);
            // 
            // btnStagingWorkers
            // 
            this.btnStagingWorkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnStagingWorkers.Location = new System.Drawing.Point(462, 117);
            this.btnStagingWorkers.Name = "btnStagingWorkers";
            this.btnStagingWorkers.Size = new System.Drawing.Size(27, 20);
            this.btnStagingWorkers.TabIndex = 7;
            this.btnStagingWorkers.Text = "...";
            this.toolTip1.SetToolTip(this.btnStagingWorkers, "select folder");
            this.btnStagingWorkers.UseVisualStyleBackColor = true;
            this.btnStagingWorkers.Click += new System.EventHandler(this.btnStagingWorkers_Click);
            // 
            // txtStagingPath
            // 
            this.txtStagingPath.Location = new System.Drawing.Point(16, 117);
            this.txtStagingPath.MaxLength = 300;
            this.txtStagingPath.Name = "txtStagingPath";
            this.txtStagingPath.Size = new System.Drawing.Size(440, 20);
            this.txtStagingPath.TabIndex = 6;
            this.txtStagingPath.TextChanged += new System.EventHandler(this.txtStagingPath_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Path for staging workers";
            // 
            // btnRejectedWorkers
            // 
            this.btnRejectedWorkers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnRejectedWorkers.Location = new System.Drawing.Point(462, 159);
            this.btnRejectedWorkers.Name = "btnRejectedWorkers";
            this.btnRejectedWorkers.Size = new System.Drawing.Size(27, 20);
            this.btnRejectedWorkers.TabIndex = 10;
            this.btnRejectedWorkers.Text = "...";
            this.toolTip1.SetToolTip(this.btnRejectedWorkers, "select folder");
            this.btnRejectedWorkers.UseVisualStyleBackColor = true;
            this.btnRejectedWorkers.Click += new System.EventHandler(this.btnRejectedWorkers_Click);
            // 
            // txtRejectsPath
            // 
            this.txtRejectsPath.Location = new System.Drawing.Point(16, 159);
            this.txtRejectsPath.MaxLength = 300;
            this.txtRejectsPath.Name = "txtRejectsPath";
            this.txtRejectsPath.Size = new System.Drawing.Size(440, 20);
            this.txtRejectsPath.TabIndex = 9;
            this.txtRejectsPath.TextChanged += new System.EventHandler(this.txtRejectsPath_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Path for rejected workers";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(158, 216);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(122, 23);
            this.btnLoad.TabIndex = 11;
            this.btnLoad.Text = "Load Configuration";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(294, 216);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save Configuration";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(429, 216);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ConfigManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 251);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnRejectedWorkers);
            this.Controls.Add(this.txtRejectsPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStagingWorkers);
            this.Controls.Add(this.txtStagingPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnWorkersPath);
            this.Controls.Add(this.txtWorkersPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigManager";
            this.Text = "Configuration Manager";
            this.Load += new System.EventHandler(this.ConfigManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWorkersPath;
        private System.Windows.Forms.Button btnWorkersPath;
        private System.Windows.Forms.Button btnStagingWorkers;
        private System.Windows.Forms.TextBox txtStagingPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRejectedWorkers;
        private System.Windows.Forms.TextBox txtRejectsPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}