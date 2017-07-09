using System.Collections.Generic;
using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.ViewModels;
using Tomelt.Templates.Services;
using Tomelt.Templates.ViewModels;

namespace Tomelt.Templates.Settings {
    public class ShapePartSettingsEvents : ContentDefinitionEditorEventsBase {
        private readonly IEnumerable<ITemplateProcessor> _processors;
        public ShapePartSettingsEvents(IEnumerable<ITemplateProcessor> processors) {
            _processors = processors;
        }

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "ShapePart")
                yield break;

            var settings = definition.Settings.GetModel<ShapePartSettings>();
            var model = new ShapePartSettingsViewModel {
                Processor = settings.Processor,
                AvailableProcessors = _processors.ToArray()
            };
            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "ShapePart")
                yield break;

            var model = new ShapePartSettingsViewModel {
                AvailableProcessors = _processors.ToArray()
            };

            updateModel.TryUpdateModel(model, "ShapePartSettingsViewModel", new[] { "Processor" }, null);
            builder.WithSetting("ShapePartSettings.Processor", model.Processor);
            yield return DefinitionTemplate(model);
        }
    }
}