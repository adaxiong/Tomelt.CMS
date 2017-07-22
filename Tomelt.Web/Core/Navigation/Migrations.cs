using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace Tomelt.Core.Navigation {
    public class Migrations : DataMigrationImpl {

        public int Create() {
            ContentDefinitionManager.AlterPartDefinition("MenuPart", builder => builder
                .Attachable()
                .WithDescription("提供一个简单创建菜单内容项的编辑器."));

            SchemaBuilder.CreateTable("MenuPartRecord", 
                table => table
                    .ContentPartRecord()
                    .Column<string>("MenuText")
                    .Column<string>("MenuPosition")
                    .Column<int>("MenuId")
                );

            ContentDefinitionManager.AlterTypeDefinition("MenuItem", cfg => cfg
                .WithPart("MenuPart")
                .WithIdentity()
                .WithPart("CommonPart")
                .DisplayedAs("Custom Link")
                .WithSetting("Description", "Represents a simple custom link with a text and an url.")
                .WithSetting("Stereotype", "MenuItem") // because we declare a new stereotype, the Shape MenuItem_Edit is needed
                );

            ContentDefinitionManager.AlterTypeDefinition("Menu", cfg => cfg
                .WithPart("CommonPart", p => p.WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                .WithPart("TitlePart")
                );

            ContentDefinitionManager.AlterTypeDefinition("MenuWidget", cfg => cfg
                .WithPart("CommonPart")
                .WithIdentity()
                .WithPart("WidgetPart")
                .WithPart("MenuWidgetPart")
                .WithSetting("Stereotype", "Widget")
                );

            SchemaBuilder.CreateTable("AdminMenuPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("AdminMenuText")
                    .Column<string>("AdminMenuPosition")
                    .Column<bool>("OnAdminMenu")
                );

            ContentDefinitionManager.AlterTypeDefinition("HtmlMenuItem", cfg => cfg
                .WithPart("MenuPart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                .WithIdentity()
                .DisplayedAs("Html Menu Item")
                .WithSetting("Description", "Renders some custom HTML in the menu.")
                .WithSetting("BodyPartSettings.FlavorDefault", "html")
                .WithSetting("Stereotype", "MenuItem")
                );
            
            ContentDefinitionManager.AlterPartDefinition("AdminMenuPart", builder => builder
                .Attachable()
                .WithDescription("将菜单项添加到链接到该内容项的管理菜单中"));
            
            ContentDefinitionManager.AlterTypeDefinition("ShapeMenuItem",
                cfg => cfg
                    .WithPart("ShapeMenuItemPart")
                    .WithPart("MenuPart")
                    .WithPart("CommonPart")
                    .DisplayedAs("Shape Link")
                    .WithSetting("Description", "Injects menu items from a Shape")
                    .WithSetting("Stereotype", "MenuItem")
                );

            return 4;
        }

        public int UpdateFrom1() {
            SchemaBuilder.CreateTable("AdminMenuPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("AdminMenuText")
                    .Column<string>("AdminMenuPosition")
                    .Column<bool>("OnAdminMenu")
                );
            ContentDefinitionManager.AlterPartDefinition("AdminMenuPart", builder => builder.Attachable());
            return 2;
        }

        public int UpdateFrom2() {
            ContentDefinitionManager.AlterTypeDefinition("MenuItem", cfg => cfg
                .WithPart("MenuPart")
                .WithPart("CommonPart")
                .WithIdentity()
                .DisplayedAs("Custom Link")
                .WithSetting("Description", "Represents a simple custom link with a text and an url.")
                .WithSetting("Stereotype", "MenuItem") // because we declare a new stereotype, the Shape MenuItem_Edit is needed
                );

            ContentDefinitionManager.AlterTypeDefinition("Menu", cfg => cfg
                .WithPart("CommonPart")
                .WithPart("TitlePart")
                );

            SchemaBuilder.CreateTable("MenuWidgetPartRecord",table => table
                .ContentPartRecord()
                .Column<int>("StartLevel")
                .Column<int>("Levels")
                .Column<bool>("Breadcrumb")
                .Column<bool>("AddHomePage")
                .Column<bool>("AddCurrentPage")
                .Column<int>("Menu_id")
                );

            ContentDefinitionManager.AlterTypeDefinition("MenuWidget", cfg => cfg
                .WithPart("CommonPart")
                .WithIdentity()
                .WithPart("WidgetPart")
                .WithPart("MenuWidgetPart")
                .WithSetting("Stereotype", "Widget")
                );

            SchemaBuilder
                .AlterTable("MenuPartRecord", table => table.DropColumn("OnMainMenu"))
                .AlterTable("MenuPartRecord", table => table.AddColumn<int>("MenuId"))
                ;

            ContentDefinitionManager.AlterTypeDefinition("HtmlMenuItem", cfg => cfg
                .WithPart("MenuPart")
                .WithPart("BodyPart")
                .WithPart("CommonPart")
                .WithIdentity()
                .DisplayedAs("Html Menu Item")
                .WithSetting("Description", "Renders some custom HTML in the menu.")
                .WithSetting("BodyPartSettings.FlavorDefault", "html")
                .WithSetting("Stereotype", "MenuItem")
               );

            return 3;
        }

        public int UpdateFrom3() {
            SchemaBuilder.CreateTable("ShapeMenuItemPartRecord",
                table => table.ContentPartRecord()
                    .Column<string>("ShapeType")
                );

            ContentDefinitionManager.AlterTypeDefinition("ShapeMenuItem",
                cfg => cfg
                    .WithPart("ShapeMenuItemPart")
                    .WithPart("MenuPart")
                    .WithPart("CommonPart")
                    .DisplayedAs("Shape Link")
                    .WithSetting("Description", "Injects menu items from a Shape")
                    .WithSetting("Stereotype", "MenuItem")
                );

            return 4;
        }

        public int UpdateFrom4() {
            ContentDefinitionManager.AlterPartDefinition("MenuPart", builder => builder
                .WithDescription("提供一个简单创建菜单内容项的编辑器"));

            ContentDefinitionManager.AlterPartDefinition("AdminMenuPart", builder => builder
                .Attachable()
                .WithDescription("将菜单项添加到链接到该内容项的管理菜单中"));

            return 5;
        }

        public int UpdateFrom5() {
            ContentDefinitionManager.AlterTypeDefinition("Menu", cfg => cfg
                .WithIdentity()
            );

            return 6;
        }
    }
}