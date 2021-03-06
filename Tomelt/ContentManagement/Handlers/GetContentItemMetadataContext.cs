using Tomelt.Logging;

namespace Tomelt.ContentManagement.Handlers {
    public class GetContentItemMetadataContext {
        public ContentItem ContentItem { get; set; }
        public ContentItemMetadata Metadata { get; set; }
        public ILogger Logger { get; set; }
    }
}