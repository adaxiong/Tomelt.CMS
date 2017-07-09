using System.IO;

namespace Tomelt.Environment.Extensions.Compilers {
    public interface IProjectFileParser {
        ProjectFileDescriptor Parse(string virtualPath);
        ProjectFileDescriptor Parse(Stream stream);
    }
}