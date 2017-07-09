using System.Linq;
using System.Web.Mvc;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.Core.Settings.Models;
using Tomelt.Localization;
using Tomelt.Logging;
using Tomelt.Mvc.Filters;
using Tomelt.Themes;
using Tomelt.UI.Admin;
using Tomelt.Widgets.Services;

namespace Tomelt.Widgets.Filters {
    public class WidgetFilter : FilterProvider, IResultFilter {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IWidgetsService _widgetsService;
        private readonly ITomeltServices _tomeltServices;
        private readonly ILayerEvaluationService _layerEvaluationService;

        public WidgetFilter(
            IWorkContextAccessor workContextAccessor,
            IWidgetsService widgetsService,
            ITomeltServices tomeltServices,
            ILayerEvaluationService layerEvaluationService) {
            _workContextAccessor = workContextAccessor;
            _widgetsService = widgetsService;
            _tomeltServices = tomeltServices;
            _layerEvaluationService = layerEvaluationService;
            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public ILogger Logger { get; set; }
        public Localizer T { get; private set; }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            // layers and widgets should only run on a full view rendering result
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult == null)
                return;

            var workContext = _workContextAccessor.GetContext(filterContext);

            if (workContext == null ||
                workContext.Layout == null ||
                workContext.CurrentSite == null ||
                AdminFilter.IsApplied(filterContext.RequestContext) ||
                !ThemeFilter.IsApplied(filterContext.RequestContext)) {
                return;
            }

            var widgetParts = _widgetsService.GetWidgets(_layerEvaluationService.GetActiveLayerIds());

            // Build and add shape to zone.
            var zones = workContext.Layout.Zones;
            var defaultCulture = workContext.CurrentSite.As<SiteSettingsPart>().SiteCulture;
            var currentCulture = workContext.CurrentCulture;

            foreach (var widgetPart in widgetParts) {
                var commonPart = widgetPart.As<ICommonPart>();
                if (commonPart == null || commonPart.Container == null) {
                    Logger.Warning("The widget '{0}' has no assigned layer or the layer does not exist.", widgetPart.Title);
                    continue;
                }

                // ignore widget for different cultures
                var localizablePart = widgetPart.As<ILocalizableAspect>();
                if (localizablePart != null) {
                    // if localized culture is null then show if current culture is the default
                    // this allows a user to show a content item for the default culture only
                    if (localizablePart.Culture == null && defaultCulture != currentCulture) {
                        continue;
                    }

                    // if culture is set, show only if current culture is the same
                    if (localizablePart.Culture != null && localizablePart.Culture != currentCulture) {
                        continue;
                    }
                }

                // check permissions
                if (!_tomeltServices.Authorizer.Authorize(Core.Contents.Permissions.ViewContent, widgetPart)) {
                    continue;
                }

                var widgetShape = _tomeltServices.ContentManager.BuildDisplay(widgetPart);
                zones[widgetPart.Zone].Add(widgetShape, widgetPart.Position);
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {
        }
    }
}
