using Tomelt.Environment.Extensions;
using Tomelt.Layouts.Framework.Elements;
using Tomelt.Localization;

namespace Tomelt.Layouts.Elements {
    [TomeltFeature("Tomelt.Layouts.Snippets")]
    public class Snippet : Element {
        public override string Category {
            get { return "Snippets"; }
        }

        public override bool IsSystemElement {
            get { return true; }
        }

        public override bool HasEditor {
            get { return false; }
        }

        public override string ToolboxIcon {
            get { return "\uf10c"; }
        }
    }
}