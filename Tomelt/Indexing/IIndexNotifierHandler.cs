using Tomelt.Events;

namespace Tomelt.Indexing {
    public interface IIndexNotifierHandler : IEventHandler {
        void UpdateIndex(string indexName);
    }
}
