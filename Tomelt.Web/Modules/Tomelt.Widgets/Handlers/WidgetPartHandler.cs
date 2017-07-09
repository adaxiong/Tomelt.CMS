using System.Web.Routing;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Title.Models;
using Tomelt.Data;
using Tomelt.Widgets.Models;

namespace Tomelt.Widgets.Handlers {
    public class WidgetPartHandler : ContentHandler {
        public WidgetPartHandler(IRepository<WidgetPartRecord> widgetsRepository) {
            Filters.Add(StorageFilter.For(widgetsRepository));

            OnInitializing<WidgetPart>((context, part) => part.RenderTitle = true);
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            var widget = context.ContentItem.As<WidgetPart>();

            if (widget == null)
                return;

            context.Metadata.DisplayText = widget.Title;

            // create needs to go through the add widget flow (index -> [select layer -> ] add [widget type] to layer)
            context.Metadata.CreateRouteValues = new RouteValueDictionary {
                {"Area", "Tomelt.Widgets"},
                {"Controller", "Admin"},
                {"Action", "Index"}
            };
            context.Metadata.EditorRouteValues = new RouteValueDictionary {
                {"Area", "Tomelt.Widgets"},
                {"Controller", "Admin"},
                {"Action", "EditWidget"},
                {"Id", context.ContentItem.Id}
            };
            // remove goes through edit widget...
            context.Metadata.RemoveRouteValues = new RouteValueDictionary {
                {"Area", "Tomelt.Widgets"},
                {"Controller", "Admin"},
                {"Action", "EditWidget"},
                {"Id", context.ContentItem.Id}
            };
        }
    }
}