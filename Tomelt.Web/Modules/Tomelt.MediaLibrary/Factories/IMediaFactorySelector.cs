using System.IO;

namespace Tomelt.MediaLibrary.Factories {
    public interface IMediaFactorySelector : IDependency {
        MediaFactorySelectorResult GetMediaFactory(Stream stream, string mimeType, string contentType);
    }
}