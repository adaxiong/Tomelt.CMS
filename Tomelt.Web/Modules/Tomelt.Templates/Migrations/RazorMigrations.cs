using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.Data.Migration;
using Tomelt.ContentManagement.MetaData;
using Tomelt.Environment.Extensions;

namespace Tomelt.Templates {
    [TomeltFeature("Tomelt.Templates.Razor")]
    public class RazorMigrations : DataMigrationImpl {
        public int Create() {
            ContentDefinitionManager.AlterTypeDefinition("Template", type => type
                .WithPart("ShapePart", p => p
                    .WithSetting("ShapePartSettings.Processor", "Razor")));
            return 1;
        }
    }
}