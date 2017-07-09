using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;
using Tomelt.Layouts.Helpers;

namespace Tomelt.Layouts.Settings {
    public class ContentTypeLayoutSettingsHooks : ContentDefinitionEditorEventsBase {
        public override IEnumerable<TemplateViewModel> TypeEditor(ContentTypeDefinition definition) {
            var model = definition.Settings.GetModel<ContentTypeLayoutSettings>();
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypeEditorUpdate(ContentTypeDefinitionBuilder builder, IUpdateModel updateModel) {
            var model = new ContentTypeLayoutSettings();
            updateModel.TryUpdateModel(model, "ContentTypeLayoutSettings", null, null);
            builder.Placeable(model.Placeable);
            yield return DefinitionTemplate(model);
        }
    }
}