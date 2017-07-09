﻿using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Core.Reports {
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("reports")
                .Add(T("Reports"), "12",
                    menu => menu.Add(T("View"), "0", item => item.Action("Index", "Admin", new { area = "Reports" })
                        .Permission(StandardPermissions.SiteOwner)));
        }
    }
}