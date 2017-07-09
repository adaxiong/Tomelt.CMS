using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Elements {
    public class ContentPart : Element {
        public override string Category {
            get { return "ContentParts"; }
        }

        public override bool IsSystemElement {
            get { return true; }
        }

        public override bool HasEditor {
            get { return false; }
        }
    }
}