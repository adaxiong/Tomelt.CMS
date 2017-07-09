using Tomelt.ContentManagement;

namespace Tomelt.Layouts.Services {
    public class DescribeElementsContext {
        public IContent Content { get; set; }
        public string CacheVaryParam { get; set; }
        public bool IsHarvesting { get; set; }

        public static readonly DescribeElementsContext Empty = new DescribeElementsContext();
    }
}