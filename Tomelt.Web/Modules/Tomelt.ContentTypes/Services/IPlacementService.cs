using System.Collections.Generic;
using Tomelt.ContentTypes.Settings;

namespace Tomelt.ContentTypes.Services {
    public interface IPlacementService : IDependency {
        IEnumerable<DriverResultPlacement> GetDisplayPlacement(string contentType);
        IEnumerable<DriverResultPlacement> GetEditorPlacement(string contentType);
        IEnumerable<string> GetZones();
    }
}