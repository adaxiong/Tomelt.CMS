using System.Collections.Generic;
using Tomelt.MediaProcessing.Descriptors;
using Tomelt.MediaProcessing.Descriptors.Filter;

namespace Tomelt.MediaProcessing.ViewModels {
    public class FilterAddViewModel {
        public int Id { get; set; }
        public IEnumerable<TypeDescriptor<FilterDescriptor>> Filters { get; set; }
    }
}
