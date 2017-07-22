using Tomelt.UI.Resources;

namespace UEditor {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();
            manifest.DefineScript("UEditorConfig").SetUrl("ueditor.config.js");
            manifest.DefineScript("UEditor").SetUrl("ueditor.all.min.js").SetVersion("1.4.3").SetDependencies("UEditorConfig");
            
        }
    }
}
