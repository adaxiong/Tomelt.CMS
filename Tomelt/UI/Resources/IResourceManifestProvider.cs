namespace Tomelt.UI.Resources {
    public interface IResourceManifestProvider : ISingletonDependency {
        void BuildManifests(ResourceManifestBuilder builder);
    }
}
