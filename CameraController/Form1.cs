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

        public Form1()
        {
            InitializeComponent();
            _camera = Camera.Get("HD Pro Webcam C920");
            _camera.Refresh();

            foreach (var prop in _camera.GetSupportedProperties())
            {
                var controlSlider = new CameraControlSlider(prop);
                propertiesLayoutPanel.Controls.Add(controlSlider);

            }
        }

        private void cameraUpdateTimer_Tick(object sender, EventArgs e)
        {
            _camera.Refresh();
        }

        private void keepWindowOnTop_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = keepWindowOnTop.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
