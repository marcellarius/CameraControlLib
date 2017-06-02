using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControlLib
{
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
        public string Name { get { return Descriptor.Name; } }
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

        public void ResetToDefault()
        {
            Value = Default;
            if (Capabilities.HasFlag(CameraPropertyFlags.Automatic))
                Flags = CameraPropertyFlags.Automatic;
            else
                Flags = CameraPropertyFlags.Manual;
            Flags &= Capabilities;
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

        public void UpdateRange()
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
