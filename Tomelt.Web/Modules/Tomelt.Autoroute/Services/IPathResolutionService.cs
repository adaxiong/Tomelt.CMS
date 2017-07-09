using Tomelt.Autoroute.Models;

namespace Tomelt.Autoroute.Services {

    public interface IPathResolutionService : IDependency {
        AutoroutePart GetPath(string path);
    }
}
