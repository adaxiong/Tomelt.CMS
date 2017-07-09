using Tomelt.Environment.Extensions.Models;

namespace Tomelt.Data.Migration {
    public interface IDataMigration : IDependency {
        Feature Feature { get; }
    }
}
