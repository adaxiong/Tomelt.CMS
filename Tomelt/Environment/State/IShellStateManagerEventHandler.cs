using Tomelt.Events;

namespace Tomelt.Environment.State {
    public interface IShellStateManagerEventHandler : IEventHandler {
        void ApplyChanges();
    }
}