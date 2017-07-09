using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Users {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("users")
                .Add(T("Users"), "11",
                    menu => menu.Action("Index", "Admin", new { area = "Tomelt.Users" })
                        .Add(T("Users"), "1.0", item => item.Action("Index", "Admin", new { area = "Tomelt.Users" })
                            .LocalNav().Permission(Permissions.ManageUsers)));
        }
    }
}
