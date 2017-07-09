using System.Collections.Generic;
using Tomelt.Layouts.Framework.Elements;
using Tomelt.Layouts.Models;

namespace Tomelt.Layouts.Services {
    public interface IElementBlueprintService : IDependency {
        ElementBlueprint GetBlueprint(int id);
        IEnumerable<ElementBlueprint> GetBlueprints();
        void DeleteBlueprint(ElementBlueprint blueprint);
        int DeleteBlueprints(IEnumerable<int> ids);
        ElementBlueprint CreateBlueprint(Element baseElement, string elementTypeName, string elementDisplayName, string elementDescription, string elementCategory);
    }
}