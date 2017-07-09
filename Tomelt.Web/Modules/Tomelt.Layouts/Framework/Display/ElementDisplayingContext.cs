using Tomelt.ContentManagement;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Framework.Display {
    public class ElementDisplayingContext {
        public IContent Content { get; set; }
        public Element Element { get; set; }
        public string DisplayType { get; set; }
        public dynamic ElementShape { get; set; }
        public IUpdateModel Updater { get; set; }
    }
}