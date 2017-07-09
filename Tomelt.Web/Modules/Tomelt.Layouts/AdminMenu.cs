using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.Layouts {
    public class AdminMenu : INavigationProvider {

        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder
                .AddImageSet("layouts")
                .Add(T("Layouts"), "8.5", layouts => layouts
                    .Action("List", "Admin", new {id = "Layout", area = "Contents"}).Permission(Permissions.ManageLayouts)
                    .LinkToFirstChild(false)
                    .Add(T("Elements"), "1", elements => elements.Action("Index", "BlueprintAdmin", new {area = "Tomelt.Layouts"}).Permission(Permissions.ManageLayouts)));
        }
    }
}