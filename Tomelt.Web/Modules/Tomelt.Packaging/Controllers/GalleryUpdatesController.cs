﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tomelt.DisplayManagement;
using Tomelt.Environment.Configuration;
using Tomelt.Environment.Extensions;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Localization;
using Tomelt.Logging;
using Tomelt.Mvc.Extensions;
using Tomelt.Packaging.Models;
using Tomelt.Packaging.Services;
using Tomelt.Packaging.ViewModels;
using Tomelt.Security;
using Tomelt.Themes;
using Tomelt.UI.Admin;
using Tomelt.UI.Navigation;
using Tomelt.UI.Notify;

namespace Tomelt.Packaging.Controllers
{
	[TomeltFeature("Gallery.Updates")]
    [Themed, Admin]
    public class GalleryUpdatesController : Controller {
        private readonly ShellSettings _shellSettings;
        private readonly IPackagingSourceManager _packagingSourceManager;
        private readonly IPackageUpdateService _packageUpdateService;
        private readonly IBackgroundPackageUpdateStatus _backgroundPackageUpdateStatus;

        public GalleryUpdatesController(
            ShellSettings shellSettings,
            ITomeltServices services,
            IPackagingSourceManager packagingSourceManager,
            IPackageUpdateService packageUpdateService,
            IBackgroundPackageUpdateStatus backgroundPackageUpdateStatus,
            IShapeFactory shapeFactory) {

            _shellSettings = shellSettings;
            _packagingSourceManager = packagingSourceManager;
            _packageUpdateService = packageUpdateService;
            _backgroundPackageUpdateStatus = backgroundPackageUpdateStatus;

            Services = services;
            Shape = shapeFactory;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public ITomeltServices Services { get; private set; }
        public Localizer T { get; set; }
        public ILogger Logger { get; set; }
        public dynamic Shape { get; set; }

        public ActionResult ThemesUpdates(int? reportId, PagerParameters pagerParameters) {
            return PackageUpdate("ThemesUpdates", DefaultExtensionTypes.Theme, reportId, pagerParameters);
        }

        public ActionResult ModulesUpdates(int? reportId, PagerParameters pagerParameters) {
            return PackageUpdate("ModulesUpdates", DefaultExtensionTypes.Module, reportId, pagerParameters);
        }

        public ActionResult ReloadUpdates(string returnUrl) {
            _packageUpdateService.TriggerRefresh();
            _backgroundPackageUpdateStatus.Value = null;

            Services.Notifier.Warning(T("The feed has been notified for update. It might take a few minutes before the updates are displayed."));

            return this.RedirectLocal(returnUrl);
        }

        private ActionResult PackageUpdate(string view, string extensionType, int? reportId, PagerParameters pagerParameters) {
            if (_shellSettings.Name != ShellSettings.DefaultName || !Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to add sources")))
                return new HttpUnauthorizedResult();

            Pager pager = new Pager(Services.WorkContext.CurrentSite, pagerParameters);

            if (!_packagingSourceManager.GetSources().Any()) {
                Services.Notifier.Error(T("No Gallery feed configured"));
                return View(view, new PackagingListViewModel { Entries = new List<UpdatePackageEntry>() });
            }

            // Get status from background task state or directly
            _backgroundPackageUpdateStatus.Value =
                _backgroundPackageUpdateStatus.Value ??
                _packageUpdateService.GetPackagesStatus(_packagingSourceManager.GetSources());

            foreach (var error in _backgroundPackageUpdateStatus.Value.Errors) {
                for (var scan = error; scan != null; scan = scan.InnerException) {
                    Services.Notifier.Warning(T("Package retrieve error: {0}", scan.Message));
                }
            }

            IEnumerable<UpdatePackageEntry> updatedPackages = _backgroundPackageUpdateStatus.Value.Entries
                .Where(updatePackageEntry =>
                    updatePackageEntry.ExtensionsDescriptor.ExtensionType.Equals(extensionType) &&
                    updatePackageEntry.NewVersionToInstall != null)
                .ToList();

            int totalItemCount = updatedPackages.Count();

            if (pager.PageSize != 0) {
                updatedPackages = updatedPackages.Skip((pager.Page - 1) * pager.PageSize).Take(pager.PageSize);
            }

            return View(view, new PackagingListViewModel {
                LastUpdateCheckUtc = _backgroundPackageUpdateStatus.Value.DateTimeUtc,
                Entries = updatedPackages,
                Pager = Shape.Pager(pager).TotalItemCount(totalItemCount)
            });
        }
    }
}
