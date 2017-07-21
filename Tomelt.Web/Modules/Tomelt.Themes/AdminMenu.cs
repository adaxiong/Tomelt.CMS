using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.Themes {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.AddImageSet("ok")
        //        .Add(T("Themes"), "10", menu => menu.Action("Index", "Admin", new { area = "Tomelt.Themes" }).Permission(Permissions.ApplyTheme)
        //            .Add(T("Installed"), "0", item => item.Action("Index", "Admin", new { area = "Tomelt.Themes" }).Permission(Permissions.ApplyTheme).LocalNav()));
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("至融系统"), "-5", menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("网站主题"), "0",
                            item => item.Action("Index", "Admin", new {area = "Tomelt.Themes"})
                                .Permission(Permissions.ApplyTheme));
                    });
        }
    }
}