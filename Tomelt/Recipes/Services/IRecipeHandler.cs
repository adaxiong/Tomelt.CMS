using Tomelt.Recipes.Models;

namespace Tomelt.Recipes.Services {
    public interface IRecipeHandler : IDependency {
        void ExecuteRecipeStep(RecipeContext recipeContext);
    }
}
