using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Graphics.Imaging;
using Windows.Media.Effects;
using Windows.Media.MediaProperties;

namespace BackTask
{
    public sealed class PreserveCurrentFrameVideoEffect : IBasicVideoEffect
    {
        public SoftwareBitmap CurrentFrame { get; private set; }

        public void SetEncodingProperties(VideoEncodingProperties encodingProperties, IDirect3DDevice device)
        {

        }

        public void ProcessFrame(ProcessVideoFrameContext context)
        {
            var inputFrameBitmap = context.InputFrame.SoftwareBitmap;
            CurrentFrame = inputFrameBitmap;
        }

        public void Close(MediaEffectClosedReason reason)
        {

        }

        public void DiscardQueuedFrames()
        {

        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public IReadOnlyList<VideoEncodingProperties> SupportedEncodingProperties
        {
            get { return new List<VideoEncodingProperties>(); }
        }

        public MediaMemoryTypes SupportedMemoryTypes
        {
            get { return MediaMemoryTypes.Cpu; }
        }

        public bool TimeIndependent
        {
            get { return true; }
        }

        public void SetProperties(IPropertySet configuration)
        {

        }
    }
}
