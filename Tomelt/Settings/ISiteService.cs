namespace Tomelt.Settings {
    public interface ISiteService : IDependency {
        ISite GetSiteSettings();
    }
}
