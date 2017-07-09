using System.Xml.Linq;
using Tomelt.Recipes.Models;

namespace Tomelt.Recipes.Services {
    public interface IRecipeExecutor : IDependency {
        string Execute(Recipe recipe);
    }
}