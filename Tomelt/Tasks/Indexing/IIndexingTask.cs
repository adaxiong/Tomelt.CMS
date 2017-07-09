using System;
using Tomelt.ContentManagement;

namespace Tomelt.Tasks.Indexing {
    public interface IIndexingTask {
        ContentItem ContentItem { get; }
        DateTime? CreatedUtc { get; }
    }
}
