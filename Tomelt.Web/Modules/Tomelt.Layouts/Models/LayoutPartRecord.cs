using Tomelt.ContentManagement.Records;

namespace Tomelt.Layouts.Models {
    public class LayoutPartRecord : ContentPartVersionRecord {
        public virtual int? TemplateId { get; set; }
    }
}