using System.Collections.Generic;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.ViewModels {
    public class MediaManagerFolderCreateViewModel {
        public string Name { get; set; }
        public string FolderPath { get; set; }
        public IEnumerable<IMediaFolder> Hierarchy { get; set; }
    }
}
