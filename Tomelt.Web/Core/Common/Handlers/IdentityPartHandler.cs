using System;
using Tomelt.ContentManagement;
using Tomelt.Core.Common.Models;
using Tomelt.Data;
using Tomelt.ContentManagement.Handlers;

namespace Tomelt.Core.Common.Handlers {
    public class IdentityPartHandler : ContentHandler {
        public IdentityPartHandler(IRepository<IdentityPartRecord> identityRepository,
            IContentManager contentManager) {
            Filters.Add(StorageFilter.For(identityRepository));
            OnInitializing<IdentityPart>((ctx, part) => AssignIdentity(part));
            OnCloning<IdentityPart>((ctx, part) => AssignIdentity(part));

            OnIndexing<IdentityPart>((context, part) => {
                context.DocumentIndex.Add("identifier", part.Identifier).Store();
            });
        }

        protected void AssignIdentity(IdentityPart part) {
            part.Identifier = Guid.NewGuid().ToString("n");
        }

        protected override void Cloning(CloneContentContext context) {
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            var part = context.ContentItem.As<IdentityPart>();

            if (part != null) {
                context.Metadata.Identity.Add("Identifier", part.Identifier);
            }
        }
    }
}