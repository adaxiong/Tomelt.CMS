using System;
using Autofac;
using Tomelt.Environment.Configuration;
using Tomelt.Environment.Descriptor.Models;
using Tomelt.Environment.ShellBuilders.Models;

namespace Tomelt.Environment.ShellBuilders {
    public class ShellContext : IDisposable {
        private bool _disposed = false;

        public ShellSettings Settings { get; set; }
        public ShellDescriptor Descriptor { get; set; }
        public ShellBlueprint Blueprint { get; set; }
        public ILifetimeScope LifetimeScope { get; set; }
        public ITomeltShell Shell { get; set; }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!_disposed) {

                if (disposing) {
                    LifetimeScope.Dispose();
                }

                Settings = null;
                Descriptor = null;
                Blueprint = null;
                Shell = null;

                _disposed = true;
            }
        }

        ~ShellContext() {
            Dispose(false);
        }
    }
}