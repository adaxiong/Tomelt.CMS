using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Elements {
    public abstract class UIElement : Element {
        public override string Category {
            get { return "UI"; }
        }

        public override string ToolboxIcon {
            get { return "\uf0c8"; }
        }
    }
}