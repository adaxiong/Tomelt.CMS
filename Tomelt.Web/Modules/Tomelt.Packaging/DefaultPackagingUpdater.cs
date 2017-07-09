using Tomelt.Environment;
using Tomelt.Environment.Extensions;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Localization;
using Tomelt.Packaging.Services;

namespace Tomelt.Packaging {
    [TomeltFeature("Gallery")]
    public class DefaultPackagingUpdater : IFeatureEventHandler {
        private readonly IPackagingSourceManager _packagingSourceManager;

        public DefaultPackagingUpdater(IPackagingSourceManager packagingSourceManager) {
            _packagingSourceManager = packagingSourceManager;
        }

        public Localizer T { get; set; }

        public void Installing(Feature feature) {
        }

        public void Installed(Feature feature) {
            if (feature.Descriptor.Id == "Gallery") {
                _packagingSourceManager.AddSource("Tomelt Gallery", "https://www.tomelt.com");
            }
        }

        public void Enabling(Feature feature) {
        }

        public void Enabled(Feature feature) {
        }

        public void Disabling(Feature feature) {
        }

        public void Disabled(Feature feature) {
        }

        public void Uninstalling(Feature feature) {
        }

        public void Uninstalled(Feature feature) {
        }
    }
}
