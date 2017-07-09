using Tomelt.Layouts.Elements;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;

namespace Tomelt.Layouts.Drivers {
    public class ColumnElementDriver : ElementDriver<Column> {

        protected override void OnDisplaying(Column element, ElementDisplayingContext context) {
            context.ElementShape.Width = element.Width;
            context.ElementShape.Offset = element.Offset;
            context.ElementShape.Collapsed = false;
        }
    }
}