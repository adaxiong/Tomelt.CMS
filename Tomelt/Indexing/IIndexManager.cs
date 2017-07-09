namespace Tomelt.Indexing {
    public interface IIndexManager : IDependency {

        bool HasIndexProvider();
        IIndexProvider GetSearchIndexProvider();
    }
}