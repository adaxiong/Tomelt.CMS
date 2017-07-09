using System.Xml.Linq;

namespace Tomelt.Recipes.Models {
    public class RecipeExecutionStepConfigurationContext : ConfigurationContext {
        public RecipeExecutionStepConfigurationContext(XElement configurationElement) : base(configurationElement) {
        }
    }
}