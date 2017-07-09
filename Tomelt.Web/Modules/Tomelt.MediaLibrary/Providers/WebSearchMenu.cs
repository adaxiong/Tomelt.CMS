using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.MediaLibrary.Providers {
    public class WebSearchMenu : INavigationProvider {
        public Localizer T { get; set; }

        public WebSearchMenu() {
            T = NullLocalizer.Instance;
        }

        public string MenuName { get { return "mediaproviders"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("websearch")
                .Add(T("Web Search"), "7", 
                    menu => menu.Action("Index", "WebSearch", new { area = "Tomelt.MediaLibrary" })
                        .Permission(Permissions.ManageOwnMedia));
        }
    }
}