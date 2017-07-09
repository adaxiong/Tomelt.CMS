using System.Collections.Generic;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.ViewModels {
    public class MediaManagerMediaItemsViewModel {
        public IList<MediaManagerMediaItemViewModel> MediaItems { get; set; }
        public int MediaItemsCount { get; set; }
        public string FolderPath { get; set; }
    }

    public class MediaManagerMediaItemViewModel {
        public MediaPart MediaPart { get; set; }
        public dynamic Shape { get; set; }
    }
}