using System.Collections.Generic;

namespace Tomelt.Mvc.ModelBinders {
    public interface IModelBinderPublisher : IDependency {
        void Publish(IEnumerable<ModelBinderDescriptor> binders);
    }
}