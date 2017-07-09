using System.Collections.Generic;
using Tomelt.Localization;
using Tomelt.MediaProcessing.Descriptors;
using Tomelt.MediaProcessing.Descriptors.Filter;

namespace Tomelt.MediaProcessing.Services {
    public class ImageProcessingManager : IImageProcessingManager {
        private readonly IEnumerable<IImageFilterProvider> _filterProviders;

        public ImageProcessingManager(
            IEnumerable<IImageFilterProvider> filterProviders) {
            _filterProviders = filterProviders;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<TypeDescriptor<FilterDescriptor>> DescribeFilters() {
            var context = new DescribeFilterContext();

            foreach (var provider in _filterProviders) {
                provider.Describe(context);
            }
            return context.Describe();
        }
    }
}