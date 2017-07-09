using Tomelt.MediaProcessing.Descriptors.Filter;

namespace Tomelt.MediaProcessing.Services {
    public interface IImageFilterProvider : IDependency {
        void Describe(DescribeFilterContext describe);
    }
}