using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace Tomelt.ContentPicker {
    public class Migrations : DataMigrationImpl {

        public int Create() {

            SchemaBuilder.CreateTable("ContentMenuItemPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<int>("ContentMenuItemRecord_id")
                );

            ContentDefinitionManager.AlterTypeDefinition("ContentMenuItem", cfg => cfg
                .WithPart("MenuPart")
                .WithPart("CommonPart")
                .WithIdentity()
                .WithPart("ContentMenuItemPart")
                .DisplayedAs("Content Menu Item")
                .WithSetting("Description", "Adds a Content Item to the menu.")
                .WithSetting("Stereotype", "MenuItem")
                );

            ContentDefinitionManager.AlterPartDefinition("NavigationPart", builder => builder
                .Attachable()
                .WithDescription("Allows the management of Content Menu Items associated with a Content Item."));
            
            return 1;
        }
    }
}