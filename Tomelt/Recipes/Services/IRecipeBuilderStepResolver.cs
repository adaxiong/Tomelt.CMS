using System.Collections.Generic;

namespace Tomelt.Recipes.Services
{
    public interface IRecipeBuilderStepResolver : IDependency
    {
        IRecipeBuilderStep Resolve(string exportStepName);
        IEnumerable<IRecipeBuilderStep> Resolve(IEnumerable<string> exportStepNames);
    }
}