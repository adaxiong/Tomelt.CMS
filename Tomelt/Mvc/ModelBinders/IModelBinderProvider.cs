using System.Collections.Generic;

namespace Tomelt.Mvc.ModelBinders {
    public interface IModelBinderProvider : IDependency {
        IEnumerable<ModelBinderDescriptor> GetModelBinders();
    }
}