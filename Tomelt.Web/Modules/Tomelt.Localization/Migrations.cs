using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;
using Tomelt.Environment.Extensions;

namespace Tomelt.Localization {
    public class Migrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("LocalizationPartRecord", 
                table => table
                    .ContentPartRecord()
                    .Column<int>("CultureId")
                    .Column<int>("MasterContentItemId")
                );

            ContentDefinitionManager.AlterPartDefinition("LocalizationPart", builder => builder.Attachable());

            return 1;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("LocalizationPart", builder => builder
                .WithDescription("Provides the user interface to localize content items."));

            return 2;
        }
    }

    [TomeltFeature("Tomelt.Localization.Transliteration")]
    public class TransliterationMigrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("TransliterationSpecificationRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("CultureFrom")
                    .Column<string>("CultureTo")
                    .Column<string>("Rules", c => c.Unlimited())
                );

            return 1;
        }
    }
}