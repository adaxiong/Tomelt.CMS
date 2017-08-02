using Tomelt.UI.Navigation;

namespace Tomelt.Tags.ViewModels {
    public class TagsSearchViewModel: DatagridPagerParameters
    {
        public string TagName { get; set; }
        public dynamic List { get; set; }
        public dynamic Pager { get; set; }
    }

    public class TagsSearch : DatagridPagerParameters
    {
        public string TagName { get; set; }
    }
}
