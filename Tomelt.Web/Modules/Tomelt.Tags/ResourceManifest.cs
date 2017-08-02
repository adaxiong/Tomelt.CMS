using Tomelt.UI.Resources;

namespace Tomelt.Tags {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            manifest.DefineScript("TagsAutocomplete").SetUrl("tomelt-tags-autocomplete.js").SetDependencies("jQueryUI");

            manifest.DefineStyle("TagsAdmin").SetUrl("tomelt-tags-admin.css");
        }
    }
}