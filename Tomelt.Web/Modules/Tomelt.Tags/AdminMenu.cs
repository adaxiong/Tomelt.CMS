using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.Tags {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    builder.AddImageSet("tags")
        //        .Add(T("Tags"), "8",
        //            menu => menu.Add(T("List"), "0", item => item.Action("Index", "Admin", new { area = "Tomelt.Tags" })
        //                .Permission(Permissions.ManageTags)));
        //}
        public void GetNavigation(NavigationBuilder builder)
        {
            builder
                .Add(T("内容管理"), "66",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("内容标签"), "3", subitem => subitem
                            .Action("List", "Admin", new { area = "Tomelt.Tags" })
                            .Permission(Permissions.ManageTags));
                    });
        }
    }
}
