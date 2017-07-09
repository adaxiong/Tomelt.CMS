using System;
using System.Collections.Generic;
using Tomelt.Environment.Configuration;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Modules.Models;

namespace Tomelt.Modules.ViewModels {
    public class FeaturesViewModel {
        public IEnumerable<ModuleFeature> Features { get; set; }
        public FeaturesBulkAction BulkAction { get; set; }
        public Func<ExtensionDescriptor, bool> IsAllowed { get; set; }
    }

    public enum FeaturesBulkAction {
        None,
        Enable,
        Disable,
        Update,
        Toggle
    }
}