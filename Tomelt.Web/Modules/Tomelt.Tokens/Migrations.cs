using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Extensions;
using Tomelt.Data.Migration;
using Tomelt.Environment.Extensions;

namespace Tomelt.Tokens {
    [TomeltFeature("Tomelt.Tokens.Feeds")]
    public class FeedsMigrations : DataMigrationImpl {

        public int Create() {

            ContentDefinitionManager.AlterPartDefinition("RssPart",
                cfg => cfg
                    .Attachable()
                    .WithDescription("Attach to a content type to provide custom values in RSS feeds.")
                );

            return 1;
        }
    }
}