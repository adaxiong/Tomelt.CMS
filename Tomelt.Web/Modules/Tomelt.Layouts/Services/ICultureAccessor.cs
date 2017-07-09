using System.Globalization;

namespace Tomelt.Layouts.Services {
    public interface ICultureAccessor : IDependency {
        CultureInfo CurrentCulture { get; }
    }
}