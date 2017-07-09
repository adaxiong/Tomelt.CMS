using Tomelt.ContentManagement.Records;

namespace Tomelt.ContentPicker.Models {
    public class ContentMenuItemPartRecord : ContentPartRecord {
        public virtual ContentItemRecord ContentMenuItemRecord { get; set; }
    }
}