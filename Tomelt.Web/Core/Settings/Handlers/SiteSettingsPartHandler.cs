using System;
using Tomelt.Core.Settings.Models;
using Tomelt.ContentManagement.Handlers;

namespace Tomelt.Core.Settings.Handlers {
    public class SiteSettingsPartHandler : ContentHandler {
        public SiteSettingsPartHandler() {
            Filters.Add(new ActivatingFilter<SiteSettingsPart>("Site"));

            OnInitializing<SiteSettingsPart>(InitializeSiteSettings);
        }

        private static void InitializeSiteSettings(InitializingContentContext initializingContentContext, SiteSettingsPart siteSettingsPart) {
            siteSettingsPart.SiteSalt = Guid.NewGuid().ToString("N");
            siteSettingsPart.SiteName = "My Tomelt Project Application";
            siteSettingsPart.PageTitleSeparator = " - ";
            siteSettingsPart.SiteTimeZone = TimeZoneInfo.Local.Id;
        }
    }
}