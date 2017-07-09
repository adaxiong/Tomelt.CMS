using Tomelt.Environment.Extensions;
using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Alias {
    [TomeltFeature("Tomelt.Alias.UI")]
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }

        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Aliases"), "1.4.1", menu => {
                menu.LinkToFirstChild(true);

                menu.Add(T("Unmanaged"), "1", item => item.Action("IndexUnmanaged", "Admin", new { area = "Tomelt.Alias" }).Permission(StandardPermissions.SiteOwner).LocalNav());
                menu.Add(T("Managed"), "2", item => item.Action("IndexManaged", "Admin", new { area = "Tomelt.Alias" }).Permission(StandardPermissions.SiteOwner).LocalNav());
            });
        }
    }
}
