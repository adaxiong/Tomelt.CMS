using Tomelt.ContentManagement;

namespace Tomelt.Layouts.Services {
    public interface IContentPartDisplay : IDependency {
        dynamic BuildDisplay(ContentPart part, string displayType = "", string groupId = "");
        dynamic BuildEditor(ContentPart part, string groupId = "");
        dynamic UpdateEditor(ContentPart part, IUpdateModel updater, string groupId = "");
    }
}