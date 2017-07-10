using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CameraControlLib;

namespace CameraController
{
    public partial class MainWindow : Form
    {
        public Settings Settings { get; private set; }
        public Camera Camera { get; private set; }
        List<CameraControlSlider> _sliderControls = new List<CameraControlSlider>();

        public MainWindow(Settings settings)
        {
            InitializeComponent();
            Settings = settings;
            presetSelectorControl.Initialize(Settings, CapturePreset);

            var defaultCamera = GetDefaultCamera();
            SetCamera(defaultCamera?.Create());
        }

        private CameraDescriptor GetDefaultCamera()
        {
            CameraDescriptor preferredCamera = null;
            if (Settings.DefaultCamera != null)
                preferredCamera = CameraDescriptor.Find(Settings.DefaultCamera.Name, Settings.DefaultCamera.DevicePath);
            return preferredCamera ?? CameraDescriptor.GetAll().FirstOrDefault();
        }

        private void SetCamera(Camera camera)
        {
            if (Camera != null && Camera != camera)
                Camera.Dispose();

            Camera = camera;
            presetSelectorControl.ActivePreset = null;
            UpdateCameraSliders();
        }

        private void UpdateCameraSliders(bool forceShowAll = false)
        {
            foreach (Control control in propertySliderPanel.Controls)
                control.Dispose();
            propertySliderPanel.Controls.Clear();
            _sliderControls.Clear();

            if (Camera != null)
            {
                Camera.Refresh();
                int topPosition = 0;
                int sliderControlWidth = propertySliderPanel.Width - 10;  //Allow 10px margin on the right
                foreach (var prop in Camera.GetSupportedProperties())
                {
                    if (!forceShowAll && Settings.HiddenProperties != null && Settings.HiddenProperties.Contains(prop.Id))
                        continue;

                    var slider = new CameraControlSlider(prop);
                    slider.Top = topPosition;
                    slider.Width = sliderControlWidth;
                    slider.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    topPosition += slider.Height;
                    propertySliderPanel.Controls.Add(slider);
                    _sliderControls.Add(slider);
                }
            }
            else
            {
                Label noCameraSelectedLabel = new Label();
                noCameraSelectedLabel.AutoSize = true;
                noCameraSelectedLabel.Text = "No camera selected";
                noCameraSelectedLabel.Font = new Font(noCameraSelectedLabel.Font.FontFamily, 14, FontStyle.Regular);
                noCameraSelectedLabel.Left = 20;
                noCameraSelectedLabel.Top = 20;
                propertySliderPanel.Controls.Add(noCameraSelectedLabel);
            }
        }

        private IEnumerable<string> GetPresetEnabledProperties()
        {
            foreach (var slider in _sliderControls)
                if (slider.PresetEnabled)
                    yield return slider.Property.Id;
        }

        private void CapturePreset(Preset preset)
        {
            var enabledProperties = GetPresetEnabledProperties();
            preset.RecordPreset(Camera, enabledProperties);
        }

        private void cameraUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (Camera != null)
            {
                Camera.Refresh();

                if (presetSelectorControl.ActivePreset != null)
                {
                    presetSelectorControl.ActivePreset.Apply(Camera);
                    Camera.Save();
                }
            }
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selectCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var cameraSelectDialog = new CameraSelectDialog())
            {
                if (cameraSelectDialog.ShowDialog(this) == DialogResult.OK)
                {
                    SetCamera(cameraSelectDialog.SelectedCamera?.Create());
                }
            }
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var optionsDialog = new OptionsDialog(Settings))
            {
                if (optionsDialog.ShowDialog(this) == DialogResult.OK)
                {
                    optionsDialog.UpdateSettings();
                    Settings.Save();

                    UpdateCameraSliders();
                }
            }
        }

        private void resetToDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            presetSelectorControl.ActivePreset = null;
            Camera?.ResetToDefault();
            Camera?.Save();
        }

        private void presetSelectorControl_SelectedPresetChanged(object sender, PresetSelectorEventArgs e)
        {
            if (e.Preset != null)
            {
                foreach (var slider in _sliderControls)
                {
                    slider.PresetEnabled = e.Preset.Properties.ContainsKey(slider.Property.Id);
                }
            }
        }

        private void presetSelectorControl_ApplyPreset(object sender, PresetSelectorEventArgs e)
        {
            e.Preset.Apply(Camera);
            Camera.Save();
        }

        private void presetSelectorControl_ActivePresetChanged(object sender, PresetSelectorEventArgs e)
        {
            foreach (var slider in _sliderControls)
            {
                slider.Enabled = e.Preset == null || !e.Preset.Properties.ContainsKey(slider.Property.Id);
            }
        }
    }
}
