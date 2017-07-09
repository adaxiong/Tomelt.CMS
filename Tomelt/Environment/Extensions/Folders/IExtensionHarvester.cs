using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;

namespace Tomelt.Environment.Extensions.Folders {
    public interface IExtensionHarvester {
        IEnumerable<ExtensionDescriptor> HarvestExtensions(IEnumerable<string> paths, string extensionType, string manifestName, bool manifestIsOptional);
    }
}