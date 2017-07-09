using Tomelt.Layouts.Framework.Elements;
using Tomelt.Layouts.Helpers;

namespace Tomelt.Layouts.Elements {
    public abstract class ContentElement : Element {
        public override string Category {
            get { return "Content"; }
        }

        public virtual string Content {
            get { return this.Retrieve(x => x.Content); }
            set { this.Store(x => x.Content, value); }
        }
    }
}