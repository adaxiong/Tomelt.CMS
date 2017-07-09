using Tomelt.ContentManagement;

namespace Tomelt.Core.Common.Services {
    public interface ICommonService : IDependency {
        void Publish(ContentItem contentItem);
    }
}