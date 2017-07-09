using System.Collections.Generic;

namespace Tomelt.Commands {
    public class CommandHandlerDescriptor {
        public IEnumerable<CommandDescriptor> Commands { get; set; }
    }
}
