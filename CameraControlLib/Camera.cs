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

        public CameraProperty Focus { get; private set; }
        public CameraProperty Exposure { get; private set; }
        public CameraProperty Zoom { get; private set; }
        public CameraProperty Pan { get; private set; }
        public CameraProperty Tilt { get; private set; }
        public CameraProperty Roll { get; private set; }
        public CameraProperty Iris { get; private set; }

        public CameraProperty Brightness { get; private set; }
        public CameraProperty Contrast { get; private set; }
        public CameraProperty Hue { get; private set; }
        public CameraProperty Saturation { get; private set; }
        public CameraProperty Sharpness { get; private set; }
        public CameraProperty Gamma { get; private set; }
        public CameraProperty ColorEnable { get; private set; }
        public CameraProperty WhiteBalance { get; private set; }
        public CameraProperty BacklightCompensation { get; private set; }
        public CameraProperty Gain { get; private set; }

        private Camera(DsDevice device)
        {
            _device = device;
            IFilterGraph2 graphBuilder = new FilterGraph() as IFilterGraph2;
            IMoniker i = _device.Mon as IMoniker;

            graphBuilder.AddSourceFilterForMoniker(i, null, _device.Name, out _filter);

            RegisterProperties();
        }

        private void RegisterProperties()
        {
            Func<CameraProperty, CameraProperty> registerProp = (prop) => { _cameraProperties[prop.Id] = prop; return prop; };

            IAMCameraControl cameraControl = _filter as IAMCameraControl;
            Focus = registerProp(new CamControlProperty("focus", "Focus", cameraControl, CameraControlProperty.Focus));
            Exposure = registerProp(new CamControlProperty("exposure", "Exposure time", cameraControl, CameraControlProperty.Exposure));
            Zoom = registerProp(new CamControlProperty("zoom", "Zoom", cameraControl, CameraControlProperty.Zoom));
            Pan = registerProp(new CamControlProperty("pan", "Pan", cameraControl, CameraControlProperty.Pan));
            Tilt = registerProp(new CamControlProperty("tilt", "Tilt", cameraControl, CameraControlProperty.Tilt));
            Roll = registerProp(new CamControlProperty("roll", "Roll", cameraControl, CameraControlProperty.Roll));
            Iris = registerProp(new CamControlProperty("iris", "Iris", cameraControl, CameraControlProperty.Iris));

            IAMVideoProcAmp videoProcAmp = _filter as IAMVideoProcAmp;
            Brightness = registerProp(new VideoProcAmpCameraProperty("Brightness", "Brightness", videoProcAmp, VideoProcAmpProperty.Brightness));
            Contrast = registerProp(new VideoProcAmpCameraProperty("Contrast", "Contrast", videoProcAmp, VideoProcAmpProperty.Contrast));
            Hue = registerProp(new VideoProcAmpCameraProperty("Hue", "Hue", videoProcAmp, VideoProcAmpProperty.Hue));
            Saturation = registerProp(new VideoProcAmpCameraProperty("Saturation", "Saturation", videoProcAmp, VideoProcAmpProperty.Saturation));
            Sharpness = registerProp(new VideoProcAmpCameraProperty("Sharpness", "Sharpness", videoProcAmp, VideoProcAmpProperty.Sharpness));
            Gamma = registerProp(new VideoProcAmpCameraProperty("Gamma", "Gamma", videoProcAmp, VideoProcAmpProperty.Gamma));
            ColorEnable = registerProp(new VideoProcAmpCameraProperty("ColorEnable", "Color Enable", videoProcAmp, VideoProcAmpProperty.ColorEnable));
            WhiteBalance = registerProp(new VideoProcAmpCameraProperty("WhiteBalance", "White Balance", videoProcAmp, VideoProcAmpProperty.WhiteBalance));
            BacklightCompensation = registerProp(new VideoProcAmpCameraProperty("BacklightCompensation", "Backlight Compensation", videoProcAmp, VideoProcAmpProperty.BacklightCompensation));
            Gain = registerProp(new VideoProcAmpCameraProperty("Gain", "Gain", videoProcAmp, VideoProcAmpProperty.Gain));

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


    public abstract class CameraProperty
    {
        public string Id { get; protected set; }
        public string Name { get; protected set; }
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

        public CamControlProperty(string id, string name, IAMCameraControl cameraControl, CameraControlProperty cameraProperty)
        {
            Id = id;
            Name = name;
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
    }
    
    internal class VideoProcAmpCameraProperty : CameraProperty
    {
        private readonly IAMVideoProcAmp _videoAmpControl;
        private readonly VideoProcAmpProperty _videoAmpProperty;

        public VideoProcAmpCameraProperty(string id, string name, IAMVideoProcAmp videoAmpControl, VideoProcAmpProperty videoAmpProperty)
        {
            Id = id;
            Name = name;
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
    }
}
