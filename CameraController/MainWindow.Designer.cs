namespace CameraController
{
    partial class MainWindow
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
            this.cameraUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.propertySliderPanel = new System.Windows.Forms.Panel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.resetToDefaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.presetSelectorControl = new CameraController.PresetSelectorControl();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraUpdateTimer
            // 
            this.cameraUpdateTimer.Enabled = true;
            this.cameraUpdateTimer.Interval = 250;
            this.cameraUpdateTimer.Tick += new System.EventHandler(this.cameraUpdateTimer_Tick);
            // 
            // propertySliderPanel
            // 
            this.propertySliderPanel.AutoScroll = true;
            this.propertySliderPanel.AutoScrollMinSize = new System.Drawing.Size(400, 200);
            this.propertySliderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertySliderPanel.Location = new System.Drawing.Point(0, 0);
            this.propertySliderPanel.Name = "propertySliderPanel";
            this.propertySliderPanel.Size = new System.Drawing.Size(441, 337);
            this.propertySliderPanel.TabIndex = 2;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(624, 24);
            this.mainMenuStrip.TabIndex = 3;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectCameraToolStripMenuItem,
            this.toolStripSeparator2,
            this.resetToDefaultsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.fileToolStripMenuItem.Text = "Camera";
            // 
            // selectCameraToolStripMenuItem
            // 
            this.selectCameraToolStripMenuItem.Name = "selectCameraToolStripMenuItem";
            this.selectCameraToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.selectCameraToolStripMenuItem.Text = "Select Camera...";
            this.selectCameraToolStripMenuItem.Click += new System.EventHandler(this.selectCameraToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // resetToDefaultsToolStripMenuItem
            // 
            this.resetToDefaultsToolStripMenuItem.Name = "resetToDefaultsToolStripMenuItem";
            this.resetToDefaultsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.resetToDefaultsToolStripMenuItem.Text = "Reset to defaults";
            this.resetToDefaultsToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem,
            this.optionsToolStripMenuItem1});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.optionsToolStripMenuItem.Text = "Tools";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.CheckOnClick = true;
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.optionsToolStripMenuItem1.Text = "Options...";
            this.optionsToolStripMenuItem1.Click += new System.EventHandler(this.optionsToolStripMenuItem1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertySliderPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.presetSelectorControl);
            this.splitContainer1.Size = new System.Drawing.Size(624, 337);
            this.splitContainer1.SplitterDistance = 441;
            this.splitContainer1.TabIndex = 4;
            // 
            // presetSelectorControl
            // 
            this.presetSelectorControl.ActivePreset = null;
            this.presetSelectorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.presetSelectorControl.Location = new System.Drawing.Point(0, 0);
            this.presetSelectorControl.Name = "presetSelectorControl";
            this.presetSelectorControl.Settings = null;
            this.presetSelectorControl.Size = new System.Drawing.Size(179, 337);
            this.presetSelectorControl.TabIndex = 0;
            this.presetSelectorControl.ApplyPreset += new System.EventHandler<CameraController.PresetSelectorEventArgs>(this.presetSelectorControl_ApplyPreset);
            this.presetSelectorControl.SelectedPresetChanged += new System.EventHandler<CameraController.PresetSelectorEventArgs>(this.presetSelectorControl_SelectedPresetChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 200);
            this.Name = "MainWindow";
            this.RightToLeftLayout = true;
            this.Text = "Camera Controller";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer cameraUpdateTimer;
        private System.Windows.Forms.Panel propertySliderPanel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private PresetSelectorControl presetSelectorControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem resetToDefaultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

