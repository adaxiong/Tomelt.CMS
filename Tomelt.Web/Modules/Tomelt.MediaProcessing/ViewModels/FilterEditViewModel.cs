using Tomelt.MediaProcessing.Descriptors.Filter;

namespace Tomelt.MediaProcessing.ViewModels {
    public class FilterEditViewModel {
        public int Id { get; set; }
        public string Description { get; set; }
        public FilterDescriptor Filter { get; set; }
        public dynamic Form { get; set; }
    }
}
