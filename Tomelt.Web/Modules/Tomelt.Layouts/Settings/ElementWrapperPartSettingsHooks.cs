using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.Layouts.Settings {
    public class ElementWrapperPartSettingsHooks : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "ElementWrapperPart")
                yield break;

            var model = definition.Settings.GetModel<ElementWrapperPartSettings>();

            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "ElementWrapperPart")
                yield break;

            var model = new ElementWrapperPartSettings();
            updateModel.TryUpdateModel(model, "ElementWrapperPartSettings", null, null);
            builder.WithSetting("ElementWrapperPartSettings.ElementTypeName", model.ElementTypeName);
            yield return DefinitionTemplate(model);
        }
    }
}