using Tomelt.Environment.Extensions;
using Tomelt.Packaging.Models;

namespace Tomelt.Packaging.Services {
    public interface IBackgroundPackageUpdateStatus : ISingletonDependency {
        PackagesStatusResult Value { get; set; }
    }

    [TomeltFeature("Gallery.Updates")]
    public class BackgroundPackageUpdateStatus : IBackgroundPackageUpdateStatus {
        public PackagesStatusResult Value { get; set; }
    }
}