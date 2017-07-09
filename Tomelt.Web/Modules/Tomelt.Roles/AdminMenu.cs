using Tomelt.Localization;
using Tomelt.UI.Navigation;
using Tomelt.Security;

namespace Tomelt.Roles {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Users"),
                menu => menu.Add(T("Roles"), "2.0", item => item.Action("Index", "Admin", new { area = "Tomelt.Roles" })
                    .LocalNav().Permission(Permissions.ManageRoles)));
        }
    }
}
