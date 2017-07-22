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
                .WithDescription("当附件一个内容类型时，内容项的类型为显示状态时，使其禁止主题"));

            return 2;
        }
    }
}