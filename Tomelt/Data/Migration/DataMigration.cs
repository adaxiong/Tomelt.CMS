using Tomelt.ContentManagement.MetaData;
using Tomelt.Data.Migration.Schema;
using Tomelt.Environment.Extensions.Models;

namespace Tomelt.Data.Migration {
    /// <summary>
    /// Data Migration classes can inherit from this class to get a SchemaBuilder instance configured with the current tenant database prefix
    /// </summary>
    public abstract class DataMigrationImpl : IDataMigration {
        public virtual SchemaBuilder SchemaBuilder { get; set; }
        public virtual IContentDefinitionManager ContentDefinitionManager { get; set; }
        public virtual Feature Feature { get; set; }
    }
}