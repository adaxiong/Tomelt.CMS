using Tomelt.Data.Conventions;

namespace Tomelt.Tags.Models {
    public class ContentTagRecord {
        public virtual int Id { get; set; }

        [Aggregate]
        public virtual TagRecord TagRecord { get; set; }
        
        public virtual TagsPartRecord TagsPartRecord { get; set; }
    }
}