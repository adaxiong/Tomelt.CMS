using Tomelt.ContentManagement;

namespace Tomelt.Core.Common.Models {
    public class BodyPart : ContentPart<BodyPartRecord> {
        public string Text {
            get { return Retrieve(x => x.Text); }
            set { Store(x => x.Text, value); }
        }

        public string Format {
            get { return Retrieve(x => x.Format); }
            set { Store(x => x.Format, value); }
        }
    }
}
