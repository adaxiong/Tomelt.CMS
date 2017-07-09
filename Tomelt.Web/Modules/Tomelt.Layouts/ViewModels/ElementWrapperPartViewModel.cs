using System.Collections.Generic;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Localization;

namespace Tomelt.Layouts.ViewModels {
    public class ElementWrapperPartViewModel {
        public IList<string> Tabs { get; set; }
        public EditorResult ElementEditorResult { get; set; }
        public IList<dynamic> ElementEditors { get; set; }
        public string ElementTypeName { get; set; }
        public LocalizedString ElementDisplayText { get; set; }
    }
}