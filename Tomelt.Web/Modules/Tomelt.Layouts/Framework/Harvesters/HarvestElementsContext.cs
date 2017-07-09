using Tomelt.ContentManagement;

namespace Tomelt.Layouts.Framework.Harvesters {
    public class HarvestElementsContext {
        public IContent Content { get; set; }
        public bool IsHarvesting { get; set; }
    }
}