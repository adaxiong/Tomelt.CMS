using Tomelt.ContentManagement;
using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.Settings;
using Tomelt.UI.Navigation;

namespace Tomelt.Core.Settings {
    public class AdminMenu : INavigationProvider {
        private readonly ISiteService _siteService;

        public AdminMenu(ISiteService siteService, ITomeltServices tomeltServices) {
            _siteService = siteService;
            Services = tomeltServices;
        }

        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }
        public ITomeltServices Services { get; private set; }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("settings")
                .Add(T("Settings"), "99",
                    menu => menu.Add(T("General"), "0", item => item.Action("Index", "Admin", new { area = "Settings", groupInfoId = "Index" })
                        .Permission(Permissions.ManageSettings)), new [] {"collapsed"});

            var site = _siteService.GetSiteSettings();
            if (site == null)
                return;

            foreach (var groupInfo in Services.ContentManager.GetEditorGroupInfos(site.ContentItem)) {
                GroupInfo info = groupInfo;
                builder.Add(T("Settings"),
                    menu => menu.Add(info.Name, info.Position, item => item.Action("Index", "Admin", new { area = "Settings", groupInfoId = info.Id })
                        .Permission(Permissions.ManageSettings)));
            }
        }
    }
}
