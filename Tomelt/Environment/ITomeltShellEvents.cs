using Tomelt.Events;

namespace Tomelt.Environment {
    public interface ITomeltShellEvents : IEventHandler {
        void Activated();
        void Terminating();
    }
}
