using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.ViewModels {
    public class ImportMediaViewModel {
        public string FolderPath { get; set; }
        public string Type { get; set; }
        public MediaPart Replace { get; set; }
    }
}