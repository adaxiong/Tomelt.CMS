using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.Tokens.Settings {
    public class RssPartSettings {

        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Enclosure { get; set; }
        public string PubDate { get; set; }
        public string Source { get; set; }
    }

    public class RssPartSettingsEvents : ContentDefinitionEditorEventsBase {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name == "RssPart") {
                var model = definition.Settings.GetModel<RssPartSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel upOwnerModel) {
            if (builder.Name == "RssPart") {
                var model = new RssPartSettings();
                if (upOwnerModel.TryUpdateModel(model, "RssPartSettings", null, null)) {
                    builder.WithSetting("RssPartSettings.Title", model.Title);
                    builder.WithSetting("RssPartSettings.Link", model.Link);
                    builder.WithSetting("RssPartSettings.Description", model.Description);
                    builder.WithSetting("RssPartSettings.Author", model.Author);
                    builder.WithSetting("RssPartSettings.Category", model.Category);
                    builder.WithSetting("RssPartSettings.Enclosure", model.Enclosure);
                    builder.WithSetting("RssPartSettings.PubDate", model.PubDate);
                    builder.WithSetting("RssPartSettings.Source", model.Source);
                }

                yield return DefinitionTemplate(model);
            }
        }
    }
}