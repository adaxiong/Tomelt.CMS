using Tomelt.Caching;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Data;
using Tomelt.MediaProcessing.Models;

namespace Tomelt.MediaProcessing.Handlers {
    public class ImageProfilePartHandler : ContentHandler {
        private readonly ISignals _signals;

        public ImageProfilePartHandler(IRepository<ImageProfilePartRecord> repository, ISignals signals) {
            _signals = signals;
            Filters.Add(StorageFilter.For(repository));
        }

        protected override void Published(PublishContentContext context) {
            if (context.ContentItem.Has<ImageProfilePart>())
                _signals.Trigger("MediaProcessing_Published_" + context.ContentItem.As<ImageProfilePart>().Name);
            base.Published(context);
        }
    }
}
