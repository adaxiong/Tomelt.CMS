using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.Widgets {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.AddImageSet("widgets")
        //        .Add(T("Widgets"), "5",
        //            menu => menu.Add(T("Configure"), "0", item => item.Action("Index", "Admin", new { area = "Tomelt.Widgets" })
        //                .Permission(Permissions.ManageWidgets)));
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("系统功能"), "88",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("部件功能"), "95", item => item
                            .Action("Index", "Admin", new {area = "Tomelt.Widgets"})
                            .Permission(Permissions.ManageWidgets));
                    });
        }
    }
}
