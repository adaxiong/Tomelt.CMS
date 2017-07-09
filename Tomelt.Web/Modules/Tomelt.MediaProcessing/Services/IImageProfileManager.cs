using Tomelt.ContentManagement;
using Tomelt.MediaProcessing.Models;

namespace Tomelt.MediaProcessing.Services {
    public interface IImageProfileManager : IDependency {
        string GetImageProfileUrl(string path, string profileName);
        string GetImageProfileUrl(string path, string profileName, ContentItem contentItem);
        string GetImageProfileUrl(string path, string profileName, FilterRecord customFilter);
        string GetImageProfileUrl(string path, string profileName, FilterRecord customFilter, ContentItem contentItem);
        string GetImageProfileUrl(string path, string profileName, ContentItem contentItem, params FilterRecord[] customFilters);
    }
}
