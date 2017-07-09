using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace Tomelt.Templates {
    public class Migrations : DataMigrationImpl {
        public int Create() {

            ContentDefinitionManager.AlterPartDefinition("ShapePart", part => part
                .Attachable()
                .WithDescription("Turns a type into a shape provider."));

            ContentDefinitionManager.AlterTypeDefinition("Template", type => type
                .WithPart("CommonPart")
                .WithIdentity()
                .WithPart("TitlePart")
                .WithPart("ShapePart")
                .Draftable());
            return 1;
        }
    }
}