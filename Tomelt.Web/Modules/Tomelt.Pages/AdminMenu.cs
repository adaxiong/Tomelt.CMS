using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.Pages {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("page");
        }
    }
}