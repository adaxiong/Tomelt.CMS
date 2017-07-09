using System.Collections.Generic;
using Tomelt.Packaging.Models;

namespace Tomelt.Packaging.ViewModels {
    public class PackagingSourcesViewModel {
        public IEnumerable<PackagingSource> Sources { get; set; }
    }
}