using Tomelt.Layouts.Framework.Elements;
using Tomelt.Layouts.Helpers;

namespace Tomelt.Layouts.Elements {
    public class Image : Element {
        public override string Category {
            get { return "Media"; }
        }

        public override string ToolboxIcon {
            get { return "\uf1c5"; }
        }

        public int? MediaId {
            get { return this.Retrieve(x => x.MediaId); }
            set { this.Store(x => x.MediaId, value); }
        }
    }
}