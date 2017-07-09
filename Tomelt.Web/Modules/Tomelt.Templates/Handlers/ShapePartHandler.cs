using Tomelt.Caching;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Templates.Models;
using Tomelt.Templates.Services;

namespace Tomelt.Templates.Handlers {
    public class ShapePartHandler : ContentHandler {
        private ISignals _signals;

        public ShapePartHandler(ISignals signals) {
            _signals = signals;

            OnPublished<ShapePart>((ctx, part) => InvalidateTemplatesCache());
            OnUnpublished<ShapePart>((ctx, part) => InvalidateTemplatesCache());
            OnRemoved<ShapePart>((ctx, part) => InvalidateTemplatesCache());
        }

        public void InvalidateTemplatesCache() {
            _signals.Trigger(DefaultTemplateService.TemplatesSignal);
        }
    }
}