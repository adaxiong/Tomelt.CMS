using System.Collections.Generic;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Framework.Elements;
using Tomelt.Localization;

namespace Tomelt.Layouts.ViewModels {
    public class EditElementBlueprintViewModel {
        public EditElementBlueprintViewModel() {
            Tabs = new List<string>();
        }

        public EditorResult EditorResult { get; set; }
        public string TypeName { get; set; }
        public LocalizedString DisplayText { get; set; }
        public LocalizedString Description { get; set; }
        public string ElementData { get; set; }
        public IList<string> Tabs { get; set; }
        public CreateElementBlueprintViewModel Blueprint { get; set; }
        public Element BaseElement { get; set; }
    }
}