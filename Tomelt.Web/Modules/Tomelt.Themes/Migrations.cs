using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;

namespace Tomelt.Themes {
    public class ThemesDataMigration : DataMigrationImpl {

        public int Create() {
            return 1;
        }

        public int UpdateFrom1() {

            ContentDefinitionManager.AlterPartDefinition("DisableThemePart", builder => builder
                .Attachable()
                .WithDescription("When attached to a content type, disables the theme when a content item of this type is displayed."));

            return 2;
        }
    }
}