using System.Collections.Generic;
using System.Web.Routing;
using Tomelt.ContentManagement;

namespace Tomelt.UI.Navigation {
    public interface INavigationManager : IDependency {
        IEnumerable<MenuItem> BuildMenu(string menuName);
        IEnumerable<MenuItem> BuildMenu(IContent menu);
        IEnumerable<string> BuildImageSets(string menuName);
        string GetUrl(string menuItemUrl, RouteValueDictionary routeValueDictionary);
        string GetNextPosition(IContent menu);
    }
}