using System.Xml.Linq;

namespace Tomelt.Recipes.Models {
    public class ConfigurationContext {
        protected ConfigurationContext(XElement configurationElement) {
            ConfigurationElement = configurationElement;
        }

        public XElement ConfigurationElement { get; set; }
    }
}