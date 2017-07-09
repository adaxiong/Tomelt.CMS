using Tomelt.ContentManagement;

namespace Tomelt.Themes.Models {
    public class ThemeSiteSettingsPart : ContentPart {
        public string CurrentThemeName {
            get { return this.Retrieve(x => x.CurrentThemeName); }
            set { this.Store(x => x.CurrentThemeName, value); }
        }
    }
}