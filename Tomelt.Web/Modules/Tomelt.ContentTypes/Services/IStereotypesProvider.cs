using System.Collections.Generic;

namespace Tomelt.ContentTypes.Services {
    public interface IStereotypesProvider : IDependency {
        IEnumerable<StereotypeDescription> GetStereotypes();
    }

    public class StereotypeDescription {
        public string Stereotype { get; set; }
        public string DisplayName { get; set; }
    }
}