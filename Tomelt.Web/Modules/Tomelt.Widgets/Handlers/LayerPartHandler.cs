using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Data;
using Tomelt.Widgets.Models;

namespace Tomelt.Widgets.Handlers {
    public class LayerPartHandler : ContentHandler {
        public LayerPartHandler(IRepository<LayerPartRecord> layersRepository) {
            Filters.Add(StorageFilter.For(layersRepository));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            var part = context.ContentItem.As<LayerPart>();

            if (part != null) {
                 context.Metadata.Identity.Add("Layer.LayerName", part.Name);
            }
        }
    }
}