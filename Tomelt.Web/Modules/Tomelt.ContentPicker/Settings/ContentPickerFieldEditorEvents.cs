using System.Collections.Generic;
using System.Globalization;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.ContentPicker.Settings {
    public class ContentPickerFieldEditorEvents : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition) {
            if (definition.FieldDefinition.Name == "ContentPickerField") {
                var model = definition.Settings.GetModel<ContentPickerFieldSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.FieldType != "ContentPickerField") {
                yield break;
            }

            var model = new ContentPickerFieldSettings();
            if (updateModel.TryUpdateModel(model, "ContentPickerFieldSettings", null, null)) {
                builder.WithSetting("ContentPickerFieldSettings.Hint", model.Hint);
                builder.WithSetting("ContentPickerFieldSettings.Required", model.Required.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("ContentPickerFieldSettings.Multiple", model.Multiple.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("ContentPickerFieldSettings.ShowContentTab", model.ShowContentTab.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("ContentPickerFieldSettings.DisplayedContentTypes", model.DisplayedContentTypes);
            }

            yield return DefinitionTemplate(model);
        }
    }
}