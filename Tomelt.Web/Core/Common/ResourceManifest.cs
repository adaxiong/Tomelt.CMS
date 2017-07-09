using Tomelt.UI.Resources;

namespace Tomelt.Core.Common {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("DateTimeEditor").SetUrl("datetime-editor.css");
        }
    }
}
