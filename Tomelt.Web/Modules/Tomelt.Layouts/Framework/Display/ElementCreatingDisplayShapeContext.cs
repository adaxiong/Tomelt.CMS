using Tomelt.ContentManagement;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Framework.Display {
    public class ElementCreatingDisplayShapeContext {
        public IContent Content { get; set; }
        public Element Element { get; set; }
        public string DisplayType { get; set; }
        public bool Cancel { get; set; }
    }
}