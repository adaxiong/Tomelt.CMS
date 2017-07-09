using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Records;

namespace Tomelt.Core.Containers.Models {
    public class ContainablePart : ContentPart<ContainablePartRecord> {
        public int Position {
            get { return Record.Position; }
            set { Record.Position = value; }
        }
    }

    public class ContainablePartRecord : ContentPartRecord {
        public virtual int Position { get; set; }
    }
}