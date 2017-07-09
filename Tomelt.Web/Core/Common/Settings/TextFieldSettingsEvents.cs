using System.Collections.Generic;
using System.Globalization;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;
using Tomelt.Core.Common.ViewModels;

namespace Tomelt.Core.Common.Settings {
    public class TextFieldSettingsEvents : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition) {
            if (definition.FieldDefinition.Name == "TextField") {
                var model = new TextFieldSettingsEventsViewModel {
                    Settings = definition.Settings.GetModel<TextFieldSettings>(),
                };
                    
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.FieldType != "TextField") {
                yield break;
            }

            var model = new TextFieldSettingsEventsViewModel {
                Settings = new TextFieldSettings()
            };

            if (updateModel.TryUpdateModel(model, "TextFieldSettingsEventsViewModel", null, null)) {
                builder.WithSetting("TextFieldSettings.Flavor", model.Settings.Flavor);
                builder.WithSetting("TextFieldSettings.Hint", model.Settings.Hint);
                builder.WithSetting("TextFieldSettings.Required", model.Settings.Required.ToString(CultureInfo.InvariantCulture));
                builder.WithSetting("TextFieldSettings.DefaultValue", model.Settings.DefaultValue);

                yield return DefinitionTemplate(model);
            }
        }
    }
}