using System.Collections.Generic;
using Tomelt.Projections.Descriptors;
using Tomelt.Projections.Descriptors.SortCriterion;

namespace Tomelt.Projections.ViewModels {
    public class SortCriterionAddViewModel {
        public int Id { get; set; }
        public IEnumerable<TypeDescriptor<SortCriterionDescriptor>> SortCriteria { get; set; }
    }
}
