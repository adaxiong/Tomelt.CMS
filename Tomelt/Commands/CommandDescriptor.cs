using System.Reflection;

namespace Tomelt.Commands {
    public class CommandDescriptor {
        public string Name { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public string HelpText { get; set; }
    }
}