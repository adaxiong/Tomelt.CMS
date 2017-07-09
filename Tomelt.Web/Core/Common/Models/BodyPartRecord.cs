using Tomelt.ContentManagement.Records;
using Tomelt.Data.Conventions;

namespace Tomelt.Core.Common.Models {
    public class BodyPartRecord : ContentPartVersionRecord {
        [StringLengthMax]
        public virtual string Text { get; set; }

        public virtual string Format { get; set; }
    }
}