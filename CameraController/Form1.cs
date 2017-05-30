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
    public partial class Form1 : Form
    {
        private Camera _camera;

        private Camera GetNamedCamera(string cameraName)
        {
            var availableCameras = Camera.GetAll();
            var chosenDevice = availableCameras.FirstOrDefault(c => c.Name == cameraName);
            if (chosenDevice == null)
                chosenDevice = availableCameras.FirstOrDefault();
            return chosenDevice?.Create();
        }

        public Form1()
        {
            InitializeComponent();

            _camera = GetNamedCamera("HD Pro Webcam C920");
            _camera.Refresh();

            int topPosition = 0;
            foreach (var prop in _camera.GetSupportedProperties())
            {
                var slider = new CameraControlSlider(prop);
                slider.Top = topPosition;
                slider.Width = propertySliderPanel.Width;
                slider.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                topPosition += slider.Height;
                propertySliderPanel.Controls.Add(slider);
            }
        }

        private void cameraUpdateTimer_Tick(object sender, EventArgs e)
        {
            _camera?.Refresh();
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
