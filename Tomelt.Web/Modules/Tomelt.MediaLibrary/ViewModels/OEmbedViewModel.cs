using Tomelt.MediaLibrary.Models;
using System.Xml.Linq;

namespace Tomelt.MediaLibrary.ViewModels {
    public class OEmbedViewModel {
        public string FolderPath { get; set; }
        public string Url { get; set; }
        public XDocument Content { get; set; }
        public MediaPart Replace { get; set; }
    }
}