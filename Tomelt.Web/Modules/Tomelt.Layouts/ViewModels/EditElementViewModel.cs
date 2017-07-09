using System.Collections.Generic;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Models;
using Tomelt.Localization;

namespace Tomelt.Layouts.ViewModels {
    public class EditElementViewModel {
        public EditElementViewModel() {
            Tabs = new List<string>();
        }

        public EditorResult EditorResult { get; set; }
        public string TypeName { get; set; }
        public LocalizedString DisplayText { get; set; }
        public string ElementData { get; set; }
        public string ElementHtml { get; set; }
        public ILayoutAspect Layout { get; set; }
        public IList<string> Tabs { get; set; }
        public bool Submitted { get; set; }
        public string SessionKey { get; set; }
        public object ElementEditorModel { get; set; }
    }
}