using Tomelt.UI.Navigation;

namespace Tomelt.Templates {
    public class AdminMenu : Component, INavigationProvider {
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder
        //        .AddImageSet("templates")
        //        .Add(T("Templates"), "5.0", item => item.Action("List", "Admin", new { area = "Tomelt.Templates", id = "" }).Permission(Permissions.ManageTemplates));
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("系统功能"), "88",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("模板功能"), "0",
                            item => item.Action("List", "Admin", new { area = "Tomelt.Templates", id = "" })
                                .Permission(Permissions.ManageTemplates).LocalNav());
                    });
        }
    }
}