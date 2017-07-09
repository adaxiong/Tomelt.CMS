using System;
using System.Globalization;
using Tomelt.ContentManagement.MetaData;
using Tomelt.Data.Migration;

namespace Tomelt.ContentTypes {
    public class Migrations : DataMigrationImpl {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager) {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create() {
            foreach (var typeDefinition in _contentDefinitionManager.ListTypeDefinitions()) {
                if (typeDefinition.Settings.ContainsKey("ContentTypeSettings.Creatable") && Convert.ToBoolean(typeDefinition.Settings["ContentTypeSettings.Creatable"], CultureInfo.InvariantCulture)) {
                    typeDefinition.Settings["ContentTypeSettings.Listable"] = "True";
                    _contentDefinitionManager.StoreTypeDefinition(typeDefinition);
                }
            }

            return 1;
        }

        public int UpdateFrom1() {
            foreach (var typeDefinition in _contentDefinitionManager.ListTypeDefinitions()) {
                if (typeDefinition.Settings.ContainsKey("ContentTypeSettings.Creatable") && Convert.ToBoolean(typeDefinition.Settings["ContentTypeSettings.Creatable"], CultureInfo.InvariantCulture)) {
                    typeDefinition.Settings["ContentTypeSettings.Securable"] = "True";
                    _contentDefinitionManager.StoreTypeDefinition(typeDefinition);
                }
            }

            return 2;
        }
    }
}