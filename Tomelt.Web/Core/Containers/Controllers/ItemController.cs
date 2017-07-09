using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Tomelt.ContentManagement;
using Tomelt.Core.Common.Models;
using Tomelt.Core.Containers.Extensions;
using Tomelt.Core.Containers.Models;
using Tomelt.Core.Contents;
using Tomelt.Core.Feeds;
using Tomelt.DisplayManagement;
using Tomelt.Mvc;
using Tomelt.Themes;
using Tomelt.UI.Navigation;
using Tomelt.Settings;
using Tomelt.Localization;

namespace Tomelt.Core.Containers.Controllers {

    public class ItemController : Controller {
        private readonly IContentManager _contentManager;
        private readonly ISiteService _siteService;
        private readonly IFeedManager _feedManager;

        public ItemController(
            IContentManager contentManager, 
            IShapeFactory shapeFactory,
            ISiteService siteService,
            IFeedManager feedManager, 
            ITomeltServices services) {

            _contentManager = contentManager;
            _siteService = siteService;
            _feedManager = feedManager;
            Shape = shapeFactory;
            Services = services;
            T = NullLocalizer.Instance;
        }

        dynamic Shape { get; set; }
        public ITomeltServices Services { get; private set; }

        public Localizer T { get; set; }
        [Themed]
        public ActionResult Display(int id, PagerParameters pagerParameters) {
            var container = _contentManager
                .Get(id, VersionOptions.Published)
                .As<ContainerPart>();

            if (container == null) {
                return HttpNotFound(T("Container not found").Text);
            }

            if (!Services.Authorizer.Authorize(Permissions.ViewContent, container, T("Cannot view content"))) {
                return new HttpUnauthorizedResult();
            }

            // TODO: (PH) Find a way to apply PagerParameters via a driver so we can lose this controller
            container.PagerParameters = pagerParameters;
            var model = _contentManager.BuildDisplay(container);

            return new ShapeResult(this, model);
        }

    }
}