using System.Web.Hosting;

namespace Tomelt.FileSystems.VirtualPath {
    public interface ICustomVirtualPathProvider {
        VirtualPathProvider Instance { get; }
    }
}