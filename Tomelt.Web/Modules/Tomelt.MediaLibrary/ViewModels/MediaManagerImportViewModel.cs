using System.Collections.Generic;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.UI.Navigation;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.ViewModels {
    public class MediaManagerImportViewModel {
        public IEnumerable<MenuItem> Menu { get; set; }
        public IEnumerable<string> ImageSets { get; set; }
        public string FolderPath { get; set; }
        public IEnumerable<ContentTypeDefinition> MediaTypes { get; set; }
        public MediaPart Replace { get; set; }
    }
}