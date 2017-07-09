using System;

namespace Tomelt.Layouts.Services {
    public interface ICurrentThemeShapeBindingResolver : IDependency {
        IDisposable Enable();
    }
}