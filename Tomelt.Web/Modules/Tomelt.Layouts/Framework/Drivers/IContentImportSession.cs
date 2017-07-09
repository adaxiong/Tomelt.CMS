using Tomelt.ContentManagement;

namespace Tomelt.Layouts.Framework.Drivers {
    public interface IContentImportSession {
        ContentItem GetItemFromSession(string id);
    }
}