using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Environment.Extensions;
using Tomelt.Layouts.Helpers;
using Tomelt.Widgets.Models;

namespace Tomelt.Widgets.Handlers {
    [TomeltFeature("Tomelt.Widgets.Elements")]
    public class WidgetElementsHandler : ContentHandler {
        private readonly ITomeltServices _tomeltServices;

        public WidgetElementsHandler(ITomeltServices tomeltServices) {
            _tomeltServices = tomeltServices;
            OnUpdated<WidgetPart>(PostProcessPlacedWidget);
        }

        private void PostProcessPlacedWidget(UpdateContentContext context, WidgetPart part) {
            if (!part.IsPlaceableContent())
                return;

            // This is a widget placed on a layout, so clear out the zone propertiey
            // to prevent the widget from appearing on the Widgets screen and on the front-end.
            part.Zone = null;

            // To prevent the widget from being recognized as being orphaned, set its container.
            // If the current container is a LayerPart, override that as well.
            var commonPart = part.As<ICommonPart>();
            if (commonPart != null && (commonPart.Container == null || commonPart.Container.Is<LayerPart>())) {
                commonPart.Container = _tomeltServices.WorkContext.CurrentSite;
            }
        }
    }
}