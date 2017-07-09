using System.Xml.Linq;

namespace Tomelt.Recipes.Models {
    public class RecipeBuilderStepConfigurationContext : ConfigurationContext {
        public RecipeBuilderStepConfigurationContext(XElement configurationElement) : base(configurationElement) {
        }
    }
}