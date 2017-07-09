using System.Collections.Generic;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.ViewModels {
    public class MediaManagerChildFoldersViewModel {
        public IEnumerable<IMediaFolder> Children { get; set; }
    }
}
