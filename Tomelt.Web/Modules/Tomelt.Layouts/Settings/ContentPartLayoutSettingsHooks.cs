using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;
using Tomelt.Layouts.Helpers;

namespace Tomelt.Layouts.Settings {
    public class ContentPartLayoutSettingsHooks : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> PartEditor(ContentPartDefinition definition) {
            var model = definition.Settings.GetModel<ContentPartLayoutSettings>();
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> PartEditorUpdate(ContentPartDefinitionBuilder builder, IUpdateModel updateModel) {
            var model = new ContentPartLayoutSettings();
            updateModel.TryUpdateModel(model, "ContentPartLayoutSettings", null, null);
            builder.Placeable(model.Placeable);
            yield return DefinitionTemplate(model);
        }
    }
}