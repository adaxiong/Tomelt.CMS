using Tomelt.Environment.Extensions;
using Tomelt.UI.Resources;

namespace Tomelt.Packaging {
    [TomeltFeature("Gallery")]
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            UI.Resources.ResourceManifest resourceManifest = builder.Add();
            resourceManifest.DefineStyle("PackagingAdmin").SetUrl("tomelt-packaging-admin.css");
        }
    }
}
