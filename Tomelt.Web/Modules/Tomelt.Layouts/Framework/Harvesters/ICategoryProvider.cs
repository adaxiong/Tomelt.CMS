using System.Collections.Generic;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Framework.Harvesters {
    public interface ICategoryProvider : IDependency {
        IEnumerable<Category> GetCategories();
    }
}