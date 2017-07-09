using System.Linq;
using System.Web.Routing;
using Tomelt.DisplayManagement;
using Tomelt.DisplayManagement.Implementation;
using Tomelt.Environment.Extensions;
using Tomelt.UI.Admin;

namespace Tomelt.Localization.Services {
    [TomeltFeature("Tomelt.Localization.CultureSelector")]
    public class AdminCultureSelectorFactory : ShapeDisplayEvents {
        private readonly ICultureManager _cultureManager;
        private readonly WorkContext _workContext;

        public AdminCultureSelectorFactory(
            IWorkContextAccessor workContextAccessor, 
            IShapeFactory shapeFactory,
            ICultureManager cultureManager) {
            _cultureManager = cultureManager;
            _workContext = workContextAccessor.GetContext();
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }

        private bool IsActivable() {
            // activate on admin screen only
            if (AdminFilter.IsApplied(new RequestContext(_workContext.HttpContext, new RouteData())))
                return true;

            return false;
        }

        public override void Displaying(ShapeDisplayingContext context) {
            context.ShapeMetadata.OnDisplaying(displayedContext => {
                if (displayedContext.ShapeMetadata.Type == "Layout" && IsActivable()) {
                    var supportedCultures = _cultureManager.ListCultures().ToList();
                    if (supportedCultures.Count() > 1) {
                        _workContext.Layout.Header.Add(Shape.AdminCultureSelector(SupportedCultures: supportedCultures));
                    }
                }
            });
        }
    }
}