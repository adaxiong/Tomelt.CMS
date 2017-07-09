using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.MediaLibrary.Fields;

namespace Tomelt.MediaLibrary.ViewModels {

    public class MediaLibraryPickerFieldViewModel {

        public ICollection<ContentItem> ContentItems { get; set; }
        public string SelectedIds { get; set; }
        public MediaLibraryPickerField Field { get; set; }
        public ContentPart Part { get; set; }
    }
}