using System.Collections.Generic;
using Tomelt.Modules.Models;
using Tomelt.Recipes.Models;

namespace Tomelt.Modules.ViewModels {
    public class RecipesViewModel {
        public IEnumerable<ModuleRecipesViewModel> Modules { get; set; }
    }

    public class ModuleRecipesViewModel {
        public ModuleEntry Module { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; } 
    }
}