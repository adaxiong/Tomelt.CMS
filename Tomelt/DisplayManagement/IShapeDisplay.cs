using System.Collections.Generic;
using Tomelt.DisplayManagement.Shapes;

namespace Tomelt.DisplayManagement {
    public interface IShapeDisplay : IDependency {
        string Display(Shape shape);
        string Display(object shape);
        IEnumerable<string> Display(IEnumerable<object> shapes);
    }
}
