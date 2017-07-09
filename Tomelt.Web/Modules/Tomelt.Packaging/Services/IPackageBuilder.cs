using System.IO;
using Tomelt.Environment.Extensions.Models;

namespace Tomelt.Packaging.Services {
    public interface IPackageBuilder : IDependency {
        Stream BuildPackage(ExtensionDescriptor extensionDescriptor);
    }
}