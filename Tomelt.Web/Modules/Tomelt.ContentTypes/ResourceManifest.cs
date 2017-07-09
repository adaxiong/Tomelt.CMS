using Tomelt.UI.Resources;

namespace Tomelt.ContentTypes {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            builder.Add().DefineStyle("ContentTypesAdmin").SetUrl("tomelt-contenttypes-admin.css");
        }
    }
}
