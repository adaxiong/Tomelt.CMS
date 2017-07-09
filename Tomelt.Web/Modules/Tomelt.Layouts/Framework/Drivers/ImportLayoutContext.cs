using Tomelt.Layouts.Models;

namespace Tomelt.Layouts.Framework.Drivers {
    public class ImportLayoutContext {
        public ILayoutAspect Layout { get; set; }
        public IContentImportSession Session { get; set; }
    }
}