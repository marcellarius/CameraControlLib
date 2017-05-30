using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CameraControlLib
{
    /// <summary>
    /// Provides access to the properties of a camera.
    /// </summary>
    /// <remarks>
    /// To create a Camera, use Camera.GetAll() to enumerate the 
    /// available video capture devices. This method returns a list of 
    /// camera descriptors and calling the Create() method on the camera
    /// descriptor will instantiate the Camera device.
    /// 
    /// The descriptors don't store any unmanaged resources and so don't need
    /// resource cleanup, but the Camera object should be disposed when 
    /// finished with it.
    /// </remarks>
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
        internal IAMCameraControl CameraControl {  get { return _filter as IAMCameraControl; } }
        internal IAMVideoProcAmp VideoProcAmp { get { return _filter as IAMVideoProcAmp; } }

        internal Camera(DsDevice device)
        {
            _device = device;
            IFilterGraph2 graphBuilder = new FilterGraph() as IFilterGraph2;
            IMoniker i = _device.Mon as IMoniker;
            graphBuilder.AddSourceFilterForMoniker(i, null, _device.Name, out _filter);

            RegisterProperties();
        }

        private readonly static List<CameraPropertyDescriptor> s_knownProperties = new List<CameraPropertyDescriptor>()
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

        public static System.Collections.Generic.IReadOnlyList<CameraPropertyDescriptor> KnownProperties
        {
            get { return s_knownProperties.AsReadOnly(); }
        }

        private void RegisterProperties()
        {
            foreach (var descriptor in Camera.KnownProperties)
            {
                _cameraProperties[descriptor.Id] = descriptor.Create(this);
            }
        }

        /// <summary>
        /// Gets a list of all the available properties, even if the camera doesn't appear to support them.
        /// </summary>
        /// <returns></returns>
        public List<CameraProperty> GetProperties()
        {
            return _cameraProperties.Values.ToList();
        }

        /// <summary>
        /// Gets a list of the properties supported by this camera.
        /// </summary>
        /// <returns></returns>
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

        public static IList<CameraDescriptor> GetAll()
        {
            return CameraDescriptor.GetAll();
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


    /// <summary>
    /// Represents an available Camera device, but has no unmanaged resources associated
    /// </summary>
    public class CameraDescriptor
    {
        public string Name { get; private set; }
        public String DevicePath { get; private set; }

        private CameraDescriptor() { }

        public Camera Create()
        {
            DsDevice matchingDevice = null;
            DsDevice[] cameraDevices = null;
            try
            {
                cameraDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
                var exactMatch = cameraDevices.FirstOrDefault(d => d.Name == Name && d.DevicePath == DevicePath);
                var nameMatch = cameraDevices.FirstOrDefault(d => d.Name == Name);
                matchingDevice = exactMatch ?? nameMatch;
                if (matchingDevice == null)
                    throw new InvalidOperationException("Could not find selected camera device");
                return new Camera(matchingDevice);
            }
            finally
            {
                DisposeDevices(cameraDevices, deviceToKeep: matchingDevice);
            }
        }

        public static List<CameraDescriptor> GetAll()
        {
            DsDevice[] cameraDevices = null;
            try
            {
                return DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice).Select(d => CameraDescriptor.FromDsDevice(d)).ToList();
            }
            finally
            {
                DisposeDevices(cameraDevices);
            }
        }

        internal static CameraDescriptor FromDsDevice(DsDevice device)
        {
            return new CameraDescriptor() { Name = device.Name, DevicePath = device.DevicePath };
        }

        private static void DisposeDevices(IEnumerable<DsDevice> devices, DsDevice deviceToKeep = null)
        {
            if (devices != null)
            {
                foreach (var device in devices)
                    if (device != null && device != deviceToKeep)
                        device.Dispose();
            }
        }
    }
}
