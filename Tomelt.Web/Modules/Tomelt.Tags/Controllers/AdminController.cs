using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tomelt.Localization;
using Tomelt.ContentManagement;
using Tomelt.Mvc;
using Tomelt.Mvc.AntiForgery;
using Tomelt.Mvc.Extensions;
using Tomelt.Tags.Drivers;
using Tomelt.Tags.Models;
using Tomelt.Tags.ViewModels;
using Tomelt.Tags.Services;
using Tomelt.UI.Navigation;

namespace Tomelt.Tags.Controllers {
    [ValidateInput(false)]
    public class AdminController : Controller {
        private readonly ITagService _tagService;

        public AdminController(ITomeltServices services, ITagService tagService) {
            Services = services;
            _tagService = tagService;
            T = NullLocalizer.Instance;
        }

        public ITomeltServices Services { get; set; }
        
        public Localizer T { get; set; }

        public ActionResult Index(PagerParameters pagerParameters) {
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Can't manage tags")))
                return new HttpUnauthorizedResult();

            IEnumerable<TagRecord> tags = _tagService.GetTags();

            var pager = new Pager(Services.WorkContext.CurrentSite, pagerParameters);
            var pagerShape = Services.New.Pager(pager).TotalItemCount(tags.Count());
            if (pager.PageSize != 0) {
                tags = tags.Skip(pager.GetStartIndex()).Take(pager.PageSize);
            }

            var entries = tags.Select(CreateTagEntry).ToList();
            var model = new TagsAdminIndexViewModel { Pager = pagerShape, Tags = entries };

            return View(model);
        }
        public ActionResult List()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("无权限")))
                return new HttpUnauthorizedResult();
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetList(TagsSearch search)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("无权限")))
                return new HttpUnauthorizedResult();
            IEnumerable<TagRecord> tags = _tagService.GetTags();
            if (!string.IsNullOrWhiteSpace(search.TagName))
            {
                tags = tags.Where(d => d.TagName.ToLower().Contains(search.TagName.ToLower()));
            }
            search.total = tags.Count();
            //分页
            int pageSize = search.rows ?? 10;
            var maxPagedCount = Services.WorkContext.CurrentSite.MaxPagedCount;
            if (maxPagedCount > 0 && pageSize > maxPagedCount)
                pageSize = maxPagedCount;
            int page = search.page ?? 1;
            string order = string.IsNullOrWhiteSpace(search.order) ? "desc" : search.order;
            tags = order == "desc" ? tags.OrderByDescending(d => d.Id).Skip((page - 1) * pageSize).Take(pageSize) : tags.OrderBy(d => d.Id).Skip((page - 1) * pageSize).Take(pageSize);
            return Json(new
            {
                search.total,
                rows = tags.Select(d => new
                {
                    d.TagName,
                    d.Id
                })

            });
        }
        [HttpPost]
        [FormValueRequired("submit.BulkEdit")]
        public ActionResult Index(FormCollection input) {
            var viewModel = new TagsAdminIndexViewModel {Tags = new List<TagEntry>(), BulkAction = new TagAdminIndexBulkAction()};
            
            if ( !TryUpdateModel(viewModel) ) {
                return View(viewModel);
            }

            IEnumerable<TagEntry> checkedEntries = viewModel.Tags.Where(t => t.IsChecked);
            switch (viewModel.BulkAction) {
                case TagAdminIndexBulkAction.None:
                    break;
                case TagAdminIndexBulkAction.Delete:
                    if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Couldn't delete tag")))
                        return new HttpUnauthorizedResult();

                    foreach (TagEntry entry in checkedEntries) {
                        _tagService.DeleteTag(entry.Tag.Id);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Index")]
        [FormValueRequired("submit.Create")]
        public ActionResult IndexCreatePOST() {
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Couldn't create tag")))
                return new HttpUnauthorizedResult();

            var viewModel = new TagsAdminCreateViewModel();

            if (TryUpdateModel(viewModel)) {
                if (viewModel.TagName.Intersect(TagsPartDriver.DisalowedChars).Any()) {
                    ModelState.AddModelError("_FORM", T("The tag \"{0}\" could not be added because it contains forbidden chars: {1}", viewModel.TagName, String.Join(", ", TagsPartDriver.DisalowedChars)));
                }
            }

            if(!ModelState.IsValid) {
                ViewData["CreateTag"] = viewModel;
                return RedirectToAction("Index");
            }

            _tagService.CreateTag(viewModel.TagName);
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult Create(string tagName)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Couldn't create tag")))
                return Json(new {State = 0, Msg = T("无权限").Text});

            if (tagName.Intersect(TagsPartDriver.DisalowedChars).Any())
            {
                return Json(new { State = 0, Msg = T("标签 \"{0}\" 新增失败，不能包含这些特殊字符: {1}", tagName, String.Join(", ", TagsPartDriver.DisalowedChars)).Text });
            }
            _tagService.CreateTag(tagName);
            return Json(new { State = 1, Msg = T("添加成功").Text });
        }
        public ActionResult Edit(int id) {
            TagRecord tagRecord = _tagService.GetTag(id);

            if(tagRecord == null) {
                return RedirectToAction("Index");
            }

            var viewModel = new TagsAdminEditViewModel {
                Id = tagRecord.Id,
                TagName = tagRecord.TagName,
            };

            ViewData["ContentItems"] = _tagService.GetTaggedContentItems(id, VersionOptions.Latest).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection input) {
            var viewModel = new TagsAdminEditViewModel();

            if ( !TryUpdateModel(viewModel) ) {
                return View(viewModel);
            }
            
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Couldn't edit tag")))
                return new HttpUnauthorizedResult();

            if (viewModel.TagName.Intersect(TagsPartDriver.DisalowedChars).Any()) {
                ModelState.AddModelError("_FORM", T("The tag \"{0}\" could not be modified because it contains forbidden chars: {1}", viewModel.TagName, String.Join(", ", TagsPartDriver.DisalowedChars)));
                return View(viewModel);
            }

            _tagService.UpdateTag(viewModel.Id, viewModel.TagName);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditAJAX(FormCollection input)
        {
            var viewModel = new TagsAdminEditViewModel();

            if (!TryUpdateModel(viewModel))
            {
                return Json(new { State = 0, Msg = T("数据校验失败").Text });
            }

            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Couldn't edit tag")))
                return Json(new { State = 0, Msg = T("无权限").Text });

            if (viewModel.TagName.Intersect(TagsPartDriver.DisalowedChars).Any())
            {
                return Json(new { State = 0, Msg = T("标签 \"{0}\" 修改失败，不能包含这些特殊字符: {1}", viewModel.TagName, String.Join(", ", TagsPartDriver.DisalowedChars)).Text });
            }

            _tagService.UpdateTag(viewModel.Id, viewModel.TagName);
            return Json(new { State = 1, Msg = T("修改成功").Text });
        }
        [HttpPost]
        public ActionResult Remove(int id, string returnUrl) {
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Couldn't remove tag")))
                return new HttpUnauthorizedResult();

            TagRecord tagRecord = _tagService.GetTag(id);

            if (tagRecord == null)
                return new HttpNotFoundResult();

            _tagService.DeleteTag(id);

            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult DeleteAJAX(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageTags, T("Couldn't remove tag")))
                return Json(new { State = 0, Msg = T("无权限").Text });

            TagRecord tagRecord = _tagService.GetTag(id);

            if (tagRecord == null)
                return Json(new { State = 0, Msg = T("标签不存在").Text });

            _tagService.DeleteTag(id);
            return Json(new { State = 1, Msg = T("删除成功").Text });

        }
        public JsonResult FetchSimilarTags(string snippet) {
            return Json(
                _tagService.GetTagsByNameSnippet(snippet).Select(tag => tag.TagName).ToList(),
                JsonRequestBehavior.AllowGet
            );
        }

        private static TagEntry CreateTagEntry(TagRecord tagRecord) {
            return new TagEntry {
                Tag = tagRecord,
                IsChecked = false,
            };
        }
    }
}
