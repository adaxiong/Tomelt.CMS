using System;

namespace Tomelt.FileSystems.LockFile
{
    public interface ILockFile : IDisposable {
        void Release();
    }
}
