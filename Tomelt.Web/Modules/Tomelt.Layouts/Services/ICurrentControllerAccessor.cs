using System.Web.Mvc;

namespace Tomelt.Layouts.Services {
    public interface ICurrentControllerAccessor : IDependency {
        Controller CurrentController { get; }
    }
}