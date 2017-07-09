using Tomelt.ContentManagement;
using Tomelt.Environment.Extensions;
using Tomelt.Localization.Models;

namespace Tomelt.Localization.Services {
    [TomeltSuppressDependency("Tomelt.Localization.Services.DefaultCultureFilter")]
    public class LocalizationCultureFilter : ICultureFilter {
        private readonly ICultureManager _cultureManager;

        public LocalizationCultureFilter(ICultureManager cultureManager) {
            _cultureManager = cultureManager;
        }

        public IContentQuery<ContentItem> FilterCulture(IContentQuery<ContentItem> query, string cultureName) {
            var culture = _cultureManager.GetCultureByName(cultureName);
            
            if (culture != null) {
                return query.Where<LocalizationPartRecord>(x => x.CultureId == culture.Id);
            }

            return query;
        }
    }
}
