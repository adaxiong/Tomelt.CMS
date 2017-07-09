using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;

namespace Tomelt.Environment.Extensions.Folders {
    public interface IExtensionFolders {
        IEnumerable<ExtensionDescriptor> AvailableExtensions();
    }
}