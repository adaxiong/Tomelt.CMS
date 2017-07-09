using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.Core.Common.DateEditor {
    public class DateEditorSettings {
        public bool ShowDateEditor { get; set; }
    }

    public class DateEditorSettingsEvents : ContentDefinitionEditorEventsBase {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name == "CommonPart") {
                var model = definition.Settings.GetModel<DateEditorSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name == "CommonPart") {
                var model = new DateEditorSettings();
                if (updateModel.TryUpdateModel(model, "DateEditorSettings", null, null)) {
                    builder.WithSetting("DateEditorSettings.ShowDateEditor", model.ShowDateEditor.ToString());
                }
                yield return DefinitionTemplate(model);
            }
        }
    }
}