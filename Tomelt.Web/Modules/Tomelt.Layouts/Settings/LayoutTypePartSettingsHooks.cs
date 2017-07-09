using System;
using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;
using Tomelt.Layouts.Services;

namespace Tomelt.Layouts.Settings {
    public class LayoutTypePartSettingsHooks : ContentDefinitionEditorEventsBase {
        private readonly ILayoutSerializer _serializer;
        private readonly ILayoutManager _layoutManager;

        public LayoutTypePartSettingsHooks(ILayoutSerializer serializer, ILayoutManager layoutManager) {
            _serializer = serializer;
            _layoutManager = layoutManager;
        }

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "LayoutPart")
                yield break;

            var model = definition.Settings.GetModel<LayoutTypePartSettings>();
            
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "LayoutPart")
                yield break;

            var model = new LayoutTypePartSettings();
            updateModel.TryUpdateModel(model, "LayoutTypePartSettings", null, null);
            builder.WithSetting("LayoutTypePartSettings.IsTemplate", model.IsTemplate.ToString());
            builder.WithSetting("LayoutTypePartSettings.DefaultLayoutData", model.DefaultLayoutData);
            yield return DefinitionTemplate(model);
        }
    }
}