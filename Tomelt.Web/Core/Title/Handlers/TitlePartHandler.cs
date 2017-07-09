using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Title.Models;
using Tomelt.Data;

namespace Tomelt.Core.Title.Handlers {
    public class TitlePartHandler : ContentHandler {

        public TitlePartHandler(IRepository<TitlePartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
            OnIndexing<ITitleAspect>((context, part) => context.DocumentIndex.Add("title", part.Title).RemoveTags().Analyze());
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            var part = context.ContentItem.As<ITitleAspect>();

            if (part != null) {
                context.Metadata.DisplayText = part.Title;
            }
        }
    }
}
