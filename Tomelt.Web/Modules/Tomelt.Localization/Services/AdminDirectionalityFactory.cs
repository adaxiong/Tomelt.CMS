using System.Web.Routing;
using Tomelt.ContentManagement;
using Tomelt.DisplayManagement.Implementation;
using Tomelt.Environment.Extensions;
using Tomelt.UI.Admin;

namespace Tomelt.Localization.Services {
    [TomeltFeature("Tomelt.Localization.CultureSelector")]
    public class AdminDirectionalityFactory : ShapeDisplayEvents {
        private readonly WorkContext _workContext;

        public AdminDirectionalityFactory(
            IWorkContextAccessor workContextAccessor) {
            _workContext = workContextAccessor.GetContext();
        }


        private bool IsActivable() {
            // activate on admin screen only
            if (AdminFilter.IsApplied(new RequestContext(_workContext.HttpContext, new RouteData())))
                return true;

            return false;
        }

        public override void Displaying(ShapeDisplayingContext context) {
            context.ShapeMetadata.OnDisplaying(displayedContext => {
                if (!IsActivable()) {
                    return;
                }

                if (context.ShapeMetadata.Type != "EditorTemplate" &&
                    context.ShapeMetadata.Type != "Zone")
                    return;

                ContentItem contentItem = context.Shape.ContentItem;

                // if not, check for ContentPart 
                if (contentItem == null) {
                    ContentPart contentPart = context.Shape.ContentPart;
                    if (contentPart != null) {
                        contentItem = contentPart.ContentItem;
                    }
                }

                var className = "content-" + _workContext.GetTextDirection(contentItem);

                if (!_workContext.Layout.Content.Classes.Contains(className))
                    _workContext.Layout.Content.Classes.Add(className);
            });
        }


    }
}