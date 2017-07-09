using System;
using System.Linq;
using System.Web.Routing;
using Tomelt.Autoroute.Services;
using Tomelt.ContentManagement;
using Tomelt.Recipes.Models;
using Tomelt.Recipes.Services;

namespace Tomelt.Autoroute.Recipes.Executors {
    public class HomeAliasStep : RecipeExecutionStep {
        private readonly IContentManager _contentManager;
        private readonly IHomeAliasService _homeAliasService;

        public HomeAliasStep(RecipeExecutionLogger logger, IContentManager contentManager, IHomeAliasService homeAliasService) : base(logger) {
            _contentManager = contentManager;
            _homeAliasService = homeAliasService;
        }

        public override string Name { get { return "HomeAlias"; } }
        
        public override void Execute(RecipeExecutionContext context) {
            var root = context.RecipeStep.Step;
            var homePageIdentifier = root.Attr("Id");
            var homePageIdentity = !String.IsNullOrWhiteSpace(homePageIdentifier) ? new ContentIdentity(homePageIdentifier) : default(ContentIdentity);
            var homePage = homePageIdentity != null ? _contentManager.ResolveIdentity(homePageIdentity) : default(ContentItem);

            if (homePage != null)
                _homeAliasService.PublishHomeAlias(homePage);
            else {
                var routeValueDictionary = root.Elements().ToDictionary(x => x.Name.LocalName.ToLower(), x => (object) x.Value);
                _homeAliasService.PublishHomeAlias(new RouteValueDictionary(routeValueDictionary));
            }
        }
    }
}
