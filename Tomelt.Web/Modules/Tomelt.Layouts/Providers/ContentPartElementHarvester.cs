using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.Core.Contents.Settings;
using Tomelt.Environment;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Elements;
using Tomelt.Layouts.Framework.Harvesters;
using Tomelt.Layouts.Services;
using Tomelt.Layouts.Settings;
using Tomelt.Utility.Extensions;

namespace Tomelt.Layouts.Providers {
    public class ContentPartElementHarvester : Component, IElementHarvester {
        private readonly Work<IContentDefinitionManager> _contentDefinitionManager;
        private readonly Work<IElementFactory> _elementFactory;
        private readonly Work<IElementManager> _elementManager;

        public ContentPartElementHarvester(
            Work<IContentDefinitionManager> contentDefinitionManager,
            Work<IElementFactory> elementFactory,
            Work<IElementManager> elementManager) {

            _contentDefinitionManager = contentDefinitionManager;
            _elementFactory = elementFactory;
            _elementManager = elementManager;
        }

        public IEnumerable<ElementDescriptor> HarvestElements(HarvestElementsContext context) {
            var elementType = typeof(Elements.ContentPart);
            var contentPartElement = _elementFactory.Value.Activate(elementType);
            var contentParts = GetContentParts(context);

            return contentParts.Select(contentPart => {

                var partSettings = contentPart.Settings.TryGetModel<ContentPartSettings>();
                var partDescription = partSettings != null ? partSettings.Description : null;
                var description = T(!String.IsNullOrWhiteSpace(partDescription) ? partDescription : contentPart.Name);
                return new ElementDescriptor(elementType, contentPart.Name, T(contentPart.Name.CamelFriendly()), description, contentPartElement.Category) {
                    Displaying = displayContext => Displaying(displayContext),
                    ToolboxIcon = "\uf1b2",
                    StateBag = new Dictionary<string, object> {
                        {"ElementTypeName", contentPart.Name}
                    }
                };
            });
        }

        private IEnumerable<ContentPartDefinition> GetContentParts(HarvestElementsContext context) {
            var contentTypeDefinition = context.Content != null
                ? _contentDefinitionManager.Value.GetTypeDefinition(context.Content.ContentItem.ContentType)
                : default(ContentTypeDefinition);

            var parts = contentTypeDefinition != null
                ? contentTypeDefinition.Parts.Select(x => x.PartDefinition)
                : _contentDefinitionManager.Value.ListPartDefinitions();

            return parts.Where(p => p.Settings.GetModel<ContentPartLayoutSettings>().Placeable);
        }

        private void Displaying(ElementDisplayingContext context) {
            var drivers = _elementManager.Value.GetDrivers(context.Element);

            foreach (var driver in drivers) {
                driver.Displaying(context);
            }
        }
    }
}