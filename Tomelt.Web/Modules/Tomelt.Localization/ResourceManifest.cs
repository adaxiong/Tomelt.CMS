using Tomelt.UI.Resources;

namespace Tomelt.Localization {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("Localization").SetUrl("tomelt-localization-base.css");
            manifest.DefineStyle("LocalizationAdmin").SetUrl("tomelt-localization-admin.css");
        }
    }
}
