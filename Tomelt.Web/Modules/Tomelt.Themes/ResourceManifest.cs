using Tomelt.UI.Resources;

namespace Tomelt.Themes {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("ThemesAdmin").SetUrl("tomelt-themes-admin.css");
        }
    }
}
