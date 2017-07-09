using Tomelt.ContentManagement;
using Tomelt.Localization;
using Tomelt.UI.Navigation;
using System.Linq;

namespace Tomelt.Core.Navigation {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public ITomeltServices Services { get; set; }

        public AdminMenu(ITomeltServices services) {
            Services = services;
        }

        public void GetNavigation(NavigationBuilder builder) {
            var user = Services.WorkContext.CurrentUser;

            // if the current user cannot manage menus, check if they can manage at least one
            if (!Services.Authorizer.Authorize(Permissions.ManageMenus)) { 
                var menus = Services.ContentManager.Query("Menu").List();

                if (!menus.Any(x => Services.Authorizer.Authorize(Permissions.ManageMenus, x))) {
                    return;
                }
            }

            builder.AddImageSet("navigation")
                .Add(T("Navigation"), "7",
                    menu => menu
                        .Add(T("Main Menu"), "0", item => item.Action("Index", "Admin", new { area = "Navigation" })
                        ));
        }
    }
}
