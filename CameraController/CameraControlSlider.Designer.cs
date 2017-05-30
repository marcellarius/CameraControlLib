namespace CameraController
{
    partial class CameraControlSlider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.valueTrackbar = new System.Windows.Forms.TrackBar();
            this.presetEnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.modeSelector = new System.Windows.Forms.ComboBox();
            this.propertyName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.valueTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // valueTrackbar
            // 
            this.valueTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueTrackbar.Location = new System.Drawing.Point(161, 3);
            this.valueTrackbar.Name = "valueTrackbar";
            this.valueTrackbar.Size = new System.Drawing.Size(105, 45);
            this.valueTrackbar.TabIndex = 0;
            this.valueTrackbar.Scroll += new System.EventHandler(this.valueTrackbar_Scroll);
            // 
            // presetEnabledCheckbox
            // 
            this.presetEnabledCheckbox.AutoSize = true;
            this.presetEnabledCheckbox.Location = new System.Drawing.Point(140, 6);
            this.presetEnabledCheckbox.Name = "presetEnabledCheckbox";
            this.presetEnabledCheckbox.Size = new System.Drawing.Size(15, 14);
            this.presetEnabledCheckbox.TabIndex = 1;
            this.presetEnabledCheckbox.UseVisualStyleBackColor = true;
            // 
            // modeSelector
            // 
            this.modeSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeSelector.FormattingEnabled = true;
            this.modeSelector.Location = new System.Drawing.Point(272, 3);
            this.modeSelector.Name = "modeSelector";
            this.modeSelector.Size = new System.Drawing.Size(73, 21);
            this.modeSelector.TabIndex = 2;
            this.modeSelector.SelectionChangeCommitted += new System.EventHandler(this.modeSelector_SelectionChangeCommitted);
            // 
            // propertyName
            // 
            this.propertyName.Location = new System.Drawing.Point(6, 6);
            this.propertyName.Name = "propertyName";
            this.propertyName.Size = new System.Drawing.Size(128, 13);
            this.propertyName.TabIndex = 3;
            this.propertyName.Text = "<Camera Property Name>";
            this.propertyName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CameraControlSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.propertyName);
            this.Controls.Add(this.modeSelector);
            this.Controls.Add(this.presetEnabledCheckbox);
            this.Controls.Add(this.valueTrackbar);
            this.Name = "CameraControlSlider";
            this.Size = new System.Drawing.Size(348, 38);
            ((System.ComponentModel.ISupportInitialize)(this.valueTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar valueTrackbar;
        private System.Windows.Forms.CheckBox presetEnabledCheckbox;
        private System.Windows.Forms.ComboBox modeSelector;
        private System.Windows.Forms.Label propertyName;
    }
}
