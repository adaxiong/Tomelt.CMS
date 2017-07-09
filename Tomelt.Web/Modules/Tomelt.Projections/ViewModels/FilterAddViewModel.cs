using System.Collections.Generic;
using Tomelt.Projections.Descriptors;
using Tomelt.Projections.Descriptors.Filter;

namespace Tomelt.Projections.ViewModels {
    public class FilterAddViewModel {
        public int Id { get; set; }
        public int Group { get; set; }

        public IEnumerable<TypeDescriptor<FilterDescriptor>> Filters { get; set; }
    }
}
