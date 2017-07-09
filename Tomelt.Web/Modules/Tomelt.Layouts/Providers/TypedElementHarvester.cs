using System.Collections.Generic;
using System.Linq;
using Tomelt.Environment;
using Tomelt.Layouts.Framework.Elements;
using Tomelt.Layouts.Framework.Harvesters;
using Tomelt.Layouts.Services;

namespace Tomelt.Layouts.Providers {
    public class TypedElementHarvester : IElementHarvester {
        private readonly Work<IElementManager> _elementManager;
        private readonly Work<IElementFactory> _factory;

        public TypedElementHarvester(Work<IElementManager> elementManager, Work<IElementFactory> factory) {
            _elementManager = elementManager;
            _factory = factory;
        }

        public IEnumerable<ElementDescriptor> HarvestElements(HarvestElementsContext context) {
            var drivers = _elementManager.Value.GetDrivers();
            var elementTypes = drivers
                .Select(x => x.GetType().BaseType.GenericTypeArguments[0])
                .Where(x => !x.IsAbstract && !x.IsInterface)
                .Distinct()
                .ToArray();

            return elementTypes.Select(elementType => {
                var element = _factory.Value.Activate(elementType);
                return new ElementDescriptor(elementType, element.Type, element.DisplayText, element.Description, element.Category) {
                    GetDrivers = () => _elementManager.Value.GetDrivers(element),
                    IsSystemElement = element.IsSystemElement,
                    EnableEditorDialog = element.HasEditor,
                    ToolboxIcon = element.ToolboxIcon
                };
            });
        }
    }
}