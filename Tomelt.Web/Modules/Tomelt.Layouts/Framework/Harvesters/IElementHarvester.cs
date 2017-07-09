using System.Collections.Generic;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Framework.Harvesters {
    public interface IElementHarvester : ISingletonDependency {
        IEnumerable<ElementDescriptor> HarvestElements(HarvestElementsContext context);
    }
}