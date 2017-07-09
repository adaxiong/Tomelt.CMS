using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Localization;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.Handlers {
    public class WebSearchSettingsPartHandler : ContentHandler {
        public WebSearchSettingsPartHandler() {
            T = NullLocalizer.Instance;
            Filters.Add(new ActivatingFilter<WebSearchSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<WebSearchSettingsPart>("WebSearchSettings", "Parts/WebSearch.WebSearchSettings", "media"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Media")));
        }
    }
}