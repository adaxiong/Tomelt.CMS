using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Tomelt.ContentManagement;
using Tomelt.DisplayManagement;
using Tomelt.Forms.Services;
using Tomelt.Localization;
using Tomelt.MediaProcessing.Descriptors.Filter;
using Tomelt.MediaProcessing.Models;
using Tomelt.MediaProcessing.Services;
using Tomelt.MediaProcessing.ViewModels;
using Tomelt.Mvc;
using Tomelt.Security;
using Tomelt.Settings;
using Tomelt.UI.Navigation;
using Tomelt.UI.Notify;

namespace Tomelt.MediaProcessing.Controllers {
    [ValidateInput(false)]
    public class AdminController : Controller, IUpdateModel {
        private readonly ISiteService _siteService;
        private readonly IImageProfileService _profileService;
        private readonly IImageProcessingManager _imageProcessingManager;

        public AdminController(
            ITomeltServices services,
            IShapeFactory shapeFactory,
            ISiteService siteService,
            IImageProfileService profileService,
            IImageProcessingManager imageProcessingManager) {
            _siteService = siteService;
            _profileService = profileService;
            _imageProcessingManager = imageProcessingManager;
            Services = services;

            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        private dynamic Shape { get; set; }
        public ITomeltServices Services { get; set; }
        public Localizer T { get; set; }

        public ActionResult Index(AdminIndexOptions options, PagerParameters pagerParameters) {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to list media profiles")))
                return new HttpUnauthorizedResult();

            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);

            // default options
            if (options == null)
                options = new AdminIndexOptions();

            var profiles = Services.ContentManager.Query("ImageProfile");

            var pagerShape = Shape.Pager(pager).TotalItemCount(profiles.Count());

            profiles = profiles.Join<ImageProfilePartRecord>().OrderBy(u => u.Name);

            var results = profiles
                .Slice(pager.GetStartIndex(), pager.PageSize)
                .ToList();

            var model = new AdminIndexViewModel {
                ImageProfiles = results.Select(x => new ImageProfileEntry {
                    ImageProfile = x.As<ImageProfilePart>().Record,
                    ImageProfileId = x.Id,
                    Name = x.As<ImageProfilePart>().Name
                }).ToList(),
                Options = options,
                Pager = pagerShape
            };

            // maintain previous route data when generating page links
            var routeData = new RouteData();
            routeData.Values.Add("Options.Filter", options.Filter);

            pagerShape.RouteData(routeData);

            return View(model);
        }

        [HttpPost]
        [FormValueRequired("submit.BulkEdit")]
        public ActionResult Index(FormCollection input) {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to manage media profiles")))
                return new HttpUnauthorizedResult();

            var viewModel = new AdminIndexViewModel {ImageProfiles = new List<ImageProfileEntry>(), Options = new AdminIndexOptions()};
            UpdateModel(viewModel);

            var checkedItems = viewModel.ImageProfiles.Where(c => c.IsChecked);

            switch (viewModel.Options.BulkAction) {
                case ImageProfilesBulkAction.None:
                    break;
                case ImageProfilesBulkAction.Delete:
                    foreach (var checkedItem in checkedItems) {
                        _profileService.DeleteImageProfile(checkedItem.ImageProfileId);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to edit media profiles")))
                return new HttpUnauthorizedResult();

            var profile = _profileService.GetImageProfile(id);
            var viewModel = new AdminEditViewModel {
                Id = profile.Id,
                Name = profile.Name
            };

            var filterEntries = new List<FilterEntry>();
            var allFilters = _imageProcessingManager.DescribeFilters().SelectMany(x => x.Descriptors).ToList();

            foreach (var filter in profile.Filters.OrderBy(f => f.Position)) {
                var category = filter.Category;
                var type = filter.Type;

                var f = allFilters.FirstOrDefault(x => category == x.Category && type == x.Type);
                if (f != null) {
                    filterEntries.Add(
                        new FilterEntry {
                            Category = f.Category,
                            Type = f.Type,
                            FilterRecordId = filter.Id,
                            DisplayText = String.IsNullOrWhiteSpace(filter.Description) ? f.Display(new FilterContext {State = FormParametersHelper.ToDynamic(filter.State)}).Text : filter.Description
                        });
                }
            }

            viewModel.Filters = filterEntries;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to manage media profiles")))
                return new HttpUnauthorizedResult();

            var profile = _profileService.GetImageProfile(id);

            if (profile == null) {
                return HttpNotFound();
            }

            Services.ContentManager.Remove(profile.ContentItem);
            Services.Notifier.Information(T("Image Profile {0} deleted", profile.Name));

            return RedirectToAction("Index");
        }

        public ActionResult Move(string direction, int id, int filterId) {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to manage media profiles")))
                return new HttpUnauthorizedResult();

            switch (direction) {
                case "up":
                    _profileService.MoveUp(filterId);
                    break;
                case "down":
                    _profileService.MoveDown(filterId);
                    break;
                default:
                    throw new ArgumentException("direction");
            }

            return RedirectToAction("Edit", new {id});
        }

        public ActionResult Preview(int id) {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to manage media profiles")))
                return new HttpUnauthorizedResult();

            throw new NotImplementedException();
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        public void AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}
