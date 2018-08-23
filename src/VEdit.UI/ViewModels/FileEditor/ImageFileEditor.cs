using System.Linq;
using VEdit.Common;
using VEdit.Editor;

namespace VEdit.UI
{
    public class ImageFileEditor : FileEditor
    {
        private int _scale;
        public int Scale
        {
            get => _scale;
            set
            {
                SetProperty(ref _scale, value);
                ScalePercent = (double)value / MaxScale;
            }
        }

        private double _scalePercent;
        public double ScalePercent
        {
            get => _scalePercent;
            set => SetProperty(ref _scalePercent, value * ScaleMultiplier);
        }

        public int ScaleInterval { get; }
        public int MaxScale { get; }
        public double ScaleMultiplier { get; }

        public ImageFileEditor(ProjectFile file, IServiceProvider serviceProvider) : base(file, serviceProvider)
        {
            MaxScale = 10;
            ScaleMultiplier = MaxScale / 2;
            ScaleInterval = 1;
            Scale = 2;

            Content = ServiceProvider.Get<IImage>();
        }

        private byte[] _bytes;
        public byte[] Bytes
        {
            get => _bytes;
            set => SetProperty(ref _bytes, value);
        }

        public IImage Content { get; }

        public override bool LoadContent()
        {
            Bytes = System.IO.File.ReadAllBytes(Path);
            return Content.TrySetBytes(Bytes);
        }

        public override void SaveContent()
        {
            System.IO.File.WriteAllBytes(Path, Bytes);
        }
    }
}
