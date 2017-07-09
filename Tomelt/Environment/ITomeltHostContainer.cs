namespace Tomelt.Environment {
    public interface ITomeltHostContainer {
        T Resolve<T>();
    }
}