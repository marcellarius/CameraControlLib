using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CameraControlLib;

namespace CameraController
{
    public partial class CameraControlSlider : UserControl
    {
        public CameraProperty Property { get; private set; }

        public CameraControlSlider()
        {
            InitializeComponent();
        }

        public CameraControlSlider(CameraProperty prop) : this()
        {
            SetCameraProperty(prop);
        }

        public void SetCameraProperty(CameraProperty prop)
        {
            if (Property != null)
            {
                Property.Refreshed -= Property_Refreshed;
            }

            Property = prop;
            Enabled = (Property != null && Property.Supported);
            propertyName.Text = Property.Name ?? "Undefined property";

            if (Property != null)
            {
                Property.Refreshed += Property_Refreshed;
                valueTrackbar.Minimum = Property.Min;
                valueTrackbar.Maximum = Property.Max;
                valueTrackbar.SmallChange = Property.MinimumStepSize;
                valueTrackbar.LargeChange = Property.MinimumStepSize * 4;
                valueTrackbar.TickFrequency = Property.MinimumStepSize;

                var validModes = new List<PropertyMode>();
                foreach (var propertyMode in SUPPORTED_MODES)
                    if (Property.Capabilities.HasFlag(propertyMode.Flag))
                        validModes.Add(propertyMode);

                modeSelector.DataSource = validModes;
                modeSelector.DisplayMember = "Name";
                modeSelector.ValueMember = "Flag";

                Property_Refreshed(Property, new EventArgs());
            }
        }

        class PropertyMode
        {
            public CameraPropertyFlags Flag { get; set; }
            public String Name { get; set; }
        }

        static List<PropertyMode> SUPPORTED_MODES = new List<PropertyMode>() {
            new PropertyMode() {Name = "Automatic", Flag = CameraPropertyFlags.Automatic},
            new PropertyMode() {Name = "Manual", Flag = CameraPropertyFlags.Manual}
        };

        private void Property_Refreshed(object sender, EventArgs e)
        {
            valueTrackbar.Value = Property.Value;
            if (!modeSelector.DroppedDown)
                modeSelector.SelectedValue = Property.Flags;
        }

        private void valueTrackbar_Scroll(object sender, EventArgs e)
        {
            if (Property != null && Property.Supported)
            {
                Property.Value = valueTrackbar.Value;
                Property.Flags = CameraPropertyFlags.Manual;
                Property.Save();
            }
        }

        private void modeSelector_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Property != null && Property.Supported)
            {
                Property.Flags = (CameraPropertyFlags)modeSelector.SelectedValue;
                Property.Save();
            }
        }
    }
}
