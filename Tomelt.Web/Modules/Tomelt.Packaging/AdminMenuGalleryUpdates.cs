﻿using System.Linq;
using Tomelt.Environment.Extensions;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Localization;
using Tomelt.Mvc.Html;
using Tomelt.Packaging.Services;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Packaging {
    [TomeltFeature("Gallery.Updates")]
    public class AdminMenuGalleryUpdates : INavigationProvider {
        public Localizer T { get; set; }

        public string MenuName {
            get { return "admin"; }
        }

        readonly IBackgroundPackageUpdateStatus _backgroundPackageUpdateStatus;

        public AdminMenuGalleryUpdates(IBackgroundPackageUpdateStatus backgroundPackageUpdateStatus) {
            _backgroundPackageUpdateStatus = backgroundPackageUpdateStatus;
        }

        public void GetNavigation(NavigationBuilder builder) {
            int? modulesCount = GetUpdateCount(DefaultExtensionTypes.Module);
            var modulesCaption = modulesCount == null ? T("Updates") : T("Updates ({0})", modulesCount);

            int? themesCount = GetUpdateCount(DefaultExtensionTypes.Theme);
            var themesCaption = modulesCount == null ? T("Updates") : T("Updates ({0})", themesCount);

            builder
                .Add(T("Modules"), menu => menu
                    .Add(modulesCaption, "8", item => Describe(item, "ModulesUpdates", "GalleryUpdates", true)))
                .Add(T("Themes"), menu => menu
                    .Add(themesCaption, "8", item => Describe(item, "ThemesUpdates", "GalleryUpdates", true)));
        }

        private int? GetUpdateCount(string extensionType) {
            try {
                // Admin menu should never block, so simply return the result from the background task
                if (_backgroundPackageUpdateStatus.Value == null)
                    return null;

                return _backgroundPackageUpdateStatus.Value.Entries.Count(updatePackageEntry =>
                    updatePackageEntry.NewVersionToInstall != null &&
                        updatePackageEntry.ExtensionsDescriptor.ExtensionType == extensionType);
            }
            catch {
                return null;
            }
        }

        private static NavigationItemBuilder Describe(NavigationItemBuilder item, string actionName, string controllerName, bool localNav) {
            item = item.Action(actionName, controllerName, new { area = "Tomelt.Packaging" }).Permission(StandardPermissions.SiteOwner);
            if (localNav)
                item = item.LocalNav();
            return item;
        }
    }
}