using Tomelt.Environment.Extensions;
using Tomelt.Layouts.Framework.Elements;
using Tomelt.Layouts.Helpers;

namespace Tomelt.Widgets.Layouts.Elements {
    [TomeltFeature("Tomelt.Widgets.Elements")]
    public class Widget : Element {
        public override string Category {
            get { return "Widgets"; }
        }

        public override bool IsSystemElement {
            get { return true; }
        }

        public override bool HasEditor {
            get { return false; }
        }

        public int? WidgetId {
            get { return this.Retrieve(x => x.WidgetId); }
            set { this.Store(x => x.WidgetId, value); }
        }
    }
}