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

        private CameraDescriptor GetDefaultCamera()
        {
            CameraDescriptor preferredCamera = null;
            if (Settings.DefaultCamera != null)
                preferredCamera = CameraDescriptor.Find(Settings.DefaultCamera.Name, Settings.DefaultCamera.DevicePath);
            return preferredCamera ?? CameraDescriptor.GetAll().FirstOrDefault();
        }

        public MainWindow(Settings settings)
        {
            InitializeComponent();
            Settings = settings;
            var defaultCamera = GetDefaultCamera();
            SetCamera(defaultCamera?.Create());
        }

        private void SetCamera(Camera camera)
        {
            if (Camera != null && Camera != camera)
                Camera.Dispose();

            Camera = camera;

            foreach (Control control in propertySliderPanel.Controls)
                control.Dispose();
            propertySliderPanel.Controls.Clear();

            if (Camera != null)
            {
                Camera.Refresh();
                int topPosition = 0;
                foreach (var prop in Camera.GetSupportedProperties())
                {
                    var slider = new CameraControlSlider(prop);
                    slider.Top = topPosition;
                    slider.Width = propertySliderPanel.Width;
                    slider.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                    topPosition += slider.Height;
                    propertySliderPanel.Controls.Add(slider);
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

        private void cameraUpdateTimer_Tick(object sender, EventArgs e)
        {
            Camera?.Refresh();
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
                if (cameraSelectDialog.ShowDialog() == DialogResult.OK)
                {
                    SetCamera(cameraSelectDialog.SelectedCamera?.Create());
                }
            }
        }
    }
}
