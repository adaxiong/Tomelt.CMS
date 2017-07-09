using Tomelt.UI.Resources;

namespace Tomelt.Autoroute {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineStyle("AutorouteSettings").SetUrl("Tomelt-autoroute-settings.css");
            manifest.DefineScript("AutorouteBrowser").SetUrl("autoroute-browser.js").SetDependencies("jQuery");
        }
    }
}
