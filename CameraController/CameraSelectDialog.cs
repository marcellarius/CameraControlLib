using CameraControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CameraController
{
    public partial class CameraSelectDialog : Form
    {
        List<CameraDescriptor> _availableCameras;

        public CameraDescriptor SelectedCamera
        {
            get
            {
                return availableCameraComboBox.SelectedItem as CameraDescriptor;
            }
        }

        public CameraSelectDialog()
        {
            InitializeComponent();
            _availableCameras = CameraDescriptor.GetAll();
            availableCameraComboBox.DisplayMember = "Name";
            availableCameraComboBox.DataSource = _availableCameras;
        }
    }
}
