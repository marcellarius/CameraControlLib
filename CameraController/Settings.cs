using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraController
{
    public class Settings
    {
        public CameraReference DefaultCamera = null;
        public bool OverrideCameraRanges = false;
        public List<string> HiddenProperties = new List<string>();

        public static Settings Load(StreamReader reader)
        {
            var jsonString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Settings>(jsonString);
        }

        public void Save(StreamWriter writer)
        {
            var serializedJson = JsonConvert.SerializeObject(this);
            writer.Write(serializedJson);
        }
    }

    public class CameraReference
    {
        public string DevicePath;
        public string Name;
    }
}
