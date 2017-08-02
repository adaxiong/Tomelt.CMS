using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace UEditor.Settings
{
    public class FilePickSettingsEvents : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> PartFieldEditor(ContentPartFieldDefinition definition)
        {
            if (definition.FieldDefinition.Name == "FilePickField")
            {
                var model = definition.Settings.GetModel<FilePickSettings>();
                yield return DefinitionTemplate(model);
            }
        }
        public override IEnumerable<TemplateViewModel> PartFieldEditorUpdate(ContentPartFieldDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.FieldType != "FilePickField")
            {
                yield break;
            }

            var model = new FilePickSettings();
            if (updateModel.TryUpdateModel(model, "FilePickSettings", null, null))
            {
                builder.WithSetting("FilePickSettings.Width", model.Width);
                builder.WithSetting("FilePickSettings.Height", model.Height.ToString());
                builder.WithSetting("FilePickSettings.Editable", model.Editable.ToString().ToLower());
                builder.WithSetting("FilePickSettings.Required", model.Required.ToString().ToLower());
                builder.WithSetting("FilePickSettings.Prompt", model.Prompt);
                builder.WithSetting("FilePickSettings.LabelAlign", model.LabelAlign);
                builder.WithSetting("FilePickSettings.LabelPosition", model.LabelPosition);
           
            }

            yield return DefinitionTemplate(model);
        }
    }
}