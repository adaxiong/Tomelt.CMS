using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace Tomelt.Pages {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            ContentDefinitionManager.AlterTypeDefinition("Page",
                cfg => cfg
                .WithPart("CommonPart", p => p
                    .WithSetting("DateEditorSettings.ShowDateEditor", "True"))
                .WithPart("PublishLaterPart")
                .WithPart("TitlePart")
                .WithPart("AutoroutePart", builder => builder
                    .WithSetting("AutorouteSettings.AllowCustomPattern", "True")
                    .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "False")
                    .WithSetting("AutorouteSettings.PatternDefinitions", "[{\"Name\":\"Title\",\"Pattern\":\"{Content.Slug}\",\"Description\":\"my-page\"}]"))
                .WithPart("LayoutPart")
                .Creatable()
                .Listable()
                .Securable());

            return 3;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterTypeDefinition("Page", cfg => cfg.WithPart("CommonPart", p => p.WithSetting("DateEditorSettings.ShowDateEditor", "True")));
            return 2;
        }
    }
}
