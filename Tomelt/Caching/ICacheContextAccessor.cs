namespace Tomelt.Caching {
    public interface ICacheContextAccessor {
        IAcquireContext Current { get; set; }
    }
}