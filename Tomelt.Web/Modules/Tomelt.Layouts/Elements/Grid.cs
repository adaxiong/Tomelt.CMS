using Tomelt.Layouts.Framework.Elements;
using Tomelt.Localization;

namespace Tomelt.Layouts.Elements {
    public class Grid : Container {
        public const int GridSize = 12;

        public override string Category {
            get { return "Layout"; }
        }

        public override LocalizedString DisplayText {
            get { return T("Grid"); }
        }

        public override bool IsSystemElement {
            get { return true; }
        }

        public override bool HasEditor {
            get { return false; }
        }
    }
}