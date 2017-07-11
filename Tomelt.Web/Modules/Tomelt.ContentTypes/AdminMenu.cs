using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.ContentTypes {
    public class AdminMenu : INavigationProvider {

        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.AddImageSet("contenttypes");
        //    builder.Add(T("Content Definition"), "1.4.1", menu => {
        //        menu.LinkToFirstChild(true);

        //        menu.Add(T("Content Types"), "1", item => item.Action("Index", "Admin", new { area = "Tomelt.ContentTypes" }).Permission(Permissions.ViewContentTypes).LocalNav());
        //        menu.Add(T("Content Parts"), "2", item => item.Action("ListParts", "Admin", new { area = "Tomelt.ContentTypes" }).Permission(Permissions.ViewContentTypes).LocalNav());
        //    });
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
           
            builder.AddImageSet("ok")
                .Add(T("系统功能"), "88", menu =>
                {
                    menu.LinkToFirstChild(false);
                    menu.Add(T("内容类型"), "6.1",
                        item => item.Action("Index", "Admin", new { area = "Tomelt.ContentTypes" })
                            .Permission(Permissions.ViewContentTypes).LocalNav());
                    menu.Add(T("内容部件"), "6.2",
                        item => item.Action("ListParts", "Admin", new { area = "Tomelt.ContentTypes" })
                            .Permission(Permissions.ViewContentTypes).LocalNav());
                });
        }
    }
}