using System.Web.Routing;
using Tomelt.Environment.Extensions.Models;

namespace Tomelt.Themes {
    public interface IThemeManager : IDependency {
        ExtensionDescriptor GetRequestTheme(RequestContext requestContext);
    }
}
