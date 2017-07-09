using Tomelt.ContentManagement;
using Tomelt.Layouts.Settings;

namespace Tomelt.Layouts.Models {
    public class ElementWrapperPart : ContentPart {
        public string ElementTypeName {
            get { return TypePartDefinition.Settings.GetModel<ElementWrapperPartSettings>().ElementTypeName; }
        }

        public string ElementData {
            get { return this.Retrieve(x => x.ElementData, versioned: true, defaultValue: this.Retrieve(x => x.ElementData)); }
            set { this.Store(x => x.ElementData, value, versioned: true); }
        }
    }
}