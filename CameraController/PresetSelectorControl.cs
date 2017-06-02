using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraController
{
    public partial class PresetSelectorControl : UserControl
    {
        public event EventHandler<PresetSelectorEventArgs> ApplyPreset;
        public event EventHandler<PresetSelectorEventArgs> ActivePresetChanged;
        public event EventHandler<PresetSelectorEventArgs> SelectedPresetChanged;

        public Settings Settings { get; set; }

        private Preset _activePreset = null;
        public Preset ActivePreset
        {
            get { return _activePreset; }
            set
            {
                _activePreset = value;
                OnActivePresetChanged();
            }
        }

        public Preset SelectedPreset
        {
            get { return this.GetSelectedNodeTag<Preset>(); }
        }

        private Action<Preset> _capturePresetFunction;
        private Font _activePresetFont = new Font(TreeView.DefaultFont, FontStyle.Bold);

        public PresetSelectorControl()
        {
            InitializeComponent();
        }

        public void Initialize(Settings settings, Action<Preset> capturePreset)
        {
            Settings = settings;
            _capturePresetFunction = capturePreset;
            Settings.Saved += Settings_Saved;
            RefreshPresetsList();
        }

        private void Settings_Saved(object sender, EventArgs e)
        {
            RefreshPresetsList();
        }

        private void OnActivePresetChanged()
        {
            UpdateTreeViewIcons();
            ActivePresetChanged?.Invoke(this, new PresetSelectorEventArgs(ActivePreset));
        }

        private void RefreshPresetsList()
        {
            presetTreeView.Nodes.Clear();
            var presetGroups = Settings.PresetGroups.OrderBy(group => group.Name).ToList();
            foreach (var group in presetGroups)
            {
                TreeNode groupNode = new TreeNode(group.Name);
                groupNode.Tag = group;
                groupNode.ContextMenuStrip = groupContextMenu;
                foreach (var preset in group.Presets.OrderBy(p => Name))
                {
                    TreeNode presetNode = new TreeNode(preset.Name);
                    presetNode.ContextMenuStrip = presetContextMenu;
                    presetNode.Tag = preset;
                    groupNode.Nodes.Add(presetNode);
                }
                presetTreeView.Nodes.Add(groupNode);
            }
            UpdateTreeViewIcons();
            presetTreeView.ExpandAll();
        }

        private void UpdateTreeViewIcons()
        {
            foreach (TreeNode groupNode in presetTreeView.Nodes)
            {
                groupNode.ImageKey = groupNode.SelectedImageKey = groupNode.IsExpanded ? "GroupOpen" : "GroupClosed";
                foreach (TreeNode presetNode in groupNode.Nodes)
                {
                    bool isActivePreset = ActivePreset != null && presetNode.Tag == ActivePreset;
                    presetNode.ImageKey = presetNode.SelectedImageKey = "Preset";
                    presetNode.NodeFont = isActivePreset ? _activePresetFont : TreeView.DefaultFont;
                    presetNode.Text = presetNode.Text;
                }
            }
        }

        private void presetTreeView_OnNodeInteraction(object sender, TreeViewEventArgs e)
        {
            UpdateTreeViewIcons();
        }

        private void applyPresetMenuItem_Click(object sender, EventArgs e)
        {
            var preset = GetSelectedNodeTag<Preset>();
            if (preset != null)
            {
                ApplyPreset?.Invoke(this, new PresetSelectorEventArgs(preset));
            }
        }

        private void presetTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeView)sender).SelectedNode = e.Node;
        }

        private T GetSelectedNodeTag<T>() where T : class
        {
            return presetTreeView.SelectedNode?.Tag as T;
        }

        private void renameGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var group = GetSelectedNodeTag<PresetGroup>();
            var groupNode = presetTreeView.SelectedNode;
            if (group != null)
            {
                using (var dialog = new NameEntryDialog("Rename group"))
                {
                    dialog.GroupName = group.Name;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        group.Name = groupNode.Text = dialog.GroupName;
                    }
                }
            }
        }

        private void addGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new NameEntryDialog("Add preset group"))
            {
                dialog.GroupName = "New group";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    PresetGroup group = new PresetGroup() { Name = dialog.GroupName };
                    Settings.PresetGroups.Add(group);
                    RefreshPresetsList();
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var preset = GetSelectedNodeTag<Preset>();
            var presetNode = presetTreeView.SelectedNode;
            if (preset != null)
            {
                using (var dialog = new NameEntryDialog("Rename preset"))
                {
                    dialog.GroupName = preset.Name;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        preset.Name = presetNode.Text = dialog.GroupName;
                    }
                }
            }
        }

        private void addPresetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var group = GetSelectedNodeTag<PresetGroup>();
            var groupNode = presetTreeView.SelectedNode;
            if (group != null)
            {
                var preset = new Preset();
                _capturePresetFunction(preset);
                using (var dialog = new NameEntryDialog("Add preset"))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        preset.Name = dialog.GroupName;
                        group.Presets.Add(preset);
                        RefreshPresetsList();
                    }
                }
            }
        }

        private void presetTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedPresetChanged?.Invoke(this, new PresetSelectorEventArgs(SelectedPreset));
        }

        private void presetTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (GetSelectedNodeTag<Preset>() != null)
            {
                keepPresetAppliedToolStripMenuItem_Click(this, new EventArgs());
            }
        }

        private void keepPresetAppliedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var preset = GetSelectedNodeTag<Preset>();
            if (ActivePreset != preset)
                ActivePreset = preset;
            else
                ActivePreset = null;
        }

        private void presetContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var preset = GetSelectedNodeTag<Preset>();
            keepPresetAppliedToolStripMenuItem.Checked = ActivePreset == preset;
        }
    }

    public class PresetSelectorEventArgs : EventArgs
    {
        public Preset Preset { get; set; }
        
        public PresetSelectorEventArgs(Preset preset)
        {
            Preset = preset;
        }
    }
}
