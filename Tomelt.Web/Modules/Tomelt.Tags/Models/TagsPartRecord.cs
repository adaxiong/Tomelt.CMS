using System.Collections.Generic;
using Tomelt.ContentManagement.Records;

namespace Tomelt.Tags.Models {
    public class TagsPartRecord : ContentPartRecord {
        public TagsPartRecord() {
            Tags = new List<ContentTagRecord>();
        }

        public virtual IList<ContentTagRecord> Tags { get; set; }
    }
}