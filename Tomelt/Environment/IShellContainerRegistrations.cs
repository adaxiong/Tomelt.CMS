using System;
using Autofac;

namespace Tomelt.Environment {
    public interface IShellContainerRegistrations {
        Action<ContainerBuilder> Registrations { get; }
    }

    public class ShellContainerRegistrations : IShellContainerRegistrations {
        public ShellContainerRegistrations() {
            Registrations = builder => { return; };
        }

        public Action<ContainerBuilder> Registrations { get; private set; }
    }
}
