using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace UEditor
{
    public class Migrations: DataMigrationImpl
    {
        //public int Create()
        //{
        //    SchemaBuilder.CreateTable("AlbumPartRecord",
        //        table => table
        //            .ContentPartRecord()
        //    );
        //    SchemaBuilder.CreateTable("ContentAlbumRecord",
        //        table => table
        //            .Column<int>("Id", column => column.PrimaryKey().Identity())
        //            .Column<string>("ThumbPath")
        //            .Column<string>("OriginalPath")
        //            .Column<string>("Describe")
        //            .Column<int>("AlbumPartRecord_Id")
                    
        //    );
        //    ContentDefinitionManager.AlterPartDefinition("AlbumPart", builder => builder.Attachable().WithDescription("内容相册功能"));
        //    return 1;
        //}
        //public int UpdateFrom1()
        //{
        //    SchemaBuilder.AlterTable("ContentAlbumRecord", table => table
        //        .CreateIndex("IDX_AlbumPartRecord_Id", "AlbumPartRecord_Id")
        //    );
        //    return 2;
        //}
    }
}