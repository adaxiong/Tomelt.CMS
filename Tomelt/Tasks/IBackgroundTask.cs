namespace Tomelt.Tasks {
    public interface IBackgroundTask : IDependency {
        void Sweep();
    }
}
