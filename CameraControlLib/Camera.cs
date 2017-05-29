using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CameraControlLib
{
    public class Camera : IDisposable
    {
        private DsDevice _device;
        private IBaseFilter _filter;
        private Dictionary<string, CameraProperty> _cameraProperties = new Dictionary<string, CameraProperty>();

        public CameraProperty Focus { get { return _cameraProperties["Focus"]; } }
        public CameraProperty Exposure { get { return _cameraProperties["Exposure"]; } }
        public CameraProperty Zoom { get { return _cameraProperties["Zoom"]; } }
        public CameraProperty Pan { get { return _cameraProperties["Pan"]; } }
        public CameraProperty Tilt { get { return _cameraProperties["Tilt"]; } }
        public CameraProperty Roll { get { return _cameraProperties["Roll"]; } }
        public CameraProperty Iris { get { return _cameraProperties["Iris"]; } }

        public CameraProperty Brightness { get { return _cameraProperties["Brightness"]; } }
        public CameraProperty Contrast { get { return _cameraProperties["Contrast"]; } }
        public CameraProperty Hue { get { return _cameraProperties["Hue"]; } }
        public CameraProperty Saturation { get { return _cameraProperties["Saturation"]; } }
        public CameraProperty Sharpness { get { return _cameraProperties["Sharpness"]; } }
        public CameraProperty Gamma { get { return _cameraProperties["Gamma"]; } }
        public CameraProperty ColorEnable { get { return _cameraProperties["ColorEnable"]; } }
        public CameraProperty WhiteBalance { get { return _cameraProperties["WhiteBalance"]; } }
        public CameraProperty BacklightCompensation { get { return _cameraProperties["BacklightCompensation"]; } }
        public CameraProperty Gain { get { return _cameraProperties["Gain"]; } }

        internal IBaseFilter Filter { get { return _filter; } }

        private Camera(DsDevice device)
        {
            _device = device;
            IFilterGraph2 graphBuilder = new FilterGraph() as IFilterGraph2;
            IMoniker i = _device.Mon as IMoniker;

            graphBuilder.AddSourceFilterForMoniker(i, null, _device.Name, out _filter);

            RegisterProperties();
        }

        private readonly static List<CameraPropertyDescriptor> s_supportedProperties = new List<CameraPropertyDescriptor>()
        {
            CamControlProperty.CreateDescriptor("Focus", "Focus", CameraControlProperty.Focus),
            CamControlProperty.CreateDescriptor("Exposure", "Exposure time", CameraControlProperty.Exposure),
            CamControlProperty.CreateDescriptor("Zoom", "Zoom", CameraControlProperty.Zoom),
            CamControlProperty.CreateDescriptor("Pan", "Pan", CameraControlProperty.Pan),
            CamControlProperty.CreateDescriptor("Tilt", "Tilt", CameraControlProperty.Tilt),
            CamControlProperty.CreateDescriptor("Roll", "Roll", CameraControlProperty.Roll),
            CamControlProperty.CreateDescriptor("Iris", "Iris", CameraControlProperty.Iris),

            VideoProcAmpCameraProperty.CreateDescriptor("Brightness", "Brightness", VideoProcAmpProperty.Brightness),
            VideoProcAmpCameraProperty.CreateDescriptor("Contrast", "Contrast", VideoProcAmpProperty.Contrast),
            VideoProcAmpCameraProperty.CreateDescriptor("Hue", "Hue", VideoProcAmpProperty.Hue),
            VideoProcAmpCameraProperty.CreateDescriptor("Saturation", "Saturation", VideoProcAmpProperty.Saturation),
            VideoProcAmpCameraProperty.CreateDescriptor("Sharpness", "Sharpness", VideoProcAmpProperty.Sharpness),
            VideoProcAmpCameraProperty.CreateDescriptor("Gamma", "Gamma", VideoProcAmpProperty.Gamma),
            VideoProcAmpCameraProperty.CreateDescriptor("ColorEnable", "Color Enable", VideoProcAmpProperty.ColorEnable),
            VideoProcAmpCameraProperty.CreateDescriptor("WhiteBalance", "White Balance", VideoProcAmpProperty.WhiteBalance),
            VideoProcAmpCameraProperty.CreateDescriptor("BacklightCompensation", "Backlight Compensation", VideoProcAmpProperty.BacklightCompensation),
            VideoProcAmpCameraProperty.CreateDescriptor("Gain", "Gain", VideoProcAmpProperty.Gain)
        };

        public static System.Collections.Generic.IReadOnlyList<CameraPropertyDescriptor> SupportedProperties
        {
            get { return s_supportedProperties.AsReadOnly(); }
        }

        private void RegisterProperties()
        {
            foreach (var descriptor in Camera.SupportedProperties)
            {
                _cameraProperties[descriptor.Id] = descriptor.Create(this);
            }
        }

        public List<CameraProperty> GetProperties()
        {
            return _cameraProperties.Values.ToList();
        }

        public List<CameraProperty> GetSupportedProperties()
        {
            return _cameraProperties.Values.Where(p => p.Supported).ToList();
        }

        public void Refresh()
        {
            foreach (var prop in _cameraProperties.Values)
                prop.Refresh();
        }

        public void Save()
        {
            foreach (var prop in _cameraProperties.Values)
                prop.Save();
        }

        public static Camera Get(string deviceName)
        {
            var devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            var device = devices.FirstOrDefault(d => d.Name == deviceName);
            

            if (device == null)
                throw new ArgumentException(String.Format("Couldn't find device named {0}!", deviceName));

            return new Camera(device);
        }

        public static IEnumerable<CameraDevice> GetAll()
        {
            return DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice).Select(d => new CameraDevice(d));
        }

        public class CameraDevice
        {
            private DsDevice _device;
            public CameraDevice(DsDevice device)
            {
                _device = device;
            }

            public Camera Create()
            {
                return new Camera(_device);
            }

            public String Name { get { return _device.Name; } }
            public String DevicePath { get { return _device.DevicePath; } }
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                _disposedValue = true;
            }
        }

        ~Camera()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }


    [Flags]
    public enum CameraPropertyFlags
    {
        None = 0,
        Automatic = 1,
        Manual = 2,
        Relative = 16
    }

    public abstract class CameraPropertyDescriptor
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public abstract string Group { get; }

        public abstract CameraProperty Create(Camera camera);
    }


    public abstract class CameraProperty
    {
        public CameraPropertyDescriptor Descriptor { get; protected set; }
        public string Id { get { return Descriptor.Id; } }
        public string Name { get { return Descriptor.Id;  } }
        public bool Supported { get; protected set; }
        public int Min { get; protected set; }
        public int Max { get; protected set; }
        public int Default { get; protected set; }
        public int MinimumStepSize { get; protected set; }
        public CameraPropertyFlags Capabilities { get; protected set; }
        public int Value { get; set; }
        public CameraPropertyFlags Flags { get; set; }

        /// <summary>
        /// Updates the value and flags on the device
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// Gets the current value and flags on the device
        /// </summary>
        public abstract void Refresh();

        public event EventHandler<EventArgs> Saved;

        protected void OnSave()
        {
            Saved?.Invoke(this, new EventArgs());
        }

        public event EventHandler<EventArgs> Refreshed;

        protected void OnRefresh()
        {
            Refreshed?.Invoke(this, new EventArgs());
        }
    }


    internal class CamControlProperty : CameraProperty
    {
        private readonly IAMCameraControl _cameraControl;
        private readonly CameraControlProperty _cameraProperty;

        public CamControlProperty(CameraPropertyDescriptor descriptor, IAMCameraControl cameraControl, CameraControlProperty cameraProperty)
        {
            Descriptor = descriptor;
            _cameraControl = cameraControl;
            _cameraProperty = cameraProperty;
            UpdateRange();
        }

        void UpdateRange()
        {
            int min = 0, max = 0, stepping = 0, defaultValue = 0;
            CameraControlFlags flags = CameraControlFlags.None;

            if (_cameraControl == null)
                Supported = false;
            else
                Supported = _cameraControl.GetRange(_cameraProperty, out min, out max, out stepping, out defaultValue, out flags) == 0;

            Min = min;
            Max = max;
            Default = defaultValue;
            MinimumStepSize = stepping;
            Capabilities = (CameraPropertyFlags)flags;
        }

        public override void Save()
        {
            _cameraControl.Set(_cameraProperty, Value, (CameraControlFlags)Flags);
            OnSave();
        }

        public override void Refresh()
        {
            int value;
            CameraControlFlags flags;
            _cameraControl.Get(_cameraProperty, out value, out flags);
            Value = value;
            Flags = (CameraPropertyFlags)flags;
            OnRefresh();
        }

        internal static CameraPropertyDescriptor CreateDescriptor(string id, string name, CameraControlProperty cameraProperty)
        {
            return new CamControlPropertyDescriptor(id, name, cameraProperty);
        }

        internal class CamControlPropertyDescriptor : CameraPropertyDescriptor
        {
            private CameraControlProperty _cameraProperty;
            public override string Group { get { return "cameraControl"; } }

            public CamControlPropertyDescriptor(string id, string name, CameraControlProperty cameraProperty)
            {
                Id = id;
                Name = name;
                _cameraProperty = cameraProperty;
            }

            public override CameraProperty Create(Camera camera)
            {
                return new CamControlProperty(this, camera.Filter as IAMCameraControl, _cameraProperty);
            }
        }
    }
    
    internal class VideoProcAmpCameraProperty : CameraProperty
    {
        private readonly IAMVideoProcAmp _videoAmpControl;
        private readonly VideoProcAmpProperty _videoAmpProperty;

        public VideoProcAmpCameraProperty(CameraPropertyDescriptor descriptor, IAMVideoProcAmp videoAmpControl, VideoProcAmpProperty videoAmpProperty)
        {
            Descriptor = descriptor;
            _videoAmpControl = videoAmpControl;
            _videoAmpProperty = videoAmpProperty;
            UpdateRange();
        }

        void UpdateRange()
        {
            int min = 0, max = 0, stepping = 0, defaultValue = 0;
            VideoProcAmpFlags flags = VideoProcAmpFlags.None;

            if (_videoAmpControl == null)
                Supported = false;
            else
                Supported = _videoAmpControl.GetRange(_videoAmpProperty, out min, out max, out stepping, out defaultValue, out flags) == 0;

            Min = min;
            Max = max;
            Default = defaultValue;
            MinimumStepSize = stepping;
            Capabilities = (CameraPropertyFlags)flags;
        }

        public override void Save()
        {
            _videoAmpControl.Set(_videoAmpProperty, Value, (VideoProcAmpFlags)Flags);
            OnSave();
        }

        public override void Refresh()
        {
            int value;
            VideoProcAmpFlags flags;
            _videoAmpControl.Get(_videoAmpProperty, out value, out flags);
            Value = value;
            Flags = (CameraPropertyFlags)flags;
            OnRefresh();
        }

        internal static VideoProcAmpPropertyDescriptor CreateDescriptor(string id, string name, VideoProcAmpProperty videoProcProperty)
        {
            return new VideoProcAmpPropertyDescriptor(id, name, videoProcProperty);
        }

        internal class VideoProcAmpPropertyDescriptor : CameraPropertyDescriptor
        {
            private VideoProcAmpProperty _videoProcProperty;
            public override string Group { get { return "videoProcAmp"; } }

            public VideoProcAmpPropertyDescriptor(string id, string name, VideoProcAmpProperty cameraProperty)
            {
                Id = id;
                Name = name;
                _videoProcProperty = cameraProperty;
            }

            public override CameraProperty Create(Camera camera)
            {
                return new VideoProcAmpCameraProperty(this, camera.Filter as IAMVideoProcAmp, _videoProcProperty);
            }
        }
    }
}
