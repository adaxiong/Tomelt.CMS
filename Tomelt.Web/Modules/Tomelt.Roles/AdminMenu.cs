using Tomelt.Localization;
using Tomelt.UI.Navigation;
using Tomelt.Security;

namespace Tomelt.Roles {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.Add(T("Users"),
        //        menu => menu.Add(T("Roles"), "2.0", item => item.Action("Index", "Admin", new { area = "Tomelt.Roles" })
        //            .LocalNav().Permission(Permissions.ManageRoles)));
        //}
        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("系统功能"), "88",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("角色功能"), "0",
                            item => item.Action("Index", "Admin", new { area = "Tomelt.Roles" })
                                .Permission(Permissions.ManageRoles).LocalNav());
                    });
        }
    }
}
