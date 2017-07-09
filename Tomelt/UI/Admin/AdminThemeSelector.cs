using System.Web.Routing;
using Tomelt.Themes;

namespace Tomelt.UI.Admin {
    public class AdminThemeSelector : IThemeSelector {
        public ThemeSelectorResult GetTheme(RequestContext context) {
            if (AdminFilter.IsApplied(context)) {
                return new ThemeSelectorResult { Priority = 100, ThemeName = "TheAdmin" };
            }

            return null;
        }

    }
}
