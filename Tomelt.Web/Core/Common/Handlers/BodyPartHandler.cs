using Tomelt.Core.Common.Models;
using Tomelt.Data;
using Tomelt.ContentManagement.Handlers;

namespace Tomelt.Core.Common.Handlers {
    public class BodyPartHandler : ContentHandler {       
        public BodyPartHandler(IRepository<BodyPartRecord> bodyRepository) {
            Filters.Add(StorageFilter.For(bodyRepository));

            OnIndexing<BodyPart>((context, bodyPart) => context.DocumentIndex
                                                                .Add("body", bodyPart.Record.Text).RemoveTags().Analyze()
                                                                .Add("format", bodyPart.Record.Format).Store());
        }
    }
}