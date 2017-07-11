using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Workflows {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder
        //        .AddImageSet("workflows")
        //        .Add(T("Workflows"), "10",
        //        menu => menu
        //            .Add(T("Workflows"), "1.0",
        //                qi => qi.Action("Index", "Admin", new { area = "Tomelt.Workflows" }).Permission(StandardPermissions.SiteOwner).LocalNav())
        //    );
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder
                .AddImageSet("ok")
                .Add(T("系统功能"), "88",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("工作流"), "96",
                                qi => qi.Action("Index", "Admin", new {area = "Tomelt.Workflows"})
                                    .Permission(StandardPermissions.SiteOwner).LocalNav());
                    }
                );
        }
    }
}
