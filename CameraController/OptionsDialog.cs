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
    public partial class OptionsDialog : Form
    {
        private Settings _settings;
        List<CameraPropertyDescriptor> _cameraPropertyDescriptors = Camera.KnownProperties.ToList();

        public OptionsDialog(Settings settings)
        {
            InitializeComponent();
            _settings = settings;

            // Prepare a list of available cameras.
            // If the currently selected default camera is not available, it will be added to 
            // the list with an "[unplugged?]" comment in front of it. This means the existing
            // default will be preserved if the options are edited with the camera unplugged.
            var availableCameras = CameraDescriptor.GetAll().Cast<ICameraDescriptor>().ToList();
            var defaultCameraList = availableCameras.Select(c => new DefaultCameraListItem() { CameraDescriptor = c }).ToList();
            var currentCameraSetting = _settings.DefaultCamera;
            ICameraDescriptor cameraToSelect = null;
            if (currentCameraSetting != null)
            {
                var existingCameraEntry = availableCameras.FirstOrDefault(c => c.Name == currentCameraSetting.Name && c.DevicePath == currentCameraSetting.DevicePath);
                if (existingCameraEntry != null)
                {
                    cameraToSelect = existingCameraEntry;
                }
                else
                {
                    defaultCameraList.Insert(0, new DefaultCameraListItem() { CameraDescriptor = currentCameraSetting, Comment = "unplugged?" });
                    cameraToSelect = currentCameraSetting;
                }
            }

            availableCamerasComboBox.DataSource = defaultCameraList;
            availableCamerasComboBox.DisplayMember = "Text";
            availableCamerasComboBox.ValueMember = "CameraDescriptor";
            availableCamerasComboBox.SelectedValue = cameraToSelect;

            visiblePropertiesListbox.DataSource = _cameraPropertyDescriptors;
            visiblePropertiesListbox.DisplayMember = "Name";
            UpdateVisiblePropertiesCheckState();
        }

        class DefaultCameraListItem
        {
            public ICameraDescriptor CameraDescriptor { get; set; }
            public string Comment { get; set; } = null;
            public string Text
            {
                get
                {
                    if (Comment != null)
                        return String.Format("[{0}] {1}", Comment, CameraDescriptor.Name);
                    else
                        return CameraDescriptor.Name;
                }
            }
        }

        public void UpdateSettings()
        {
            _settings.HiddenProperties = GetHiddenProperties();
            var selectedDefaultCamera = availableCamerasComboBox.SelectedValue as ICameraDescriptor;
            if (selectedDefaultCamera != null)
            {
                _settings.DefaultCamera = new CameraReference() { Name = selectedDefaultCamera.Name, DevicePath = selectedDefaultCamera.DevicePath };
            }
        }

        private void UpdateVisiblePropertiesCheckState()
        {
            Predicate<string> isVisible = (id) => _settings.HiddenProperties == null || !_settings.HiddenProperties.Contains(id);
            for (int i = 0; i < _cameraPropertyDescriptors.Count; i++)
            {
                visiblePropertiesListbox.SetItemChecked(i, isVisible(_cameraPropertyDescriptors[i].Id));
            }
        }

        private List<string> GetHiddenProperties()
        {
            List<string> hiddenProperties = new List<string>();
            for (int i = 0; i < _cameraPropertyDescriptors.Count; i++)
                if (!visiblePropertiesListbox.GetItemChecked(i))
                    hiddenProperties.Add(_cameraPropertyDescriptors[i].Id);
            return hiddenProperties;
        }
    }
}
