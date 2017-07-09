using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentPicker.Fields;

namespace Tomelt.ContentPicker.ViewModels {

    public class ContentPickerFieldViewModel {

        public ICollection<ContentItem> ContentItems { get; set; }
        public string SelectedIds { get; set; }
        public ContentPickerField Field { get; set; }
        public ContentPart Part { get; set; }
    }
}