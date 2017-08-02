using System;
using System.Linq;
using System.Web.Mvc;
using Tomelt.ContentManagement;
using Tomelt.DisplayManagement;
using Tomelt.Localization;
using Tomelt.Logging;
using Tomelt.Settings;
using Tomelt.Tags.Services;
using Tomelt.Tags.ViewModels;
using Tomelt.Themes;
using Tomelt.UI.Navigation;

namespace Tomelt.Tags.Controllers {
    [ValidateInput(false), Themed]
    public class HomeController : Controller {
        private readonly ITagService _tagService;
        private readonly IContentManager _contentManager;
        private readonly ISiteService _siteService;

        public HomeController(
            ITagService tagService,
            IContentManager contentManager,
            ISiteService siteService,
            IShapeFactory shapeFactory) {
            _tagService = tagService;
            _contentManager = contentManager;
            _siteService = siteService;
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }
        
        public ILogger Logger { get; set; }
        public Localizer T { get; set; }
        public dynamic Shape { get; set; }

        public ActionResult Index() {
            var tags = _tagService.GetTags();
            var model = new TagsIndexViewModel { Tags = tags.ToList() };
            return View(model);
        }

        public ActionResult Search(string tagName, PagerParameters pagerParameters) {
            Pager pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);

            var tag = _tagService.GetTagByName(tagName);

            if (tag == null) {
                return RedirectToAction("Index");
            }

            var taggedItems = _tagService.GetTaggedContentItems(tag.Id, pager.GetStartIndex(), pager.PageSize).ToList();
            var tagShapes = taggedItems.Select(item => _contentManager.BuildDisplay(item, "Summary"));

            var list = Shape.List();
            list.AddRange(tagShapes);

            var totalItemCount = _tagService.GetTaggedContentItemCount(tag.Id);
            var viewModel = new TagsSearchViewModel {
                TagName = tag.TagName,
                List = list,
                Pager = Shape.Pager(pager).TotalItemCount(totalItemCount)
            };

            return View(viewModel);
        }
    }
}
