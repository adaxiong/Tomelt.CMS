using System.Collections.Generic;
using Tomelt.DisplayManagement.Descriptors;
using Tomelt.DisplayManagement.Shapes;

namespace Tomelt.DisplayManagement {
    public interface IShapeBindingResolver : IDependency {
        bool TryGetDescriptorBinding(string shapeType, out ShapeBinding shapeBinding);
    }
}
