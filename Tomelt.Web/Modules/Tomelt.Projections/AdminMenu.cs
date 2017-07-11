using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Projections {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.AddImageSet("projector").Add(T("Queries"), "3",
        //        menu => menu
        //            .Add(T("Queries"), "1.0",
        //                qi => qi.Action("Index", "Admin", new { area = "Tomelt.Projections" }).Permission(Permissions.ManageQueries).LocalNav())
        //            .Add(T("Bindings"), "2.0", 
        //                bi => bi.Action("Index", "Binding", new { area = "Tomelt.Projections" }).Permission(StandardPermissions.SiteOwner).LocalNav())
        //    );
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("系统功能"), "88",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("咨询"), "3.1",
                                qi => qi.Action("Index", "Admin", new {area = "Tomelt.Projections"})
                                    .Permission(Permissions.ManageQueries).LocalNav())
                            .Add(T("绑定"), "3.2",
                                bi => bi.Action("Index", "Binding", new {area = "Tomelt.Projections"})
                                    .Permission(StandardPermissions.SiteOwner).LocalNav());
                    });
        }
    }
}
