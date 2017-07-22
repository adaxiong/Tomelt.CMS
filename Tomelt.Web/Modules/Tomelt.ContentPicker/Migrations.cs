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
                .WithDescription("允许管理与内容项相关联的内容菜单项。"));
            
            return 1;
        }
    }
}