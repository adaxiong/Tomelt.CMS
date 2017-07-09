using Tomelt.Recipes.Models;

namespace Tomelt.Recipes.Services {
    public interface IRecipeManager : IDependency {
        string Execute(Recipe recipe);
    }
}
