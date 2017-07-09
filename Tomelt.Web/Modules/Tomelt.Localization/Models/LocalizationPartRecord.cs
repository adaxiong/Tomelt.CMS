using Tomelt.ContentManagement.Records;

namespace Tomelt.Localization.Models {
    public class LocalizationPartRecord : ContentPartRecord {
        public virtual int CultureId { get; set; }
        public virtual int MasterContentItemId { get; set; }
    }
}