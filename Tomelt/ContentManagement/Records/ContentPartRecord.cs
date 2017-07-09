using Tomelt.Data.Conventions;

namespace Tomelt.ContentManagement.Records {
    public abstract class ContentPartRecord {
        public virtual int Id { get; set; }
        [CascadeAllDeleteOrphan]
        public virtual ContentItemRecord ContentItemRecord { get; set; }
    }
}
