using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;

namespace Tomelt.Core.Containers.Settings {
    public class ContainableTypePartSettings {
        public bool ShowContainerPicker { get; set; }
        public bool ShowPositionEditor { get; set; }
    }

    public class ContainableTypePartSettingsHooks : ContentDefinitionEditorEventsBase {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "ContainablePart")
                yield break;

            var model = definition.Settings.GetModel<ContainableTypePartSettings>();
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "ContainablePart")
                yield break;

            var model = new ContainableTypePartSettings();
            updateModel.TryUpdateModel(model, "ContainableTypePartSettings", null, null);
            builder.WithSetting("ContainableTypePartSettings.ShowContainerPicker", model.ShowContainerPicker.ToString());
            builder.WithSetting("ContainableTypePartSettings.ShowPositionEditor", model.ShowPositionEditor.ToString());
            yield return DefinitionTemplate(model);
        }
    }
}
