using System.Collections.Generic;
using Tomelt.Environment.Configuration;
using Tomelt.Recipes.Models;

namespace Tomelt.Setup.Services {
    public interface ISetupService : IDependency {
        ShellSettings Prime();
        IEnumerable<Recipe> Recipes();
        string Setup(SetupContext context);
    }
}