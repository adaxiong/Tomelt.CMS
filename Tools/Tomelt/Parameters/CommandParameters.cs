using System.Collections.Generic;

namespace Tomelt.Parameters {
    public class CommandParameters {
        public IList<string> Arguments { get; set; }
        public IDictionary<string, string> Switches { get; set; }
    }
}