using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Modules {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }

        public string MenuName {
            get { return "admin"; }
        }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("modules")
                .Add(T("Modules"), "9", menu => menu.Action("Features", "Admin", new {area = "Tomelt.Modules"}).Permission(Permissions.ManageFeatures)
                    .Add(T("Features"), "0", item => item.Action("Features", "Admin", new {area = "Tomelt.Modules"}).Permission(Permissions.ManageFeatures).LocalNav())
                    .Add(T("Installed"), "1", item => item.Action("Index", "Admin", new { area = "Tomelt.Modules" }).Permission(StandardPermissions.SiteOwner).LocalNav())
                    .Add(T("Recipes"), "2", item => item.Action("Recipes", "Admin", new { area = "Tomelt.Modules" }).Permission(StandardPermissions.SiteOwner).LocalNav())
                    );
        }
    }
}
