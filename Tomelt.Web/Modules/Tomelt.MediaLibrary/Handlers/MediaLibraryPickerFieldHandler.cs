using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.ContentManagement.MetaData;
using Tomelt.MediaLibrary.Fields;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.Handlers {
    public class MediaLibraryPickerFieldHandler : ContentHandler {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public MediaLibraryPickerFieldHandler(
            IContentManager contentManager, 
            IContentDefinitionManager contentDefinitionManager) {
            
            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;

        }

        protected override void Loaded(LoadContentContext context) {
            base.Loaded(context);
            InitilizeLoader(context.ContentItem);
        }

        private void InitilizeLoader(ContentItem contentItem) {
            var fields = contentItem.Parts.SelectMany(x => x.Fields.OfType<MediaLibraryPickerField>());

            // define lazy initializer for MediaLibraryPickerField.MediaParts
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(contentItem.ContentType);
            if (contentTypeDefinition == null) {
                return;
            }

            foreach (var field in fields) {
                var localField = field;
                localField._contentItems = new Lazy<IEnumerable<MediaPart>>(() => _contentManager.GetMany<MediaPart>(localField.Ids, VersionOptions.Published, QueryHints.Empty).ToList());
            }
        }
    }
}