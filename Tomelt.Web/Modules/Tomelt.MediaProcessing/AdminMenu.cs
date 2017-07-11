using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.MediaProcessing {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }

        public string MenuName {
            get { return "admin"; }
        }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.Add(T("Media"), "6", item => item.Add(T("Profiles"), "5", i => i.Action("Index", "Admin", new {area = "Tomelt.MediaProcessing"}).Permission(StandardPermissions.SiteOwner)));
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.AddImageSet("ok")
                .Add(T("系统功能"), "88", menu =>
                {
                    menu.LinkToFirstChild(false);
                    menu.Add(T("媒体管理"), "0",
                        item => item.Action("Index", "Admin", new { area = "Tomelt.MediaProcessing" })
                            .Permission(StandardPermissions.SiteOwner).LocalNav());
                });
        }
    }
}
