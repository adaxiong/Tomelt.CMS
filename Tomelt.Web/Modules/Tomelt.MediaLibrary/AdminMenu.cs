using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.MediaLibrary {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }

        public AdminMenu() {
            T = NullLocalizer.Instance;
        }

        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.AddImageSet("media-library")
        //        .Add(T("Media"), "6",
        //            menu => menu.Add(T("Media"), "0", item => item.Action("Index", "Admin", new { area = "Tomelt.MediaLibrary" })
        //                .Permission(Permissions.ManageOwnMedia)));
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("系统功能"), "88", menu =>
                {
                    menu.LinkToFirstChild(false);
                    menu.Add(T("媒体库"), "0",
                        item => item.Action("Index", "Admin", new { area = "Tomelt.MediaLibrary" })
                            .Permission(Permissions.ManageOwnMedia).LocalNav());
                });
        }
    }
}