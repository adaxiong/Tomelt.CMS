using System.Collections.Generic;

namespace Tomelt.Alias.Implementation.Holder {
    public class AliasInfo {
        public string Area { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> RouteValues { get; set; }
        public bool IsManaged { get; set; }
    }
}