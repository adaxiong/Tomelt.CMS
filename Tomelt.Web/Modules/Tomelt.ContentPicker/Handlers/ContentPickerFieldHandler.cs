using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentPicker.Fields;

namespace Tomelt.ContentPicker.Handlers {
    public class ContentPickerFieldHandler : ContentHandler {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ContentPickerFieldHandler(
            IContentManager contentManager, 
            IContentDefinitionManager contentDefinitionManager) {
            
            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;
        }

        protected override void Loading(LoadContentContext context) {
            base.Loading(context);

            var fields = context.ContentItem.Parts.SelectMany(x => x.Fields.Where(f => f.FieldDefinition.Name == typeof (ContentPickerField).Name)).Cast<ContentPickerField>();
            
            // define lazy initializer for ContentPickerField.ContentItems
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(context.ContentType);
            if (contentTypeDefinition == null) {
                return;
            }

            foreach (var field in fields) {
                var localField = field;
                field._contentItems.Loader(() => _contentManager.GetMany<ContentItem>(localField.Ids, VersionOptions.Published, QueryHints.Empty));
            }
        }
    }
}