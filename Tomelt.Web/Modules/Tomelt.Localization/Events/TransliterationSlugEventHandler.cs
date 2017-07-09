using Tomelt.Autoroute.Services;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.Environment.Extensions;
using Tomelt.Localization.Services;

namespace Tomelt.Localization.Events {
    [TomeltFeature("Tomelt.Localization.Transliteration.SlugGeneration")]
    public class TransliterationSlugEventHandler : ISlugEventHandler {
        private readonly ITransliterationService _transliterationService;

        public TransliterationSlugEventHandler(ITransliterationService transliterationService) {
            _transliterationService = transliterationService;
        }

        public void FillingSlugFromTitle(FillSlugContext context) {
            var localizationAspect = context.Content.As<ILocalizableAspect>();
            if (localizationAspect == null) return;

            context.Title = _transliterationService.Convert(context.Title, localizationAspect.Culture);
        }

        public void FilledSlugFromTitle(FillSlugContext context) {
        }
    }
}