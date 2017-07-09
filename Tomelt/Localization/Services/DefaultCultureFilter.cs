using Tomelt.ContentManagement;

namespace Tomelt.Localization.Services {
    public class DefaultCultureFilter : ICultureFilter {
        public IContentQuery<ContentItem> FilterCulture(IContentQuery<ContentItem> query, string cultureName) {
            return query;
        }
    }
}
