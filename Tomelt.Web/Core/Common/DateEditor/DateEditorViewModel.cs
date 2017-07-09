using Tomelt.Core.Common.ViewModels;
using Tomelt.DisplayManagement.Shapes;

namespace Tomelt.Core.Common.DateEditor {
    public class DateEditorViewModel : Shape {
        public virtual DateTimeEditor Editor { get; set; }
    }
}