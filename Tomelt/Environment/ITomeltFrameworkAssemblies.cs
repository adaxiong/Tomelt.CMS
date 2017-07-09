using System.Collections.Generic;
using System.Reflection;

namespace Tomelt.Environment {
    public interface ITomeltFrameworkAssemblies : IDependency {
        IEnumerable<AssemblyName> GetFrameworkAssemblies();
    }

    public class DefaultTomeltFrameworkAssemblies : ITomeltFrameworkAssemblies {
        public IEnumerable<AssemblyName> GetFrameworkAssemblies() {
            return typeof (IDependency).Assembly.GetReferencedAssemblies();
        }
    }
}
