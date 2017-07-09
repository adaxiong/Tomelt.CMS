using Tomelt.Events;

namespace Tomelt.Environment.Configuration {
    public interface IShellSettingsManagerEventHandler : IEventHandler {
        void Saved(ShellSettings settings);
    }
}