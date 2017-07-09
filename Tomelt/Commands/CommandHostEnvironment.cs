using System.Reflection;
using Tomelt.Environment;
using Tomelt.Localization;

namespace Tomelt.Commands {
    internal class CommandHostEnvironment : HostEnvironment {
        public CommandHostEnvironment() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override void RestartAppDomain() {
            throw new TomeltCommandHostRetryException(T("A change of configuration requires the session to be restarted."));
        }
    }
}