using CameraControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CameraController
{
    public class Preset
    {
        public string Name { get; set; }
        public Dictionary<string, PropertyValue> Properties { get; } = new Dictionary<string, PropertyValue>();

        public void SetProperty(string propertyId, int value, CameraPropertyFlags flags)
        {
            PropertyValue propValue;
            if (!Properties.TryGetValue(propertyId, out propValue))
            {
                propValue = new PropertyValue();
                Properties[propertyId] = propValue;
            }
            propValue.Value = value;
            propValue.Flags = flags;
        }

        public void Clear()
        {
            Properties.Clear();
        }

        public void Clear(string propertyId)
        {
            Properties.Remove(propertyId);
        }

        public void RecordPreset(Camera camera, IEnumerable<string> propertiesToSave)
        {
            Clear();
            foreach (var propertyId in propertiesToSave)
            {
                var cameraProperty = camera.Get(propertyId);
                SetProperty(propertyId, cameraProperty.Value, cameraProperty.Flags);
            }
        }

        public void Apply(Camera camera)
        {
            foreach (var kv in Properties)
            {
                var presetProperty = kv.Value;
                var cameraProperty = camera.Get(kv.Key);
                cameraProperty.Value = presetProperty.Value;
                cameraProperty.Flags = presetProperty.Flags;
            }
        }

        public class PropertyValue
        {
            public int Value { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public CameraPropertyFlags Flags { get; set; }
        }
    }

    public class PresetGroup
    {
        public string Name { get; set; }
        public List<Preset> Presets { get; } = new List<Preset>();
    }
}
