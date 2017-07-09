using System.Collections.Generic;
using Tomelt.Projections.Descriptors;
using Tomelt.Projections.Descriptors.Property;

namespace Tomelt.Projections.ViewModels {
    public class PropertyAddViewModel {
        public int Id { get; set; }
        public IEnumerable<TypeDescriptor<PropertyDescriptor>> Properties { get; set; }
    }
}
