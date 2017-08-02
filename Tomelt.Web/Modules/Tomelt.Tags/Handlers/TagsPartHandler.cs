using Tomelt.Data;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Tags.Models;
using Tomelt.Tags.Services;

namespace Tomelt.Tags.Handlers {
    public class TagsPartHandler : ContentHandler {
        public TagsPartHandler(IRepository<TagsPartRecord> repository, ITagService tagService) {
            Filters.Add(StorageFilter.For(repository));
 
            OnRemoved<TagsPart>((context, tags) => 
                tagService.RemoveTagsForContentItem(context.ContentItem));

            OnIndexing<TagsPart>(
                (context, tagsPart) => {
                    foreach (var tag in tagsPart.CurrentTags) {
                        context.DocumentIndex.Add("tags", tag).Analyze();
                    }
                });
        }
    }
}