using System.Web;
using Tomelt.ContentManagement.Drivers;
using Tomelt.Themes.Models;
using Tomelt.UI.Admin;

namespace Tomelt.Themes.Drivers {
    public class DisableThemePartDriver : ContentPartDriver<DisableThemePart> {
        private readonly HttpContextBase _httpContext;

        public DisableThemePartDriver(HttpContextBase httpContext) {
            _httpContext = httpContext;
        }

        protected override DriverResult Display(DisableThemePart part, string displayType, dynamic shapeHelper) {
            if (AdminFilter.IsApplied(_httpContext.Request.RequestContext)) {
                return null;
            }
            return ContentShape("Parts_DisableTheme", () => {
                ThemeFilter.Disable(_httpContext.Request.RequestContext);
                return null;
            });
        }
    }

}
