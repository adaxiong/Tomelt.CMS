using System.Collections.Generic;
using System.Xml.Linq;

namespace Tomelt.Recipes.Services {
    public interface IRecipeBuilder : IDependency {
        XDocument Build(IEnumerable<IRecipeBuilderStep> steps);
    }
}