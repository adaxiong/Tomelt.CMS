using System.Collections.Generic;

namespace Tomelt.Core.Common.Services {
    public interface IFlavorService : IDependency {
        IList<string> GetFlavors();
    }
}