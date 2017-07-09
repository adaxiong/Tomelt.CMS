using System.Collections.Generic;

namespace Tomelt.Recipes.Services
{
    public interface IRecipeExecutionStepResolver :IDependency
    {
        IRecipeExecutionStep Resolve(string importStepName);
        IEnumerable<IRecipeExecutionStep> Resolve(IEnumerable<string> exportStepNames);
    }
}