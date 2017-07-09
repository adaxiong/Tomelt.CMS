using System.Web;
using Tomelt.ContentManagement;

namespace Tomelt.Localization.Services {
    public interface ICultureFilter : IDependency {
        IContentQuery<ContentItem> FilterCulture(IContentQuery<ContentItem> query, string cultureName);
    }
}
