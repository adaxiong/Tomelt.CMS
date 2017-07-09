using Tomelt.ContentManagement.Records;

namespace Tomelt.ContentManagement.Handlers {
    public class CreateContentContext : ContentContextBase {
        public CreateContentContext(ContentItem contentItem) : base(contentItem) {
            ContentItemVersionRecord = contentItem.VersionRecord;
        }

        public ContentItemVersionRecord ContentItemVersionRecord { get; set; }
    }
}
