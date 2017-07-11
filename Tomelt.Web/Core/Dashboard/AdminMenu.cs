using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Core.Dashboard {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.AddImageSet("dashboard")
        //        .Add(T("Dashboard"), "-5",
        //            menu => menu.Add(T("Tomelt"), "-5",
        //                item => item
        //                    .Action("Index", "Admin", new { area = "Dashboard" })
        //                    .Permission(StandardPermissions.AccessAdminPanel)));
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("至融系统"), "-5",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("系统首页"), "1", item => item.Action("Index", "Admin", new {area = "Dashboard"})
                            .Permission(StandardPermissions.AccessAdminPanel));
                    });
        }
    }
}