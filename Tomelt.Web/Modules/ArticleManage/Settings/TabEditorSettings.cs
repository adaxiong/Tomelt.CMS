using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace ArticleManage.Settings
{
    public class TabEditorSettings
    {
        public TabEditorSettings()
        {
            IsTab = false;
        }
        public bool IsTab { get; set; }
    }

    public class TabEditorSettingsEvents : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name == "ArticlePart")
            {
                var model = definition.Settings.GetModel<TabEditorSettings>();
                yield return DefinitionTemplate(model);
            }
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name == "ArticlePart")
            {
                var model = new TabEditorSettings();
                if (updateModel.TryUpdateModel(model, "TabEditorSettings", null, null))
                {
                    builder.WithSetting("TabEditorSettings.IsTab", model.IsTab.ToString());
                }
                yield return DefinitionTemplate(model);
            }
        }
    }
}