using Tomelt.MediaLibrary.Models;

namespace Tomelt.Layouts.ViewModels {
    public class VectorImageEditorViewModel {
        public string VectorImageId { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public VectorImagePart CurrentVectorImage { get; set; }
    }
}