using System.Collections.Generic;
using Tomelt.ContentManagement.Handlers;
using Tomelt.ContentManagement.MetaData;

namespace Tomelt.ContentManagement.Drivers {
    public interface IContentPartDriver : IDependency {
        DriverResult BuildDisplay(BuildDisplayContext context);
        DriverResult BuildEditor(BuildEditorContext context);
        DriverResult UpdateEditor(UpdateEditorContext context);
        void Importing(ImportContentContext context);
        void Imported(ImportContentContext context);
        void ImportCompleted(ImportContentContext context);
        void Exporting(ExportContentContext context);
        void Exported(ExportContentContext context);
        IEnumerable<ContentPartInfo> GetPartInfo();
        void GetContentItemMetadata(GetContentItemMetadataContext context);
    }
}