using System.Collections.Generic;
using Tomelt.ContentManagement.Handlers;
using Tomelt.ContentManagement.MetaData;

namespace Tomelt.ContentManagement.Drivers {
    public interface IContentFieldDriver : IDependency {
        DriverResult BuildDisplayShape(BuildDisplayContext context);
        DriverResult BuildEditorShape(BuildEditorContext context);
        DriverResult UpdateEditorShape(UpdateEditorContext context);
        void Importing(ImportContentContext context);
        void Imported(ImportContentContext context);
        void ImportCompleted(ImportContentContext context);
        void Exporting(ExportContentContext context);
        void Exported(ExportContentContext context);
        void Describe(DescribeMembersContext context);
        IEnumerable<ContentFieldInfo> GetFieldInfo();
        void GetContentItemMetadata(GetContentItemMetadataContext context);
    }
}