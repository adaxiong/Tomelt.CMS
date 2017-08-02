using System.Linq;
using Tomelt.Caching;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Environment.Extensions;
using Tomelt.Tags.Models;
using Tomelt.Tags.Services;

namespace Tomelt.Tags.Handlers {
    [TomeltFeature("Tomelt.Tags.TagCloud")]
    public class TagCloudHandler : ContentHandler {
        private readonly ISignals _signals;

        public TagCloudHandler(
            ITagCloudService tagCloudService,
            ISignals signals) {

            _signals = signals;

            OnInitializing<TagCloudPart>((context, part) => part
                ._tagCountField.Loader(() =>
                    tagCloudService.GetPopularTags(part.Buckets, part.Slug).ToList()
                    ));

            OnPublished<TagsPart>((context, part) => InvalidateTagCloudCache());
            OnRemoved<TagsPart>((context, part) => InvalidateTagCloudCache());
            OnUnpublished<TagsPart>((context, part) => InvalidateTagCloudCache());
        }

        public void InvalidateTagCloudCache() {
            _signals.Trigger(TagCloudService.TagCloudTagsChanged);
        }
    }
}