using System;
using System.Collections.Generic;
using System.Data;
using Tomelt.ContentManagement.Drivers;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace ArticleManage {
    public class Migrations : DataMigrationImpl {

        public int Create()
        {
            // Creating table Tomelt_ArticleManage_ArticlePartRecord
            SchemaBuilder.CreateTable("ArticlePartRecord", table => table
                .ContentPartRecord()
                .Column("Subtitle", DbType.String)
                .Column("Summary", DbType.String)
                .Column("LinkUrl", DbType.String)
                .Column("ColumnPartRecordId", DbType.Int32)
                .Column("Sort", DbType.Int32)
                .Column("IsSlide", DbType.Boolean)
                .Column("IsTop", DbType.Boolean)
                .Column("IsHot", DbType.Boolean)
                .Column("IsRecommend", DbType.Boolean)
                .Column("IsStriking", DbType.Boolean)
                .Column("Source", DbType.String)
                .Column("Author", DbType.String)
                .Column("ClickNum", DbType.Int32)
                .Column("ColumnPartRecord_id", DbType.Int32)
            );

            // Creating table Tomelt_ArticleManage_ColumnPartRecord
            SchemaBuilder.CreateTable("ColumnPartRecord", table => table
                .ContentPartRecord()
                .Column("ParentId", DbType.Int32)
                .Column("Sort", DbType.Int32)
                .Column("Summary", DbType.String)
                .Column("CallIndex", DbType.String)
                .Column("LinkUrl", DbType.String)
                .Column("ImageUrl", DbType.String)
                .Column("TreePath", DbType.String)
                .Column("Layer", DbType.Int32)
                .Column("Groups", DbType.String)
            );

            ContentDefinitionManager.AlterPartDefinition("ArticlePart", part => part
                .WithDescription("文章内容元件").Attachable());
            ContentDefinitionManager.AlterPartDefinition("ColumnPart", part => part
                .WithDescription("文章栏目元件"));

            ContentDefinitionManager.AlterTypeDefinition("Article", cfg => cfg
                .WithPart("CommonPart", p => p.WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                .WithPart("BodyPart").WithPart("TitlePart").WithPart("ArticlePart").DisplayedAs("文章内容").Creatable().Draftable()
            );
            ContentDefinitionManager.AlterTypeDefinition("Column", cfg => cfg
                .WithPart("CommonPart", p => p.WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
                .WithPart("BodyPart").WithPart("TitlePart").WithPart("ColumnPart").DisplayedAs("文章栏目").Creatable().Draftable()
            );
            return 1;
        }
        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition("ArticlePart", part => part
                .WithDescription("文章内容元件").Attachable());
            ContentDefinitionManager.AlterPartDefinition("ColumnPart", part => part
                .WithDescription("文章栏目元件").Attachable());

            return 2;
        }
    }
}