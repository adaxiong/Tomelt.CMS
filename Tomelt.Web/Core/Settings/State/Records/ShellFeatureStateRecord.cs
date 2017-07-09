using Tomelt.Environment.State.Models;

namespace Tomelt.Core.Settings.State.Records {
    public class ShellFeatureStateRecord {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ShellFeatureState.State InstallState { get; set; }
        public virtual ShellFeatureState.State EnableState { get; set; }
    }
}
