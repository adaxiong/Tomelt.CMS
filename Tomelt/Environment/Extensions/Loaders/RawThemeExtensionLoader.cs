using System;
using System.Linq;
using Tomelt.Environment.Extensions.Models;
using Tomelt.FileSystems.Dependencies;
using Tomelt.FileSystems.VirtualPath;
using Tomelt.Logging;

namespace Tomelt.Environment.Extensions.Loaders {
    public class RawThemeExtensionLoader : ExtensionLoaderBase {
        private readonly IVirtualPathProvider _virtualPathProvider;

        public RawThemeExtensionLoader(IDependenciesFolder dependenciesFolder, IVirtualPathProvider virtualPathProvider)
            : base(dependenciesFolder) {
            _virtualPathProvider = virtualPathProvider;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }
        public bool Disabled { get; set; }

        public override int Order { get { return 10; } }

        public override ExtensionProbeEntry Probe(ExtensionDescriptor descriptor) {
            if (Disabled)
                return null;

            // Temporary - theme without own project should be under ~/themes
            if (descriptor.Location.StartsWith("~/Themes",StringComparison.InvariantCultureIgnoreCase)) {
                string projectPath = _virtualPathProvider.Combine(descriptor.Location, descriptor.Id,
                                           descriptor.Id + ".csproj");

                // ignore themes including a .csproj in this loader
                if ( _virtualPathProvider.FileExists(projectPath) ) {
                    return null;
                }

                var assemblyPath = _virtualPathProvider.Combine(descriptor.Location, descriptor.Id, "bin",
                                                descriptor.Id + ".dll");

                // ignore themes with /bin in this loader
                if ( _virtualPathProvider.FileExists(assemblyPath) )
                    return null;

                return new ExtensionProbeEntry {
                    Descriptor = descriptor,
                    Loader = this,
                    VirtualPath = descriptor.VirtualPath,
                    VirtualPathDependencies = Enumerable.Empty<string>(),
                };
            }
            return null;
        }

        protected override ExtensionEntry LoadWorker(ExtensionDescriptor descriptor) {
            if (Disabled)
                return null;

            Logger.Information("Loaded no-code theme \"{0}\"", descriptor.Name);

            return new ExtensionEntry {
                Descriptor = descriptor,
                Assembly = GetType().Assembly,
                ExportedTypes = new Type[0]
            };
        }
    }
}