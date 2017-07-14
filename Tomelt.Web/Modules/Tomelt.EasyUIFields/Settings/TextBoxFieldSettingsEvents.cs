using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.EasyUIFields.Settings
{
    public class TextBoxFieldListModeEvents : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition)
        {
            if (definition.FieldDefinition.Name == "TextBoxField")
            {
                var model = definition.Settings.GetModel<TextBoxFieldSettings>();
                yield return DefinitionTemplate(model);
            }
        }
        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.FieldType != "TextBoxField")
            {
                yield break;
            }

            var model = new TextBoxFieldSettings();
            if (updateModel.TryUpdateModel(model, "TextBoxFieldSettings", null, null))
            {
                builder.WithSetting("TextBoxFieldSettings.Width", model.Width);
                builder.WithSetting("TextBoxFieldSettings.Height", model.Height.ToString());
                builder.WithSetting("TextBoxFieldSettings.Readonly", model.Readonly.ToString().ToLower());
                builder.WithSetting("TextBoxFieldSettings.Editable", model.Editable.ToString().ToLower());
                builder.WithSetting("TextBoxFieldSettings.Disabled", model.Disabled.ToString().ToLower());
                builder.WithSetting("TextBoxFieldSettings.Multiline", model.Multiline.ToString().ToLower());
                builder.WithSetting("TextBoxFieldSettings.Required", model.Required.ToString().ToLower());
                builder.WithSetting("TextBoxFieldSettings.Prompt", model.Prompt);
                builder.WithSetting("TextBoxFieldSettings.LabelAlign", model.LabelAlign);
                builder.WithSetting("TextBoxFieldSettings.LabelPosition", model.LabelPosition);
                builder.WithSetting("TextBoxFieldSettings.DefaultValue", model.DefaultValue);
            }

            yield return DefinitionTemplate(model);
        }
    }
}