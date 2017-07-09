using Tomelt.ContentManagement.MetaData.Models;

namespace Tomelt.ContentManagement.FieldStorage {
    public interface IFieldStorageProvider : IDependency {
        string ProviderName { get; }
        
        IFieldStorage BindStorage(
            ContentPart contentPart, 
            ContentPartFieldDefinition partFieldDefinition);
    }
}