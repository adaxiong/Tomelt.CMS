namespace Tomelt.Autoroute.Services {
    public interface ISlugService : IDependency {
        string Slugify(ContentManagement.IContent content);
        string Slugify(string text);
    }
}
