using Tomelt.Security;

namespace Tomelt.MediaLibrary.Providers {
    public interface IMediaFolderProvider : IDependency {
        string GetFolderName(IUser content);
    }
}