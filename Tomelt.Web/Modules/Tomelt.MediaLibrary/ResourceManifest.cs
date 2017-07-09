using Tomelt.UI.Resources;

namespace Tomelt.MediaLibrary {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("MediaManagerAdmin").SetUrl("Tomelt-medialibrary-admin.css").SetDependencies("~/Themes/TheAdmin/Styles/Site.css");
        }
    }
}