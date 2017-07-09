using Tomelt.Recipes.Models;

namespace Tomelt.Recipes.Services {
    public interface IRecipeStepQueue : ISingletonDependency {
        void Enqueue(string executionId, RecipeStep step);
        RecipeStep Dequeue(string executionId);
    }
}
