using System.Collections.Generic;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Elements {
    public abstract class Container : Element {
        protected Container() {
            Elements = new List<Element>();
        }
        public IList<Element> Elements { get; set; }
    }
}