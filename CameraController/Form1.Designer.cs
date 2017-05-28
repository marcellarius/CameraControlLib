namespace CameraController
{
    partial class Form1
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
            this.keepWindowOnTop = new System.Windows.Forms.CheckBox();
            this.propertiesLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // cameraUpdateTimer
            // 
            this.cameraUpdateTimer.Enabled = true;
            this.cameraUpdateTimer.Interval = 250;
            this.cameraUpdateTimer.Tick += new System.EventHandler(this.cameraUpdateTimer_Tick);
            // 
            // keepWindowOnTop
            // 
            this.keepWindowOnTop.AutoSize = true;
            this.keepWindowOnTop.Location = new System.Drawing.Point(12, 12);
            this.keepWindowOnTop.Name = "keepWindowOnTop";
            this.keepWindowOnTop.Size = new System.Drawing.Size(142, 17);
            this.keepWindowOnTop.TabIndex = 1;
            this.keepWindowOnTop.Text = "Keep this window on top";
            this.keepWindowOnTop.UseVisualStyleBackColor = true;
            this.keepWindowOnTop.CheckedChanged += new System.EventHandler(this.keepWindowOnTop_CheckedChanged);
            // 
            // propertiesLayoutPanel
            // 
            this.propertiesLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.propertiesLayoutPanel.Location = new System.Drawing.Point(12, 35);
            this.propertiesLayoutPanel.Name = "propertiesLayoutPanel";
            this.propertiesLayoutPanel.Size = new System.Drawing.Size(593, 314);
            this.propertiesLayoutPanel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 361);
            this.Controls.Add(this.propertiesLayoutPanel);
            this.Controls.Add(this.keepWindowOnTop);
            this.Name = "Form1";
            this.Text = "Camera Controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer cameraUpdateTimer;
        private System.Windows.Forms.CheckBox keepWindowOnTop;
        private System.Windows.Forms.FlowLayoutPanel propertiesLayoutPanel;
    }
}

