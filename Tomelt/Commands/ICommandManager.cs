using System.Collections.Generic;

namespace Tomelt.Commands {
    public interface ICommandManager : IDependency {
        void Execute(CommandParameters parameters);
        IEnumerable<CommandDescriptor> GetCommandDescriptors();
    }
}