using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.Core.Navigation.Settings {
    public class AdminMenuPartTypeSettings {
        public string DefaultPosition { get; set; }
    }

    public class AdminMenuSettingsHooks : ContentDefinitionEditorEventsBase {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "AdminMenuPart") {
                yield break;
            }

            var model = definition.Settings.GetModel<AdminMenuPartTypeSettings>();

            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "AdminMenuPart") {
                yield break;
            }

            var model = new AdminMenuPartTypeSettings();
            updateModel.TryUpdateModel(model, "AdminMenuPartTypeSettings", null, null);
            builder.WithSetting("AdminMenuPartTypeSettings.DefaultPosition", model.DefaultPosition);
            yield return DefinitionTemplate(model);
        }
    }
}
