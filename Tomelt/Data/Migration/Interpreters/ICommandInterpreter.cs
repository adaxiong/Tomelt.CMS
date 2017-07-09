using Tomelt.Data.Migration.Schema;

namespace Tomelt.Data.Migration.Interpreters {
    /// <summary>
    /// This interface can be implemented to provide a data migration behavior
    /// </summary>
    public interface ICommandInterpreter<in T> : ICommandInterpreter
    where T : ISchemaBuilderCommand {
        string[] CreateStatements(T command);
    }

    public interface ICommandInterpreter : IDependency {
        string DataProvider { get; }
    }
}
