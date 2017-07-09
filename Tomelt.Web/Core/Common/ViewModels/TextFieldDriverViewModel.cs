using Tomelt.ContentManagement;
using Tomelt.Core.Common.Fields;
using Tomelt.Core.Common.Settings;

namespace Tomelt.Core.Common.ViewModels {
    public class TextFieldDriverViewModel {
        public TextField Field { get; set; }
        public string Text { get; set; }
        public TextFieldSettings Settings { get; set; }
        public IContent ContentItem { get; set; }
    }
}