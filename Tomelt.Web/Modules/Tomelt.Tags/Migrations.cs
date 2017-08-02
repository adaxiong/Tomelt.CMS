using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;
using Tomelt.Environment.Extensions;

namespace Tomelt.Tags {
    public class TagsDataMigration : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("TagsPartRecord",
                table => table
                    .ContentPartRecord()
                );

            SchemaBuilder.CreateTable("TagRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("TagName")
                );

            SchemaBuilder.CreateTable("ContentTagRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("TagRecord_Id")
                    .Column<int>("TagsPartRecord_Id")
                );

            ContentDefinitionManager.AlterPartDefinition("TagsPart", builder => builder.Attachable());

            return 1;
        }

        public int UpdateFrom1() {
            ContentDefinitionManager.AlterPartDefinition("TagsPart", builder => builder
                .WithDescription("为您的内容贴上标签功能."));

            return 2;
        }

        public int UpdateFrom2() {
            SchemaBuilder.AlterTable("ContentTagRecord", table => table
                .CreateIndex("IDX_TagsPartRecord_Id", "TagsPartRecord_Id")
            );
            return 3;
        }
    }

    [TomeltFeature("Tomelt.Tags.TagCloud")]
    public class TagCloudMigrations : DataMigrationImpl {

        public int Create() {

            ContentDefinitionManager.AlterTypeDefinition(
                "TagCloud",
                cfg => cfg
                           .WithPart("TagCloudPart")
                           .AsWidget()
                );

            return 1;
        }
    }
}