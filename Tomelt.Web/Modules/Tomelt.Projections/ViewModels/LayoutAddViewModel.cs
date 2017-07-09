using System.Collections.Generic;
using Tomelt.Projections.Descriptors;
using Tomelt.Projections.Descriptors.Layout;

namespace Tomelt.Projections.ViewModels {
    public class LayoutAddViewModel {
        public int Id { get; set; }
        public IEnumerable<TypeDescriptor<LayoutDescriptor>> Layouts { get; set; }
    }
}
