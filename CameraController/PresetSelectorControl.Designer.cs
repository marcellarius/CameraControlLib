namespace CameraController
{
    partial class PresetSelectorControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetSelectorControl));
            this.presetTreeView = new System.Windows.Forms.TreeView();
            this.backgroundContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presetStatusImages = new System.Windows.Forms.ImageList(this.components);
            this.presetContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.applyPresetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepPresetAppliedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.savePresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundContextMenu.SuspendLayout();
            this.presetContextMenu.SuspendLayout();
            this.groupContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // presetTreeView
            // 
            this.presetTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.presetTreeView.ContextMenuStrip = this.backgroundContextMenu;
            this.presetTreeView.FullRowSelect = true;
            this.presetTreeView.HideSelection = false;
            this.presetTreeView.ImageIndex = 0;
            this.presetTreeView.ImageList = this.presetStatusImages;
            this.presetTreeView.Location = new System.Drawing.Point(3, 3);
            this.presetTreeView.Name = "presetTreeView";
            this.presetTreeView.SelectedImageIndex = 0;
            this.presetTreeView.Size = new System.Drawing.Size(207, 287);
            this.presetTreeView.StateImageList = this.presetStatusImages;
            this.presetTreeView.TabIndex = 0;
            this.presetTreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.presetTreeView_OnNodeInteraction);
            this.presetTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.presetTreeView_OnNodeInteraction);
            this.presetTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.presetTreeView_AfterSelect);
            this.presetTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.presetTreeView_NodeMouseClick);
            this.presetTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.presetTreeView_NodeMouseDoubleClick);
            // 
            // backgroundContextMenu
            // 
            this.backgroundContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGroupToolStripMenuItem});
            this.backgroundContextMenu.Name = "backgroundContextMenu";
            this.backgroundContextMenu.Size = new System.Drawing.Size(142, 26);
            // 
            // addGroupToolStripMenuItem
            // 
            this.addGroupToolStripMenuItem.Image = global::CameraController.Properties.Resources.NewGroup;
            this.addGroupToolStripMenuItem.Name = "addGroupToolStripMenuItem";
            this.addGroupToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addGroupToolStripMenuItem.Text = "Add Group...";
            this.addGroupToolStripMenuItem.Click += new System.EventHandler(this.addGroupToolStripMenuItem_Click);
            // 
            // presetStatusImages
            // 
            this.presetStatusImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("presetStatusImages.ImageStream")));
            this.presetStatusImages.TransparentColor = System.Drawing.Color.Transparent;
            this.presetStatusImages.Images.SetKeyName(0, "GroupClosed");
            this.presetStatusImages.Images.SetKeyName(1, "GroupOpen");
            this.presetStatusImages.Images.SetKeyName(2, "Preset");
            this.presetStatusImages.Images.SetKeyName(3, "blank.png");
            // 
            // presetContextMenu
            // 
            this.presetContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applyPresetMenuItem,
            this.keepPresetAppliedToolStripMenuItem,
            this.toolStripSeparator1,
            this.savePresetToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.presetContextMenu.Name = "presetContextMenu";
            this.presetContextMenu.Size = new System.Drawing.Size(188, 142);
            this.presetContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.presetContextMenu_Opening);
            // 
            // applyPresetMenuItem
            // 
            this.applyPresetMenuItem.Name = "applyPresetMenuItem";
            this.applyPresetMenuItem.Size = new System.Drawing.Size(187, 22);
            this.applyPresetMenuItem.Text = "Apply Once";
            this.applyPresetMenuItem.Click += new System.EventHandler(this.applyPresetMenuItem_Click);
            // 
            // keepPresetAppliedToolStripMenuItem
            // 
            this.keepPresetAppliedToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.keepPresetAppliedToolStripMenuItem.Name = "keepPresetAppliedToolStripMenuItem";
            this.keepPresetAppliedToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.keepPresetAppliedToolStripMenuItem.Text = "Keep Preset Applied";
            this.keepPresetAppliedToolStripMenuItem.Click += new System.EventHandler(this.keepPresetAppliedToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // savePresetToolStripMenuItem
            // 
            this.savePresetToolStripMenuItem.Name = "savePresetToolStripMenuItem";
            this.savePresetToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.savePresetToolStripMenuItem.Text = "Save Preset";
            this.savePresetToolStripMenuItem.Click += new System.EventHandler(this.savePresetToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = global::CameraController.Properties.Resources.Rename;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // groupContextMenu
            // 
            this.groupContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPresetToolStripMenuItem,
            this.renameGroupToolStripMenuItem});
            this.groupContextMenu.Name = "groupContextMenu";
            this.groupContextMenu.Size = new System.Drawing.Size(162, 48);
            // 
            // addPresetToolStripMenuItem
            // 
            this.addPresetToolStripMenuItem.Name = "addPresetToolStripMenuItem";
            this.addPresetToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addPresetToolStripMenuItem.Text = "Add Preset...";
            this.addPresetToolStripMenuItem.Click += new System.EventHandler(this.addPresetToolStripMenuItem_Click);
            // 
            // renameGroupToolStripMenuItem
            // 
            this.renameGroupToolStripMenuItem.Image = global::CameraController.Properties.Resources.Rename;
            this.renameGroupToolStripMenuItem.Name = "renameGroupToolStripMenuItem";
            this.renameGroupToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.renameGroupToolStripMenuItem.Text = "Rename group...";
            this.renameGroupToolStripMenuItem.Click += new System.EventHandler(this.renameGroupToolStripMenuItem_Click);
            // 
            // PresetSelectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.presetTreeView);
            this.Name = "PresetSelectorControl";
            this.Size = new System.Drawing.Size(213, 293);
            this.backgroundContextMenu.ResumeLayout(false);
            this.presetContextMenu.ResumeLayout(false);
            this.groupContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView presetTreeView;
        private System.Windows.Forms.ImageList presetStatusImages;
        private System.Windows.Forms.ContextMenuStrip presetContextMenu;
        private System.Windows.Forms.ToolStripMenuItem applyPresetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip groupContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addPresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip backgroundContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepPresetAppliedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
