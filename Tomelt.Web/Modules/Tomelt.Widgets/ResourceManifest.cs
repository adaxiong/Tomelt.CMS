using Tomelt.UI.Resources;

namespace Tomelt.Widgets {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            builder.Add().DefineStyle("WidgetsAdmin").SetUrl("tomelt-widgets-admin.css").SetDependencies("~/Themes/TheAdmin/Styles/Site.css");
        }
    }
}
