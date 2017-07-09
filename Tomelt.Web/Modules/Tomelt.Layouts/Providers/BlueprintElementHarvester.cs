using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Environment;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Elements;
using Tomelt.Layouts.Framework.Harvesters;
using Tomelt.Layouts.Helpers;
using Tomelt.Layouts.Models;
using Tomelt.Layouts.Services;

namespace Tomelt.Layouts.Providers {
    public class BlueprintElementHarvester : Component, IElementHarvester {
        private readonly Work<IElementBlueprintService> _elementBlueprintService;
        private readonly Work<IElementManager> _elementManager;

        public BlueprintElementHarvester(Work<IElementBlueprintService> elementBlueprintService, Work<IElementManager> elementManager) {
            _elementBlueprintService = elementBlueprintService;
            _elementManager = elementManager;
        }

        public IEnumerable<ElementDescriptor> HarvestElements(HarvestElementsContext context) {
            if (context.IsHarvesting)
                return Enumerable.Empty<ElementDescriptor>();

            var blueprints = _elementBlueprintService.Value.GetBlueprints().ToArray();

            var query =
                from blueprint in blueprints
                let describeContext = new DescribeElementsContext {Content = context.Content, CacheVaryParam = "Blueprints", IsHarvesting = true }
                let baseElementDescriptor = _elementManager.Value.GetElementDescriptorByTypeName(describeContext, blueprint.BaseElementTypeName)
                let baseElement = _elementManager.Value.ActivateElement(baseElementDescriptor)
                select new ElementDescriptor(
                    baseElement.Descriptor.ElementType,
                    blueprint.ElementTypeName,
                    T(blueprint.ElementDisplayName),
                    T(!String.IsNullOrWhiteSpace(blueprint.ElementDescription) ? blueprint.ElementDescription : blueprint.ElementDisplayName),
                    GetCategory(blueprint)) {
                        EnableEditorDialog = false,
                        IsSystemElement = false,
                        CreatingDisplay = creatingDisplayContext => CreatingDisplay(creatingDisplayContext, blueprint),
                        Displaying = displayContext => Displaying(displayContext, baseElement),
                        StateBag = new Dictionary<string, object> {
                            {"Blueprint", true},
                            {"ElementTypeName", baseElement.Descriptor.TypeName}
                        }
                    };

            return query.ToArray();
        }

        private static string GetCategory(ElementBlueprint blueprint) {
            return !String.IsNullOrWhiteSpace(blueprint.ElementCategory) ? blueprint.ElementCategory : "Blueprints";
        }

        private void CreatingDisplay(ElementCreatingDisplayShapeContext context, ElementBlueprint blueprint) {
            var bluePrintState = ElementDataHelper.Deserialize(blueprint.BaseElementState);
            context.Element.Data = bluePrintState;
        }

        private void Displaying(ElementDisplayingContext context, Element element) {
            var drivers = _elementManager.Value.GetDrivers(element);

            foreach (var driver in drivers) {
                driver.Displaying(context);
            }
        }
    }
}