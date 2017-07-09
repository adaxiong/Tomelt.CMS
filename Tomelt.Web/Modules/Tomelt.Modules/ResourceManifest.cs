using Tomelt.UI.Resources;

namespace Tomelt.Modules {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            builder.Add().DefineStyle("ModulesAdmin").SetUrl("tomelt-modules-admin.css");
        }
    }
}
