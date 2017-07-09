using System.Collections.Generic;
using Tomelt.ContentManagement;

namespace Tomelt.Layouts.Framework.Elements {
    public class LayoutSavingContext {
        public IUpdateModel Updater { get; set; }

        public IEnumerable<Element> Elements { get; set; }
        public IEnumerable<Element> RemovedElements { get; set; }
        public IContent Content { get; set; }
    }
}