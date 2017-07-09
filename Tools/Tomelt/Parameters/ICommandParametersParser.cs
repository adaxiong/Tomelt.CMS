using System.Collections.Generic;

namespace Tomelt.Parameters {
    public interface ICommandParametersParser {
        CommandParameters Parse(IEnumerable<string> args);
    }
}