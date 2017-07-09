using System.Collections.Generic;
using Tomelt.MediaProcessing.Descriptors;
using Tomelt.MediaProcessing.Descriptors.Filter;

namespace Tomelt.MediaProcessing.Services {
    public interface IImageProcessingManager : IDependency {
        IEnumerable<TypeDescriptor<FilterDescriptor>> DescribeFilters();
    }
}