using Tomelt.ContentManagement.MetaData.Models;

namespace Tomelt.ContentManagement.FieldStorage {
    public interface IFieldStorageProviderSelector : IDependency {
        IFieldStorageProvider GetProvider(ContentPartFieldDefinition partFieldDefinition);
    }
}