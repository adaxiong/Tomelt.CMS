using System.Globalization;
using Tomelt.Data;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Localization.Models;
using Tomelt.Localization.Services;

namespace Tomelt.Localization.Handlers {
    public class LocalizationPartHandler : ContentHandler {
        private readonly ICultureManager _cultureManager;
        private readonly IContentManager _contentManager;

        public LocalizationPartHandler(IRepository<LocalizationPartRecord> localizedRepository, ICultureManager cultureManager, IContentManager contentManager) {
            _cultureManager = cultureManager;
            _contentManager = contentManager;
            T = NullLocalizer.Instance;

            Filters.Add(StorageFilter.For(localizedRepository));

            OnActivated<LocalizationPart>(PropertySetHandlers);

            OnLoading<LocalizationPart>((context, part) => LazyLoadHandlers(part));
            OnVersioning<LocalizationPart>((context, part, versionedPart) => LazyLoadHandlers(versionedPart));

            OnIndexed<LocalizationPart>((context, localized) => context.DocumentIndex
                .Add("culture", CultureInfo.GetCultureInfo(localized.Culture != null ? localized.Culture.Culture : _cultureManager.GetSiteCulture()).LCID).Store()
                );
        }

        public Localizer T { get; set; }

        protected static void PropertySetHandlers(ActivatedContentContext context, LocalizationPart localizationPart) {
            localizationPart.CultureField.Setter(cultureRecord => {
                localizationPart.Record.CultureId = cultureRecord.Id;
                return cultureRecord;
            });
            
            localizationPart.MasterContentItemField.Setter(masterContentItem => {
                localizationPart.Record.MasterContentItemId = masterContentItem.ContentItem.Id;
                return masterContentItem;
            });            
        }

        protected void LazyLoadHandlers(LocalizationPart localizationPart) {
            localizationPart.CultureField.Loader(() => 
                _cultureManager.GetCultureById(localizationPart.Record.CultureId));

            localizationPart.MasterContentItemField.Loader(() =>
                _contentManager.Get(localizationPart.Record.MasterContentItemId, VersionOptions.AllVersions));
        }
    }
}
