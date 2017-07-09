using Tomelt.Recipes.Models;

namespace Tomelt.Recipes.Services {
    /// <summary>
    /// Provides information about the result of recipe execution.
    /// </summary>
    public interface IRecipeResultAccessor : IDependency {
        RecipeResult GetResult(string executionId);
    }
}
