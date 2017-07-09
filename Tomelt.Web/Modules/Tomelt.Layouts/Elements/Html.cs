using Tomelt.Localization;

namespace Tomelt.Layouts.Elements {
    public class Html : ContentElement {
        public override LocalizedString DisplayText {
            get { return T("HTML"); }
        }
    }
}