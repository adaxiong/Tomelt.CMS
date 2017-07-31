using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.Core.Common.BodyTabEditor
{
    public class BodyTabEditorSettings
    {
        public BodyTabEditorSettings()
        {
            IsTab = false;
        }
        public bool IsTab { get; set; }
    }

    public class TabEditorSettingsEvents : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name == "BodyPart")
            {
                var model = definition.Settings.GetModel<BodyTabEditorSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name == "BodyPart")
            {
                var model = new BodyTabEditorSettings();
                if (updateModel.TryUpdateModel(model, "BodyTabEditorSettings", null, null))
                {
                    builder.WithSetting("BodyTabEditorSettings.IsTab", model.IsTab.ToString());
                }
                yield return DefinitionTemplate(model);
            }
        }
    }
}