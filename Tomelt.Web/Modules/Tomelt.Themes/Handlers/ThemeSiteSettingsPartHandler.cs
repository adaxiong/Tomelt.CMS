using Tomelt.ContentManagement.Handlers;
using Tomelt.Themes.Models;

namespace Tomelt.Themes.Handlers {
    public class ThemeSiteSettingsPartHandler : ContentHandler {
        public ThemeSiteSettingsPartHandler() {
            Filters.Add(new ActivatingFilter<ThemeSiteSettingsPart>("Site"));
        }
    }
}