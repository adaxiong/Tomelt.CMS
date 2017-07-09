using System.Collections.Generic;
using Tomelt.ContentManagement;

namespace Tomelt.Layouts.ViewModels {
    public class ContentItemEditorViewModel {
        public string ContentItemIds { get; set; }
        public IList<ContentItem> ContentItems { get; set; }
        public string DisplayType { get; set; }
    }
}