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
        public int Create()
        {

            SchemaBuilder.CreateTable("AlbumPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("ThumbPath")
                    .Column<string>("OriginalPath")
                    .Column<string>("Describe")
                    

            );
            ContentDefinitionManager.AlterPartDefinition("AlbumPart", builder => builder.Attachable().WithDescription("内容相册功能"));
            return 1;
        }
        
    }
}